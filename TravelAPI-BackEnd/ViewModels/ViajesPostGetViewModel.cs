using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI_BackEnd.ViewModels
{
    public class ViajesPostGetViewModel
    {
        public List<TipoActividadViewModel> TipoActividades { get; set; }
        public List<PromocionViewModel> Promociones { get; set; }
    }
}
