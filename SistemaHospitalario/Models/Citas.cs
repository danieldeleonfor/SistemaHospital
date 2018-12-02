using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospitalario.Models
{
    public class Citas
    {
        [Required]
        public int CitasId { get; set; }
        [Required]
        public Usuario Doctor { get; set; }
        [Required]
        public DateTime HoraCita { get; set; }
        [Required]
        public Paciente Paciente { get; set; }
        [Required]
        public int Duracion { get; set; }
    }
}
