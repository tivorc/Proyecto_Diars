using _03_Dominio;
using _04_Presistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Aplicacion
{
    public class appPedido
    {
        #region singleton
        private static readonly appPedido _instancia = new appPedido();
        public static appPedido Instancia
        {
            get { return appPedido._instancia; }
        }
        #endregion singleton

        #region metodos

        public int InsertarPedido(entPedido p)
        {
            try
            {
                return daoPedido.Instancia.InsertarPedido(p);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<entPedido> ListaPedidos(string tipoPedido, string estado)
        {
            try
            {
                return daoPedido.Instancia.ListaPedidos(tipoPedido, estado);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion metodos
    }
}
