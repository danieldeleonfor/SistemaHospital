using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaHospitalario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospitalario.Controllers
{
    public class AsistenteController : Controller
    {
        private readonly DbContext2 _Context;

        public AsistenteController(DbContext2 context)
        {
            _Context = context;
        }

        [HttpGet]
        public IActionResult Paciente()
        {
            if (RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this).NombreUsuario;
            return View();
        }
        [HttpPost]
        public IActionResult Paciente(Paciente paciente)
        {
            if (RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this).NombreUsuario;

            if (ModelState.IsValid)
            {
                _Context.Pacientes.Add(paciente);
                _Context.SaveChanges();
                return RedirectToAction("Pacientes");
            }
            else
            {
                return View();
            }

        }

        public IActionResult ObtenerCumpleanios(int mes)
        {
            if (RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var pacientes = _Context.Pacientes.Where(r => r.FechaNacimiento.Month == mes).ToList();
            return Ok(pacientes);
        }
        

        public IActionResult Pacientes()
        {
            if (RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this).NombreUsuario;
            ViewBag.Pacientes = _Context.Pacientes.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult ReporteCumpleanioPorMes(int id)
        {
            if (RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this).NombreUsuario;
            var pacientes = _Context.Pacientes.Where(r => r.FechaNacimiento.Month == id).ToList();
            ViewBag.Reporte = pacientes;
            return View();
        }

        [HttpGet]
        public IActionResult AniadirCitas()
        {
            if (RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this).NombreUsuario;
            ViewBag.Pacientes = _Context.Pacientes.ToList();
            ViewBag.Doctores = _Context.Usuarios.Where(x => x.Rol == "Doctor").ToList();
            return View();
        }

        [HttpGet]
        public IActionResult ObtenerCitasEnDia(DateTime dia)
        {
            if (RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this).NombreUsuario;
            if (dia < DateTime.Now)
            {
                return Ok(new List<Citas>());
            }
            var citas = _Context.Citas
                .Include(x => x.Doctor)
                .Include(x => x.Paciente)
                .Where(x => x.HoraCita.ToString("dd/MM/yyyy") == dia.ToString("dd/MM/yyyy"));
            return Ok(citas);

        }

        [HttpGet]
        public IActionResult CitasPorDias()
        {
            if (RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this).NombreUsuario;
            var citas = _Context.Citas.Where(x => x.HoraCita.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")).ToList();
            if (citas == null)
            {
                citas = new List<Citas>();
            }
            ViewBag.Citas = citas;
            return View();
        }


        public IActionResult BorrarCita(int id)
        {
            if (RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this).NombreUsuario;
            var citaABorrar = _Context.Citas.FirstOrDefault(r => r.CitasId == id);

            if (citaABorrar != null)
            {
                _Context.Citas.Remove(citaABorrar);
                _Context.SaveChanges();
            }

            return RedirectToAction("ListadoCitas");
        }

        [HttpPost]
        public IActionResult AniadirCitas(AsignarCita asignar)
        {
            if (RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this).NombreUsuario;
            ViewBag.Pacientes = _Context.Pacientes.ToList();
            ViewBag.Doctores = _Context.Usuarios.Where(x => x.Rol == "Doctor").ToList();

            var cita = new Citas()
            {
                HoraCita = asignar.HoraCita,
                Doctor = _Context.Usuarios.FirstOrDefault(r => r.UsuarioId == asignar.Doctor),
                Paciente = _Context.Pacientes.FirstOrDefault(r => r.PacienteId == asignar.Paciente),
                Duracion = asignar.Duracion
            };

            if (ModelState.IsValid)
            {
                _Context.Citas.Add(cita);
                _Context.SaveChanges();
                return View();
            }
            else
            {
                return View(cita);
            }
        }

        [HttpGet]
        public IActionResult ListadoCitas()
        {
            if (RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this).NombreUsuario;
            var nombreUsuario = HttpContext.Session.GetString(GeneralConfig.userSessionKey);

            ViewBag.Citas = _Context.Citas
                .Include(x => x.Doctor)
                .Include(x => x.Paciente)
                .ToList();

            return View();
        }

        [HttpPost]
        public IActionResult VerificarCedulaPaciente(string Cedula)
        {
            if (RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this).NombreUsuario;
            var paciente = _Context.Pacientes.FirstOrDefault(r => r.Cedula.Replace("-", string.Empty) == Cedula.Replace("-", string.Empty));
            if (paciente != null)
            {
                return Ok(paciente);
            }
            else
            {
                return Ok(false);
            }

        }
    }
}
