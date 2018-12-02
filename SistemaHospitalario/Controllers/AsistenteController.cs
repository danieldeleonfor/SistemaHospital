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
            return View();
        }
        [HttpPost]
        public IActionResult Paciente(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _Context.Pacientes.Add(paciente);
                _Context.SaveChanges();
                return View();
            }
            else
            {
                return View();
            }
            
        }

        public IActionResult Pacientes()
        {
            ViewBag.Pacientes = _Context.Pacientes.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult ReporteCumpleanioPorMes(int id)
        {
            var pacientes=_Context.Pacientes.Where(r => r.FechaNacimiento.Month == id).ToList();
            ViewBag.Reporte = pacientes;
            return View();
        }

        [HttpGet]
        public IActionResult AniadirCitas()
        {
            var nombreUsuario = HttpContext.Session.GetString(GeneralConfig.userSessionKey);
            var doctor = _Context.Usuarios.FirstOrDefault(r => r.NombreUsuario == nombreUsuario);
            ViewBag.Pacientes = _Context.Pacientes.ToList();
            ViewBag.Doctor = doctor;
            return View();
        }

        [HttpPost]
        public IActionResult AniadirCitas(Citas citas)
        {
            var nombreUsuario = HttpContext.Session.GetString(GeneralConfig.userSessionKey);
            var doctor = _Context.Usuarios.FirstOrDefault(r => r.NombreUsuario == nombreUsuario);
            citas.Doctor = doctor;
            ViewBag.Pacientes = _Context.Pacientes.ToList();
            ViewBag.Doctor = doctor;

            if (citas != null && doctor != null)
            {
                _Context.Citas.Add(citas);
                _Context.SaveChanges();
                return View();
            }
            else
            {
                return View(citas);
            }
        }

        [HttpGet]
        public IActionResult VerificarCedulaPaciente()
        {
            if (true)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }

        }

        [HttpPost]
        public IActionResult VerificarCedulaPaciente(string Cedula )
        {
            var paciente=_Context.Pacientes.FirstOrDefault(r => r.Cedula.Replace("-", string.Empty) == Cedula.Replace("-",string.Empty));
            if (paciente!=null)
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
