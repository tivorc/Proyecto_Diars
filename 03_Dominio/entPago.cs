using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Dominio
{
    public class entPago
    {
        public int PagoID { get; set; }
        public entPedido Pedido { get; set; }
        public entTrabajador Trabajador { get; set; }
        public int FechaPago { get; set; }
        public decimal SubtotalPago { get; set; }
        public decimal DescuentoPago { get; set; }
        public decimal TotalPago { get; set; }

        public decimal calcularSubTotal(List<entMenu> m, List<entDetallePedido> dp)
        {
            decimal subTotal = 0;
            foreach (var item in m)
            {
                subTotal = subTotal + (item.Cantidad * item.Precio);
            }

            foreach (var item in dp)
            {
                subTotal = subTotal + (item.CantidadProducto * item.PrecioProducto);
            }

            return subTotal;
        }

        public decimal calcularTotal(decimal subTotal, decimal descuento)
        {
            return subTotal - descuento;
        }
    }
}
