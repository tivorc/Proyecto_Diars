using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Dominio
{
    public class entTipoProducto
    {
        public int TipoProductoID { get; set; }

        [Display(Name = "Producto")]
        public string DescripcionTipoProducto { get; set; }
    }
}
