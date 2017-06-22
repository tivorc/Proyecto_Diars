using _03_Dominio;
using _04_Presistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Aplicacion
{
    public class appCliente
    {
        #region singleton
        private static readonly appCliente _instancia = new appCliente();
        public static appCliente Instancia
        {
            get { return appCliente._instancia; }
        }
        #endregion singleton

        #region metodos
        public Boolean InsertarCliente(entCliente cli)
        {
            try
            {
                return daoCliente.Instancia.InsertarCliente(cli);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion metodos
    }
}
