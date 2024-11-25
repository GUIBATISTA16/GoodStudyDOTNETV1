using GoodStudydotNET.Models;
using GoodStudydotNET.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GoodStudydotNET.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CConta()
        {
            List<Especialidade> especialidades = EspecialidadeRepository.GetEspecialidades();
            return View(especialidades);
        }

        public IActionResult Home()
        {
            return View();
        }
    }
}
