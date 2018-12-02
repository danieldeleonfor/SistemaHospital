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
            return View();
        }

        [HttpPost]
        public IActionResult CrearUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                DbContext.Usuarios.Add(usuario);
                DbContext.SaveChanges();
                return View();
            }
            return View(usuario);
        }

        [HttpGet]
        public IActionResult ListadoUsuario()
        {
            ViewBag.Usuarios = DbContext.Usuarios.ToList();
            return View();
        }
    }
}
