using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Dominio
{
    public class entProducto
    {
        public int ProductoID { get; set; }
        public entSubTipoProducto SubTipoProducto { get; set; }

        [Display(Name = "Producto")]
        public string NombreProducto { get; set; }

        [Display(Name = "Descripción")]
        public string DescripcionProducto { get; set; }

        [Display(Name = "Precio")]
        public decimal PrecioProducto { get; set; }
        public int Stock { get; set; }

        [Display(Name = "Imagen")]
        public string ImgProducto { get; set; }
    }
}
