using Microsoft.AspNetCore.Mvc;
using SGPI.Models;

namespace SGPI.Controllers
{
    public class EstudianteController : Controller
    {
        SGPIDBContext context = new SGPIDBContext();

        public IActionResult Actualizar(int? IdUsuario)
        {
            Usuario usuario = context.Usuarios.Find(IdUsuario);

            if (usuario != null) 
            {
                ViewBag.documento = context.Documentos.ToList();
                ViewBag.genero = context.Generos.ToList();
                ViewBag.programa = context.Programas.ToList();

                return View(usuario);
            }
            else 
            { 
            return Redirect("/Administrador/Login");
            }
        }

        [HttpPost]
        public IActionResult Actualizar(Usuario usuario)
        {
            var usuarioActualizar = context.Usuarios.Where(u => u.IdUsuario == usuario.IdUsuario).FirstOrDefault();

            usuarioActualizar.NumeroDocumento = usuario.NumeroDocumento;
            usuarioActualizar.IdGenero = usuario.IdGenero;
            usuarioActualizar.IdPrograma = usuario.IdPrograma;
            usuarioActualizar.PrimerNombre = usuario.PrimerNombre;
            usuarioActualizar.SegundoNombre = usuario.SegundoNombre;
            usuarioActualizar.PrimerApellido = usuario.PrimerApellido;
            usuarioActualizar.SegundoApellido = usuario.SegundoApellido;
            usuarioActualizar.Password = usuario.Password;

            context.Update(usuarioActualizar);
            context.SaveChanges();

            ViewBag.mensajeActualizar = "Usuario modificado exitosamente en la faz de la tierra";

            return Redirect("/Estudiante/Actualizar/?IdUsuario=" + usuarioActualizar.IdUsuario);
        }
        public IActionResult Pagos()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Pagos(Pago pago)
        {
            context.Pagos.Add(pago);
            context.SaveChanges();
            return View();
        }
    }
}
