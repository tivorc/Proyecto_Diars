using _03_Dominio;
using _04_Presistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Aplicacion
{
    public class appTipoPago
    {
        #region singleton
        private static readonly appTipoPago _instancia = new appTipoPago();
        public static appTipoPago Instancia
        {
            get { return appTipoPago._instancia; }
        }
        #endregion singleton

        #region metodos

        public List<entTipoPago> ListarTipoPago()
        {
            try
            {
                return daoTipoPago.Instancia.ListarTipoPago();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #endregion metodos
    }
}
