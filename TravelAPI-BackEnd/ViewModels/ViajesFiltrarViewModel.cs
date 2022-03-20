using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI_BackEnd.ViewModels
{
    public class ViajesFiltrarViewModel
    {
        public int Pagina { get; set; }
        public int RecordsPorPagina { get; set; }
        public PaginacionViewModel PaginacionView
        {
            get { return new PaginacionViewModel() { Pagina = Pagina, recordsPorPagina = RecordsPorPagina }; }
        }
        public string Lugar { get; set; }
        public string Pais { get; set; }
        public int TipoActividadId { get; set; }
        public int PromocionId { get; set; }

    }
}
