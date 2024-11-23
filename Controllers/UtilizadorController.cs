using System.Text.Json;
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
            try
            {
                Utilizador u = UtilizadorRepository.AddUtilizador(utilizador.Dados, utilizador.Explicador, utilizador.Explicando);
                if (u == null)
                {
                    return BadRequest("Dados incorretos");
                }
                else
                {
                    return Created("Conta Criada", u);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
            
        }

        [HttpPost]
        public ActionResult<string> Pesquisa([FromBody] Pesquisa pesquisa)
        {
            try
            {
                List<Explicador> list = UtilizadorRepository.Pesquisa(pesquisa);
                return  Ok(JsonSerializer.Serialize(list));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }


    }
}
