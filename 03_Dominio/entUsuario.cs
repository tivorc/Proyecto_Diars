using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Dominio
{
    public class entUsuario
    {
        public int UsuarioID { get; set; }

        [Display(Name = "Nombre Usuario")]
        public string NombreUsuario { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }
        public string Rol { get; set; }
        public bool Estado { get; set; }

        [Display(Name = "Foto")]
        public string ImgUsuario { get; set; }

        public entUsuario VerificarAcceso(entUsuario u, out String mensaje)
        {
            try
            {
                mensaje = "";
                if (u != null)
                {
                    if (u.Estado == false)
                    {
                        mensaje = "Usuario Inactivo";
                    }
                }
                else
                {
                    mensaje = "Usuario o Password no valido";
                }
                return u;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
