using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI_BackEnd.ViewModels
{
    public class PromocionCreacionViewModel
    {
        [Required]
        [StringLength(maximumLength: 300)]
        public string Nombre { get; set; }

        [Required]
        public DateTime FechaDesde { get; set; }

        [Required]
        public DateTime FechaHasta { get; set; }

        [Required]
        [Range(1, 100)]
        public int PorcentajeDescuento { get; set; }
    }
}
