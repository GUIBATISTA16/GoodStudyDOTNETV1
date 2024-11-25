using System.Text.Json;
using GoodStudydotNET.Models;
using GoodStudydotNET.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoodStudydotNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : Controller
    {
        [HttpGet]
        public string GetEspecialidades()
        {
            List<Especialidade> especialidades = EspecialidadeRepository.GetEspecialidades();
            return JsonSerializer.Serialize(especialidades);
        }
    }
}
