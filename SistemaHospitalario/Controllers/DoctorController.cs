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
    public class DoctorController : Controller
    {
        private readonly DbContext2 _Context;

        public DoctorController(DbContext2 context)
        {
            _Context = context;
        }

        public IActionResult ConsultaCita()
        {

            if (RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this).NombreUsuario;
            var doctorLogged = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this);
            var citas = _Context.Citas
                .Include(x => x.Doctor)
                .Include(x => x.Paciente)
                .Where(x => x.Doctor.UsuarioId == doctorLogged.UsuarioId)
                .ToList();
            //return Ok(doctorLogged);
            //return Ok(citas);
            if (citas == null)
            {
                citas = new List<Citas>();
            }
            ViewBag.Citas = citas;
            return View();
        }
        public IActionResult CrearConsultaa()
        {
            return View();
        }

        public IActionResult CrearConsulta(int id)
        {
            if (RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this).NombreUsuario;
            if (id == 0)
            {
                ViewBag.Doctor = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this);
                ViewBag.Pacientes = _Context.Pacientes.ToList();
                ViewBag.Cita = null;
            }
            else
            {
                ViewBag.Doctor = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this);
                var citas = _Context.Citas.
                    Include(r => r.Doctor)
                    .Include(r => r.Paciente)
                    .FirstOrDefault(r => r.CitasId == id);

                if (citas == null)
                {
                    return RedirectToAction("ConsultaCita");
                }

                ViewBag.Cita = citas;
            }
            return View();
        }
        [HttpPost]
        public IActionResult CrearConsulta(ConsultaPaciente nuevo)
        {
            if (RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this).NombreUsuario;
            if (ModelState.IsValid)
            {
                if (nuevo.Cita.CitasId == 0)
                {
                    var cita = _Context.Citas.Add(nuevo.Cita).Entity;
                    _Context.SaveChanges();
                    nuevo.Cita = cita;
                }
                _Context.Consultas.Add(nuevo);
                _Context.SaveChanges();
                return View();
            }
            return View(nuevo);
        }
    }
}
