using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Dominio
{
    public class entDetallePedido
    {
        public int DetallePedidoID { get; set; }
        public entPedido Pedido { get; set; }
        public entProducto Producto { get; set; }
        public int CantidadProducto { get; set; }
        public decimal PrecioProducto { get; set; }
    }
}
