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
        public IActionResult CrearUsuario(int id)
        {
            var usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this);
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = usuario.NombreUsuario;
            ViewBag.UsuarioRol = usuario.Rol;
            ViewBag.ItsAdmin = usuario.EsAdministrador;
            if (id == 0)
                return View();
            else
            {
                var usuarioEncontrado = _Context.Usuarios.FirstOrDefault(r => r.UsuarioId == id);
                return View(usuarioEncontrado);
            }
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
            ViewBag.ItsAdmin = usuario.EsAdministrador;
            var invalids = ModelState.Values.Where(r => r.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid).Count();
            if (ModelState.IsValid || invalids == 1)
            {
                if (usuario.UsuarioId != 0)
                {
                    _Context.Usuarios.Update(usuario);
                }
                else
                {
                    _Context.Usuarios.Add(usuario);
                }
                
                _Context.SaveChanges();
                return RedirectToAction("ListadoUsuario");
            }
            return View(usuario);
        }

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            var usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this);
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var usuarioEnc=_Context.Usuarios.FirstOrDefault(r => r.UsuarioId == id);

            if (usuarioEnc != null)
            {
                _Context.Usuarios.Remove(usuarioEnc);
                _Context.SaveChanges();
            }

            return RedirectToAction(nameof(ListadoUsuario));
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
            ViewBag.ItsAdmin = usuario.EsAdministrador;
            ViewBag.Usuarios = _Context.Usuarios.ToList();
            return View();
        }
    }
}
