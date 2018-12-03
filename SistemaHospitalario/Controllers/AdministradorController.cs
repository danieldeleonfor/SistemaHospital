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
        public DbContext2 _Context { get; }
        public AdministradorController(DbContext2 dbContext)
        {
            this._Context = dbContext;
        }

        [HttpGet]
        public IActionResult CrearUsuario()
        {
            if (RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this).NombreUsuario;
            return View();
        }

        [HttpPost]
        public IActionResult CrearUsuario(Usuario usuario)
        {
            if (RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this).NombreUsuario;
            if (ModelState.IsValid)
            {
                _Context.Usuarios.Add(usuario);
                _Context.SaveChanges();
                return RedirectToAction("ListadoUsuario");
            }
            return View(usuario);
        }

        [HttpGet]
        public IActionResult ListadoUsuario()
        {
            if (RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this).NombreUsuario;
            ViewBag.Usuarios = _Context.Usuarios.ToList();
            return View();
        }
    }
}
