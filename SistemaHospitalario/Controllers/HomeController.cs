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
            if (HttpContext.Session.GetString(GeneralConfig.userSessionKey) == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.Usuario = HttpContext.Session.GetString(GeneralConfig.userSessionKey);
            return View();
        }

        public IActionResult CreateUser()
        {
            if (DbContext.Usuarios.Count() < 1)
            {
                Usuario administrador = new Usuario()
                {
                    NombreUsuario="admin",
                    Contrasenia="admin",
                    EsAdministrador=true,
                    Nombres="Administrator",
                    Apellidos="Admin",
                    FechaNacimiento=DateTime.Now,
                    Rol="Nada"
                };
                DbContext.Usuarios.Add(administrador);
                DbContext.SaveChanges();
            }
            HttpContext.Session.SetString(GeneralConfig.userSessionKey, "Jorge");
            return RedirectToAction("Index");
        }

        public IActionResult FakeLogin()
        {
            var usuario=DbContext.Usuarios.FirstOrDefault(r => r.NombreUsuario == "admin");
            HttpContext.Session.SetString(GeneralConfig.userSessionKey, usuario.NombreUsuario);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
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
            }
            HttpContext.Session.SetString(GeneralConfig.userSessionKey, model.Username);
            
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
