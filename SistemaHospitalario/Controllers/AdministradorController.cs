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

        [HttpGet]
        public IActionResult ListadoUsuario()
        {
            ViewBag.Usuarios = DbContext.Usuarios.ToList();
            return View();
        }
    }
}
