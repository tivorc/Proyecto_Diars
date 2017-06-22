using _03_Dominio;
using _04_Presistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Aplicacion
{
    public class appUsuario
    {
        #region singleton
        private static readonly appUsuario _instancia = new appUsuario();
        public static appUsuario Instancia
        {
            get { return appUsuario._instancia; }
        }
        #endregion singleton

        #region metodos

        public entUsuario VerificarAcceso(String Usuario, String Contrasena, out String mensaje)
        {
            try
            {
                entUsuario u = daoUsuario.Instancia.VerificarAcceso(Usuario, Contrasena);
                entUsuario ux = new entUsuario();
                return ux.VerificarAcceso(u, out mensaje);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion metodos
    }
}
