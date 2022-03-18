using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI_BackEnd.Entidades
{
    public class ViajePromocion
    {
        public int PromocionId { get; set; }
        public int ViajeId { get; set; }

        [Range(0.01, 99999.99)]
        public decimal PrecioConDescuento { get; set; }
        public Viaje Viaje { get; set; }
        public Promocion Promocion { get; set; }
    }
}
