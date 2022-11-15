using Microsoft.AspNetCore.Mvc;
using SGPI.Models;

namespace SGPI.Controllers
{
    public class CoordinadorController : Controller
    {
        SGPIDBContext context = new SGPIDBContext();
        public IActionResult BuscarCoordinador()
        {
            return View();
        }
        public IActionResult Entrevistas()
        {
            return View();
        }
        public IActionResult Homologacion()
        {
            return View();
        }
        public IActionResult ProgramarAsignatura()
        {
            ViewBag.programa = context.Programas.ToList();
            return View();
        }
        public IActionResult ReporteCoordinador()
        {
            return View();
        }
    }
}
