using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetSocial.Api.Requests
{
    public class CreatePostRequest
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Img { get; set; }
    }
}
