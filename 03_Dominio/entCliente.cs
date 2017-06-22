using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Dominio
{
    public class entCliente
    {
        public int ClienteID { get; set; }
        public entPersona Persona { get; set; }
        public entUsuario Usuario { get; set; }
    }
}
