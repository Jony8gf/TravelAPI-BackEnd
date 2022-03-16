using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI_BackEnd.ViewModels
{
    public class PromocionViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }

        [Range(1, 100)]
        public int PorcentajeDescuento { get; set; }
    }
}
