﻿using Microsoft.AspNetCore.Mvc;
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
            _Context.Pacientes.Add(paciente);
            _Context.SaveChanges();
            return View();
        }

        public IActionResult Pacientes()
        {
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
            var paciente=_Context.Pacientes.FirstOrDefault(r => r.Cedula == Cedula.Replace("-",string.Empty));
            if (paciente==null)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
            
        }
    }
}
