using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Dominio
{
    public class entDetalleMenu
    {
        public int DetalleMenuID { get; set; }
        public entMenu Menu { get; set; }
        public entProducto Producto { get; set; }
    }
}
