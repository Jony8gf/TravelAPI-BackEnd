using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI_BackEnd.ViewModels
{
    public class TipoActividadCreacionViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(maximumLength: 15)]
        public string Nombre { get; set; }
    }
}
