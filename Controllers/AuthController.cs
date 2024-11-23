using System.Text.Json;
using GoodStudydotNET.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoodStudydotNET.Controllers
{
    [Route("/auth/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        [HttpPost]
        public ActionResult<string> Login([FromBody] Dados dados)
        {

            Utilizador u = UtilizadorRepository.Login(dados);
            if (u != null)
            {
                return Ok(JsonSerializer.Serialize(u));
            }

            return BadRequest();
        }
    }
}
