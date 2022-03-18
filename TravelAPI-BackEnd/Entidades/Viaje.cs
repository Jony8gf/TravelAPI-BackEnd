using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI_BackEnd.Entidades
{
    public class Viaje
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 20)]
        public string Pais { get; set; }

        [StringLength(maximumLength: 50)]
        public string Lugar { get; set; }

        [StringLength(maximumLength: 250)]
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        public Point Ubicacion { get; set; }

        [Required]
        [Range(0.01 , 99999.99)]
        public decimal Precio { get; set; }

        public List<ViajeTipoActividad> ViajeTipoActividades { get; set; }

        public List<ViajePromocion> ViajePromociones { get; set; }
    }
}
