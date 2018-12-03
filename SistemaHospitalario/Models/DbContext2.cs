using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospitalario.Models
{
    public class DbContext2 : DbContext
    {
        public DbContext2(DbContextOptions options):base(options)
        {

        }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Citas> Citas { get; set; }
        public DbSet<ConsultaPaciente> Consultas { get; set; }
        public DbSet<Receta> Receta { get; set; }
    }
}
