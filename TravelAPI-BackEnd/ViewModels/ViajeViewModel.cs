using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI_BackEnd.Entidades;

namespace TravelAPI_BackEnd.ViewModels
{
    public class ViajeViewModel
    {
        public int Id { get; set; }
        public string Pais { get; set; }
        public string Lugar { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        [Range(0.01, 99999.99)]
        public decimal Precio { get; set; }

        public List<ViajeTipoActividad> TipoActividades { get; set; }

        public List<ViajePromocion> Promociones { get; set; }
    }
}
