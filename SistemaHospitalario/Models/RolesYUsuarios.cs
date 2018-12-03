using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaHospitalario.Models
{
    public class RolesYUsuarios
    {
        public static Usuario ObtenerUsuarioLogeado(DbContext2 context, Controller controller)
        {

            var nombreUsuario= controller.HttpContext.Session.GetString(GeneralConfig.userSessionKey);
            return context.Usuarios.FirstOrDefault(x => x.NombreUsuario == nombreUsuario);
        }

        public static bool UsuarioTienePermiso(Usuario usuarioLogeado, List<string> rolesPermitidos)
        {
            foreach(string rol in rolesPermitidos)
            {
                if(usuarioLogeado.Rol == rol)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
