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
            var usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this);
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = usuario.NombreUsuario;
            ViewBag.UsuarioRol = usuario.Rol;
            return View();
        }

        [HttpPost]
        public IActionResult CrearUsuario(Usuario usuario)
        {
            var usuarioLog = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this);
            if (usuarioLog == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = usuarioLog.NombreUsuario;
            ViewBag.UsuarioRol = usuarioLog.Rol;
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
            var usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this);
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = usuario.NombreUsuario;
            ViewBag.UsuarioRol = usuario.Rol;
            ViewBag.Usuarios = _Context.Usuarios.ToList();
            return View();
        }
    }
}
