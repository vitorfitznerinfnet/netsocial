using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetSocial.Site.Models.Posts;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Azure.Storage.Blobs;
using System;
using System.IO;

namespace NetSocial.Site.Controllers
{
    public class PostsController : Controller
    {
        public HttpClient postman { get; set; }

        public PostsController(IHttpClientFactory httpClientFactory)
        {
            postman = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel();

            var resultado = await postman.GetAsync("https://localhost:44341/api/posts");

            var postsJson = await resultado.Content.ReadAsStringAsync();

            List<PostModel> posts = JsonConvert.DeserializeObject<List<PostModel>>(postsJson);

            viewModel.Posts = posts;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(string title, string text, IFormFile imagem)
        {
            var endereco = FazerUpload(imagem);

            var createPost = new
            {
                Title = title,
                Text = text,
                Img = endereco
            };

            var postAsJson = JsonConvert.SerializeObject(createPost);

            var conteudo = new StringContent(postAsJson, System.Text.Encoding.UTF8, "application/json");

            var response = await postman.PostAsync("https://localhost:44341/api/posts", conteudo);

            if (!response.IsSuccessStatusCode)
            {
                RemoveUpload(endereco);
            }

            return RedirectToAction("Index");
        }

        private void RemoveUpload(string endereco)
        {
            throw new NotImplementedException();
        }

        private string FazerUpload(IFormFile imagem)
        {
            string containerName = "post-images";

            var pastaDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(pastaDesktop, imagem.FileName);

            var blobName =  $"{Guid.NewGuid()}.{imagem.FileName.Split('.')[1]}";

            BlobContainerClient container = new BlobContainerClient("UseDevelopmentStorage=true", containerName);

            container.CreateIfNotExists();

            BlobClient blob = container.GetBlobClient(blobName);

            blob.Upload(filePath);

            var enderecoDaImagem = blob.Uri.ToString();

            return enderecoDaImagem;
        }
    }
}
