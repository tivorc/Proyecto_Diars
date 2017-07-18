using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Dominio
{
    public class entPedido
    {
        public int PedidoID { get; set; }
        public entCliente Cliente { get; set; }
        public entTipoPago TipoPago { get; set; }
        public entMesa Mesa { get; set; }
        public string TipoPedido { get; set; }
        public DateTime Fecha { get; set; }
        public string EstadoPedido { get; set; }
    }
}
