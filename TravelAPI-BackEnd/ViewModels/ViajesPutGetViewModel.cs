using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI_BackEnd.ViewModels
{
    public class ViajesPutGetViewModel
    {
        public ViajeViewModel Pelicula { get; set; }
        public List<TipoActividadViewModel> TipoActividadesSeleccionados { get; set; }
        public List<TipoActividadViewModel> TipoActividadesNoSeleccionados { get; set; }
        public List<PromocionViewModel> PromocionesNoSeleccionados { get; set; }
        public List<PromocionViewModel> PromocionesSeleccionados { get; set; }
    }
}
