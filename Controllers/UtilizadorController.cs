using GoodStudydotNET.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoodStudydotNET.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class UtilizadorController : Controller
    {
        [HttpPost]
        public ActionResult<Utilizador> Save([FromBody] Utilizador utilizador)
        {
            Console.WriteLine("Save");
            Utilizador u = UtilizadorRepository.AddUtilizador(utilizador.Dados, utilizador.Explicador, utilizador.Explicando);
            if (u == null)
            {
                return BadRequest("Dados incorretos");
            }
            else
            {
                return Created("Conta Criada",u);
            }
        }
    }
}
