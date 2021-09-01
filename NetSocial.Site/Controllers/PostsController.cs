using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetSocial.Site.Models.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NetSocial.Site.Controllers
{
    public class PostsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel();

            HttpClient postman = new HttpClient();

            var resultado = await postman.GetAsync("https://localhost:44341/api/posts");

            var postsJson = await resultado.Content.ReadAsStringAsync();

            List<PostModel> posts = JsonConvert.DeserializeObject<List<PostModel>>(postsJson);

            viewModel.Posts = posts;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(string title, string text, IFormFile imagem)
        {
            HttpClient postman = new HttpClient();

            var createPost = new
            {
                Title = title,
                Text = text,
                Img = ""
            };

            var postAsJson = JsonConvert.SerializeObject(createPost);

            var conteudo = new StringContent(postAsJson, System.Text.Encoding.UTF8, "application/json");

            await postman.PostAsync("https://localhost:44341/api/posts", conteudo);

            return RedirectToAction("Index");
        }
    }
}
