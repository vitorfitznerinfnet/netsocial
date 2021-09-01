using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetSocial.Site.Models.Posts
{
    public class IndexViewModel
    {
        public PostModel Formulario { get; set; }
        public List<PostModel> Posts { get; set; } = new List<PostModel>();
    }
}
