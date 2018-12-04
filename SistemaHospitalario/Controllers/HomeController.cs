using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaHospitalario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospitalario.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbContext2 DbContext;

        public HomeController(DbContext2 dbContext)
        {
            this.DbContext = dbContext;
        }

        public IActionResult Index()
        {
            var usuario = RolesYUsuarios.ObtenerUsuarioLogeado(DbContext, this);
            if (usuario == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.UsuarioRol = usuario.Rol;
            ViewBag.Usuario = usuario.NombreUsuario;
            ViewBag.ItsAdmin = usuario.EsAdministrador;

            ViewBag.Usuarios = DbContext.Usuarios.Count();
            ViewBag.Pacientes = DbContext.Pacientes.Count();
            ViewBag.Citas = DbContext.Citas.Count();
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Error = "";
            if (DbContext.Usuarios.Count() < 1)
            {
                Usuario administrador = new Usuario()
                {
                    NombreUsuario = "danielito",
                    Contrasenia = "admin",
                    EsAdministrador = true,
                    Nombres = "Daniel",
                    Apellidos = "De Leon",
                    FechaNacimiento = DateTime.Now,
                    Rol = "Administrador"
                };
                DbContext.Usuarios.Add(administrador);
                DbContext.SaveChanges();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            ViewBag.Error = "";
            if (ModelState.IsValid)
            {
                var usuarioLogeado = DbContext.Usuarios.FirstOrDefault(r => r.NombreUsuario == model.Username);
                if (usuarioLogeado != null)
                {
                    if (usuarioLogeado.Contrasenia == model.Password)
                    {
                        HttpContext.Session.SetString(GeneralConfig.userSessionKey, model.Username);
                        return RedirectToAction("Index");
                    }
                }
                HttpContext.Session.SetString(GeneralConfig.userSessionKey, model.Username);
            }

            ViewBag.Error = "El usuario no existe";
            return View(model);
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
