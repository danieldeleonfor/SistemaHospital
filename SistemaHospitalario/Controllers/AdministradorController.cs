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
    public class AdministradorController : Controller
    {
        public DbContext2 DbContext { get; }
        public AdministradorController(DbContext2 dbContext)
        {
            this.DbContext = dbContext;
        }

        [HttpGet]
        public IActionResult CrearUsuario()
        {
            if (HttpContext.Session.GetString(GeneralConfig.userSessionKey) == null)
            {
                return Redirect("/home/login");
            }
            ViewBag.Usuario = HttpContext.Session.GetString(GeneralConfig.userSessionKey);
            return View();
        }

        [HttpPost]
        public IActionResult CrearUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                DbContext.Usuarios.Add(usuario);
                DbContext.SaveChanges();
                return RedirectToAction("ListadoUsuario");
            }
            return View(usuario);
        }

        [HttpGet]
        public IActionResult ListadoUsuario()
        {
            if (HttpContext.Session.GetString(GeneralConfig.userSessionKey) == null)
            {
                return Redirect("/home/login");
            }
            ViewBag.Usuario = HttpContext.Session.GetString(GeneralConfig.userSessionKey);
            ViewBag.Usuarios = DbContext.Usuarios.ToList();
            return View();
        }
    }
}
