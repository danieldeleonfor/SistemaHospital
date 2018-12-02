using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospitalario.Models
{
    public class VisitaCliente
    {
        public Usuario Doctor { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; }
        public string Comentario { get; set; }
        public string RecetaMedicamentos { get; set; }
        public DateTime FechaProximaVisita { get; set; }
    }
}
