﻿using Microsoft.AspNetCore.Mvc;
using SGPI.Models;

namespace SGPI.Controllers
{
    public class AdministradorController : Controller
    {
        SGPIDBContext context = new SGPIDBContext();
        public IActionResult Login()
        {

            //Create
            /*Usuario usr = new Usuario();
            usr.PrimerNombre = "Mauricio";
            usr.SegundoNombre = String.Empty;
            usr.PrimerApellido = "Amariles";
            usr.SegundoApellido = "Camacho";
            usr.Email = "mauricio.amariles@tdea-edu.co";
            usr.IdDoc = 1;
            usr.IdGenero = 1;
            usr.IdRol = 1;
            usr.IdPrograma = 1;
            usr.NumeroDocumento = "123456789";
            usr.Password = "123456789";

            context.Add(usr);
            context.SaveChanges();*/



            //Query
            /*Usuario usuario = new Usuario();

            usuario = context.Usuarios
                .Single(b => b.NumeroDocumento == "123456789");

            List<Usuario> usuarios = new List<Usuario>();
            usuarios = context.Usuarios.ToList();



            //Update
            var usr = context.Usuarios
                .Where(cursor => cursor.IdUsuario == 1)
                .FirstOrDefault();

            if (usr != null) 
            {
                usr.SegundoNombre = "Diego";
                usr.SegundoApellido = "Camacho";

                context.Usuarios.Update(usr);
                context.SaveChanges();
            }



            //Delete
            var usuarioEliminar = context.Usuarios
                .Where(cursor => cursor.IdUsuario == 1)
                .FirstOrDefault();

            context.Usuarios.Remove(usuarioEliminar);*/

            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario user)
        {
            string numeroDoc = user.NumeroDocumento;
            string pass = user.Password;

            var usuarioLogin = context.Usuarios
                .Where(consulta => consulta.NumeroDocumento == numeroDoc && consulta.Password == pass)
                .FirstOrDefault();

            if (usuarioLogin != null)
            {
                //esto lo manda para el modulo de administrador
                if (usuarioLogin.IdRol == 1) {
                    CrearUsuario();
                    BuscarUsuario();
                    Reporte();
                    return Redirect("Administrador/CrearUsuario");
                }
                //esto lo manda para el modulo de coordinador
                else if (usuarioLogin.IdRol == 2) {

                    return Redirect("" + "Coordinador/BuscarCoordinador");
                }
                //esto lo manda para el modulo de estudiante
                else if (usuarioLogin.IdRol == 3) {
                    return Redirect("/Estudiante/Actualizar/?IdUsuario=" + usuarioLogin.IdUsuario);
                }
                //no existe el rol
                else { }
            }
            else
            {

            }
            {
                return ViewBag.mensaje = "Usuario no existe";
            }
            return View();
        }
        public IActionResult OlvidarContrasena()
        {

            return View();
        }

        public IActionResult CrearUsuario()
        {
            ViewBag.genero = context.Generos.ToList();
            ViewBag.documento = context.Documentos.ToList();
            ViewBag.rol = context.Rols.ToList();
            ViewBag.programa = context.Programas.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult CrearUsuario(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            context.SaveChanges();

            ViewBag.mensaje = "Usuario creado exitosamente en la faz de la tierra";

            ViewBag.genero = context.Generos.ToList();
            ViewBag.documento = context.Documentos.ToList();
            ViewBag.rol = context.Rols.ToList();
            ViewBag.programa = context.Programas.ToList();

            return View();
        }

        public IActionResult BuscarUsuario()
        {
            Usuario us = new Usuario();
            return View(us);
        }

        [HttpPost]
        public IActionResult BuscarUsuario(Usuario usuario)
        {
            String numeroDoc = usuario.NumeroDocumento;

            var user = context.Usuarios.Where(consulta => consulta.NumeroDocumento == numeroDoc).FirstOrDefault();

            if (user != null)
            {
                return View(user);
            }
            else
            {
                return View();
            }

        }

        public IActionResult EditarUsuario(int ? IdUsuario)
        {
            Usuario usuario = context.Usuarios.Find(IdUsuario);

            if (usuario != null)
            {
                ViewBag.genero = context.Generos.ToList();
                ViewBag.documento = context.Documentos.ToList();
                ViewBag.rol = context.Rols.ToList();
                ViewBag.programa = context.Programas.ToList();
                return View(usuario);
            }
            else
            {
                ViewBag.genero = context.Generos.ToList();
                ViewBag.documento = context.Documentos.ToList();
                ViewBag.rol = context.Rols.ToList();
                ViewBag.programa = context.Programas.ToList();
                return Redirect("BuscarUsuario");
            }

            return View(usuario);
        }

        [HttpPost]
        public IActionResult EditarUsuario(Usuario usuario)
        {
            context.Update(usuario);
            context.SaveChanges();

            ViewBag.genero = context.Generos.ToList();
            ViewBag.documento = context.Documentos.ToList();
            ViewBag.rol = context.Rols.ToList();
            ViewBag.programa = context.Programas.ToList();

            return Redirect("BuscarUsuario");
        }
        public IActionResult EliminarUsuario(int ? IdUsuario)
        {
            Usuario user = context.Usuarios.Find(IdUsuario);

            if (user != null) 
            {
                context.Remove(user);
                context.SaveChanges();
            }

            return Redirect("BuscarUsuario");
        }
        public IActionResult Reporte()
        {
            ViewBag.documento = context.Documentos.ToList();

            return View();
        }
    }
}
