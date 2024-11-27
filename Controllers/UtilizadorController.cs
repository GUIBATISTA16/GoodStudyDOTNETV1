using System.Text.Json;
using GoodStudydotNET.Models;
using GoodStudydotNET.Models.Requests;
using GoodStudydotNET.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GoodStudydotNET.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class UtilizadorController : Controller
    {
        [HttpPost]
        public ActionResult<string> Save([FromBody] Utilizador utilizador)
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
                    return Created("Conta Criada", JsonSerializer.Serialize(u));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
            
        }

        [HttpPost("{tipo}/{id}")]
        public ActionResult<string> UploadImage(IFormFile image,int tipo,int id)
        {
            try
            {
                if ((tipo != 1 && tipo != 2) || id < 0 || image == null || image.Length == 0)
                {
                    return BadRequest("Invalid Data");
                }
                string code = UtilizadorRepository.UploadImage(image, tipo, id);
                if (code != "")
                {
                    return BadRequest(code);
                }

                return Ok("Image uploaded");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Error uploading image");
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
