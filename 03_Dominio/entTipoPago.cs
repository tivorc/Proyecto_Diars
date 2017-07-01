using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Dominio
{
    public class entTipoPago
    {
        public int TipoPagoID { get; set; }
        [Display(Name ="Tipo Pago")]
        public string DescripcionTipoPago { get; set; }
    }
}
