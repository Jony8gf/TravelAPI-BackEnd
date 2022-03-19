using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI_BackEnd.Utilidades;

namespace TravelAPI_BackEnd.ViewModels
{
    public class ViajeCreacionViewModel
    {
        [StringLength(maximumLength: 20)]
        public string Pais { get; set; }

        [StringLength(maximumLength: 50)]
        public string Lugar { get; set; }

        [StringLength(maximumLength: 250)]
        public string Descripcion { get; set; }
        public IFormFile Foto { get; set; }

        [Range(-90, 90)]
        public double Latitud { get; set; }
        [Range(-180, 180)]
        public double Longitud { get; set; }

        [Required]
        [Range(0.01, 99999.99)]
        public decimal Precio { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> TipoActividadesIds { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> PromocionesIds { get; set; }
    }
}
