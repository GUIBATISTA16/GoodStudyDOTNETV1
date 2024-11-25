using System.Text.Json;
using GoodStudydotNET.Models;
using GoodStudydotNET.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GoodStudydotNET.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class PedidoController : Controller
    {
        [HttpPost]
        public ActionResult<string> SendPedido([FromBody] Pedido pedido)
        {
            Pedido p = PedidosRepository.SavePedido(pedido);
            if (p != null)
            {
                return Ok(JsonSerializer.Serialize(p));
            }
            return BadRequest();

        }
        [HttpGet("{id}")]
        public ActionResult<string> GetPedidosExplicando(int id)
        {
            List<Pedido> list = PedidosRepository.GetPedidosExplicando(id);
            return Ok(JsonSerializer.Serialize(list));
        }
    }
}
