using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Dominio
{
    public class entPersona
    {
        public int PersonaID { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellidos { get; set; }
        public string Dni { get; set; }

        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha De Nacimiento")]
        public DateTime FechaNacimiento { get; set; }
    }
}
