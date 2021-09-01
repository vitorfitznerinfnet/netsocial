using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetSocial.Api.Requests;
using NetSocial.Api.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetSocial.Api.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsControllers : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody]CreatePostRequest request)
        {
            posts.Add(new GetPostResponse
            {
                Img = request.Img,
                Text = request.Text,
                Title = request.Title
            });

            return Ok();
        }

        static List<GetPostResponse> posts = new List<GetPostResponse>();

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(posts);
        }
    }
}
