﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospitalario.Models
{
    public class ConsultaPaciente
    {
        public int ConsultaPacienteId { get; set; }
        [Required]
        public Citas Cita { get; set; }
        public List<Receta> Recetas { get; set; }
        [Required]
        public double PagoServicio { get; set; }
        public DateTime FechaProximaCita { get; set; }
    }
}