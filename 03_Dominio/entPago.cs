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
        public int SubtotalPago { get; set; }
        public int DescuentoPago { get; set; }
        public int TotalPago { get; set; }
    }
}
