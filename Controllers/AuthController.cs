using System.Text.Json;
using GoodStudydotNET.Models;
using GoodStudydotNET.Repositories;
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
            try
            {
                Utilizador u = UtilizadorRepository.Login(dados);
                if (u != null)
                {
                    return Ok(JsonSerializer.Serialize(u));
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
