using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetSocial.Api.Requests;
using NetSocial.Api.Security;
using System;
using System.Threading.Tasks;

namespace NetSocial.Api.Controllers
{
    [Route("api/security")]
    public class SecurityController : Controller
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<dynamic> CreateUser([FromBody] LoginRequest model)
        {
            var user = new User();
            user.Username = model.UserName;
            user.Password = CreateHash(model.Password);

            UserRepository.Save(user);

            return Ok();
        }

        private string CreateHash(string password)
        {
            return "sal.." + password;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public  ActionResult<dynamic> Authenticate([FromBody] LoginRequest model)
        {
            //falar de hash na próxima aula
            //fazer exemplo de referencia circular e resolver no mapeamento em aula
            var user = UserRepository.Get(model.UserName, model.Password);

            if (user == null)
                return Ok(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";

    }
}
