using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI_BackEnd.Entidades
{
    public class ViajeTipoActividad
    {
        public int TipoActividadId { get; set; }
        public int ViajeId { get; set; }
        public Viaje Viaje { get; set; }
        public TipoActividad TipoActividad { get; set; }
    }
}
