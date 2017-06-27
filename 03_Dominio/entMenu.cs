using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Dominio
{
    public class entMenu
    {
        public int MenuID { get; set; }
        public entPedido Pedido { get; set; }
        public int Cantidad { get; set; }
        public entProducto Entrada { get; set; }
        public entProducto Segundo { get; set; }
        public entProducto Postre { get; set; }
        public decimal Precio { get; set; }
    }
}
