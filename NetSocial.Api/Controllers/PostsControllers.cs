using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetSocial.Api.Requests;
using NetSocial.Api.Responses;
using NetSocial.Core;
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
        public Aplication App { get; }

        public PostsControllers(Aplication app)
        {
            App = app;
        }

        [HttpPost]
        public ActionResult Post([FromBody]CreatePostRequest request)
        {
            App.CriarPost(request.Title, request.Text, request.Img);

            return Ok();
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
