using _03_Dominio;
using _04_Presistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Aplicacion
{
    public class appPago
    {
        #region singleton
        private static readonly appPago _instancia = new appPago();
        public static appPago Instancia
        {
            get { return appPago._instancia; }
        }
        #endregion singleton

        #region metodos

        public entPago CalcularTotal(int idPedido, int idUsuario)
        {
            try
            {
                entPago pa = new entPago();
                entPedido p = appPedido.Instancia.DevolverPedido(idPedido);
                List<entMenu> m = appMenu.Instancia.DevolverMenusPedido(p.PedidoID);
                List<entDetallePedido> dtp = appDetallePedido.Instancia.DevolverProductosPedido(p.PedidoID);
                entTrabajador t = appTrabajador.Instancia.DevolverTrabajadorLogin(idUsuario);
                entPago px = new entPago();
                decimal subtotal = px.calcularSubTotal(m, dtp);
                pa.DescuentoPago = 0;
                pa.Pedido = p;
                pa.Trabajador = t;
                pa.SubtotalPago = subtotal;
                pa.TotalPago = px.calcularTotal(subtotal, pa.DescuentoPago);
                return pa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool InsertarPago(entPago p)
        {
            try
            {
                return daoPago.Instancia.InsertarPago(p);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion metodos
    }
}
