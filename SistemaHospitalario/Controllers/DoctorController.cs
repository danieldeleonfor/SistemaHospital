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

            var usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this);
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ItsAdmin = usuario.EsAdministrador;
            ViewBag.UsuarioRol = usuario.Rol;
            ViewBag.Usuario = usuario.NombreUsuario;
            var doctorLogged = usuario;
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
            var usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this);
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ItsAdmin = usuario.EsAdministrador;
            ViewBag.UsuarioRol = usuario.Rol;
            ViewBag.Usuario = usuario.NombreUsuario;
            if (id == 0)
            {
                ViewBag.Doctor = usuario;
                ViewBag.Pacientes = _Context.Pacientes.ToList();
                ViewBag.Cita = null;
            }
            else
            {
                ViewBag.Doctor = usuario;
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

        [HttpGet]
        public IActionResult ObtenerCitasEnDia(DateTime dia)
        {
            var doctor = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this);
            if (doctor == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ItsAdmin = doctor.EsAdministrador;
            ViewBag.Usuario = doctor.NombreUsuario;
            var PagoDia = _Context.Consultas
                .Where(x => x.DiaActual.ToString("dd/MM/yyyy") == dia.ToString("dd/MM/yyyy"))
                .Sum(x => x.PagoServicio);
            return Ok(PagoDia);
        }

        [HttpPost]
        public IActionResult CrearConsulta(ConsultaPaciente nuevo)
        {
            var usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this);
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ItsAdmin = usuario.EsAdministrador;
            ViewBag.UsuarioRol = usuario.Rol;
            ViewBag.Usuario = usuario.NombreUsuario;
            if (ModelState.IsValid)
            {
                _Context.Consultas.Add(nuevo);
                _Context.SaveChanges();
                return RedirectToAction(nameof(ListadoConsulta));
            }
            return View(nuevo);
        }

        [HttpGet]
        public IActionResult ListadoConsulta()
        {
            var usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this);
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ItsAdmin = usuario.EsAdministrador;
            ViewBag.UsuarioRol = usuario.Rol;
            ViewBag.Usuario = usuario.NombreUsuario;
            var listado = new List<ConsultaPacienteModel>();
            var listadoConsulta = _Context.Consultas.Include(x => x.Recetas)
                .ToList();
            foreach(var consulta in listadoConsulta)
            {
                var cita = _Context.Citas
                    .Include(x=>x.Paciente)
                    .FirstOrDefault(x => x.CitasId == consulta.CitaId);
                listado.Add(new ConsultaPacienteModel()
                {
                    Cita=cita,
                    Consulta=consulta
                });
            }
            return View(listado);
        }

        [HttpPost]
        public IActionResult AgregarPago(Pago pago)
        {
            var modelo=_Context.Consultas.FirstOrDefault(r => r.ConsultaPacienteId == pago.IdConsulta);
            modelo.PagoServicio = pago.Amount;
            _Context.Consultas.Update(modelo);
            _Context.SaveChanges();
            return Ok();
        }

        public IActionResult VerReseta(int id)
        {
            var usuario = RolesYUsuarios.ObtenerUsuarioLogeado(_Context, this);
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ItsAdmin = usuario.EsAdministrador;
            ViewBag.UsuarioRol = usuario.Rol;
            ViewBag.Usuario = usuario.NombreUsuario;
            var recetas = _Context.Consultas
                .Include(x=>x.Recetas)
                .FirstOrDefault(r => r.ConsultaPacienteId == id).Recetas;
            if (recetas == null)
            {
                return RedirectToAction(nameof(ListadoConsulta));
            }
            ViewBag.Reseta = recetas;
            return View();
        }

    }
}
