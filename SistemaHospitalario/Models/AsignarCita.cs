using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospitalario.Models
{
    public class AsignarCita
    {
        public int CitasId { get; set; }
        [Required]
        public int Doctor { get; set; }
        [Required]
        public DateTime HoraCita { get; set; }
        [Required]
        public int Paciente { get; set; }
        [Required]
        public int Duracion { get; set; }
    }
}
