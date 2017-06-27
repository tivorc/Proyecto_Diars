using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Dominio
{
    public class entSubTipoProducto
    {
        [Display(Name = "Tipo Producto")]
        public int SubTipoProductoID { get; set; }
        public entTipoProducto TipoProducto { get; set; }

        [Display(Name = "Tipo Producto")]
        public string DescripcionSubTipo { get; set; }
    }
}
