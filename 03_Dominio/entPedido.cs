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
        public string TipoPedido { get; set; }
        public int Fecha { get; set; }
        public int EstadoPedido { get; set; }
    }
}
