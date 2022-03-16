using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace TravelAPI_BackEnd.Entidades
{
    public class Promocion
    {
        public int Id { get; set; }

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
