using _03_Dominio;
using _04_Presistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Aplicacion
{
    public class appTrabajador
    {
        #region singleton
        private static readonly appTrabajador _instancia = new appTrabajador();
        public static appTrabajador Instancia
        {
            get { return appTrabajador._instancia; }
        }
        #endregion singleton

        public entTrabajador DevolverTrabajadorLogin(int idUsuario)
        {
            try
            {
                return daoTrabajador.Instancia.DevolverTrabajadorLogin(idUsuario);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
