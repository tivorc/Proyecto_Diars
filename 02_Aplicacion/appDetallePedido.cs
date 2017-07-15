using _03_Dominio;
using _04_Presistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Aplicacion
{
    public class appDetallePedido
    {
        #region singleton
        private static readonly appDetallePedido _instancia = new appDetallePedido();
        public static appDetallePedido Instancia
        {
            get { return appDetallePedido._instancia; }
        }
        #endregion singleton

        #region metodos

        public List<entDetallePedido> DevolverProductosPedido(int pedidoID)
        {
            try
            {
                return daoDetallePedido.Instancia.DevolverProductosPedido(pedidoID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion metodos
    }
}
