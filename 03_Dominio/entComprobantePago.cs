using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Dominio
{
    public class entComprobantePago
    {
        public int ComprobantePagoID { get; set; }
        public entPago Pago { get; set; }
        public int Numero { get; set; }
        public decimal TotalComprobante { get; set; }
        public string EstadoComprobante { get; set; }
    }
}
