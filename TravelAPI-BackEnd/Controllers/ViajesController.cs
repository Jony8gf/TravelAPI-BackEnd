using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAPI_BackEnd.Entidades;
using TravelAPI_BackEnd.Utilidades;
using TravelAPI_BackEnd.ViewModels;

namespace TravelAPI_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class ViajesController : ControllerBase
    {
        private readonly ILogger<ViajesController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly string contenedor = "viajes";
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly UserManager<IdentityUser> userManager;

        public ViajesController(ILogger<ViajesController> logger,
           ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos,
            UserManager<IdentityUser> userManager)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
            this.userManager = userManager;
        }

        [HttpGet("filtrar")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ViajeViewModel>>> Filtrar([FromQuery] ViajesFiltrarViewModel viajesFiltrarVM)
        {
            var viajesQueryable = context.Viajes.AsQueryable();
            if (!string.IsNullOrEmpty(viajesFiltrarVM.Lugar))
            {
                viajesQueryable = viajesQueryable.Where(x => x.Lugar.Contains(viajesFiltrarVM.Lugar));
            }

            if (!string.IsNullOrEmpty(viajesFiltrarVM.Pais))
            {
                viajesQueryable = viajesQueryable.Where(x => x.Pais.Contains(viajesFiltrarVM.Pais));
            }

            if (viajesFiltrarVM.PromocionId != 0)
            {
                viajesQueryable = viajesQueryable
                    .Where(x => x.ViajePromociones.Select(y => y.PromocionId)
                    .Contains(viajesFiltrarVM.PromocionId));
            }

            if (viajesFiltrarVM.TipoActividadId != 0)
            {
                viajesQueryable = viajesQueryable
                    .Where(x => x.ViajeTipoActividades.Select(y => y.TipoActividadId)
                    .Contains(viajesFiltrarVM.TipoActividadId));
            }

            await HttpContext.InsertarParametrosPaginacionEnCabecera(viajesQueryable);
            var peliculas = await viajesQueryable.Paginar(viajesFiltrarVM.PaginacionView).ToListAsync();
            return mapper.Map<List<ViajeViewModel>>(peliculas);

        }


        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ViajeCreacionViewModel viajeCreacionVM)
        {
            var viaje = mapper.Map<Viaje>(viajeCreacionVM);

            if (viajeCreacionVM.Foto != null)
            {
                viaje.Foto = await almacenadorArchivos.GuardarArchivo(contenedor, viajeCreacionVM.Foto);
            }

            context.Add(viaje);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("PostGet")]
        public async Task<ActionResult<ViajesPostGetViewModel>> PostGet()
        {
            var tipoActividades = await context.TipoActividades.ToListAsync();
            var promociones = await context.Promociones.ToListAsync();

            var tipoActividadesVM = mapper.Map<List<TipoActividadViewModel>>(tipoActividades);
            var promocionesVM = mapper.Map<List<PromocionViewModel>>(promociones);

            return new ViajesPostGetViewModel() { TipoActividades = tipoActividadesVM, Promociones = promocionesVM };
        }

        [HttpGet("PutGet/{id:int}")]
        public async Task<ActionResult<ViajesPutGetViewModel>> PutGet(int id)
        {
            var viajeActionResult = await Get(id);
            if (viajeActionResult.Result is NotFoundResult) { return NotFound(); }

            var viaje = viajeActionResult.Value;

            var promocionesSeleccionadosIds = viaje.Promociones.Select(x => x.Id).ToList();
            var promocionesNoSeleccionados = await context.Promociones
                .Where(x => !promocionesSeleccionadosIds.Contains(x.Id))
                .ToListAsync();

            var tipoActividadSeleccionadosIds = viaje.TipoActividades.Select(x => x.Id).ToList();
            var tipoActividadNoSeleccionados = await context.TipoActividades
                .Where(x => !tipoActividadSeleccionadosIds.Contains(x.Id))
                .ToListAsync();

            var promocionesNoSeleccionadosVM = mapper.Map<List<PromocionViewModel>>(promocionesNoSeleccionados);
            var tipoActividadNoSeleccionadosVM = mapper.Map<List<TipoActividadViewModel>>(tipoActividadNoSeleccionados);

            var respuesta = new ViajesPutGetViewModel();
            respuesta.Viaje = viaje;
            respuesta.PromocionesSeleccionados = viaje.Promociones;
            respuesta.PromocionesNoSeleccionados = promocionesNoSeleccionadosVM;
            respuesta.TipoActividadesSeleccionados = viaje.TipoActividades;
            respuesta.TipoActividadesNoSeleccionados = tipoActividadNoSeleccionadosVM;

            return respuesta;
        }


        [HttpGet("{Id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<ViajeViewModel>> Get(int Id)
        {
            var viaje = await context.Viajes
                .Include(x => x.ViajePromociones).ThenInclude(x => x.Promocion)
                .Include(x => x.ViajeTipoActividades).ThenInclude(x => x.TipoActividad)
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (viaje == null)
                return NotFound();

            var vm = mapper.Map<ViajeViewModel>(viaje);
            return vm;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ViajeCreacionViewModel viajeCreacionViewModel)
        {
            var viaje = await context.Viajes
                .Include(x => x.ViajePromociones)
                .Include(x => x.ViajeTipoActividades)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (viaje == null)
                return NotFound();

            viaje = mapper.Map(viajeCreacionViewModel, viaje);

            if (viajeCreacionViewModel.Foto != null)
            {
                viaje.Foto = await almacenadorArchivos.EditarArchivo(contenedor, viajeCreacionViewModel.Foto, viaje.Foto);
            }

            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {

            var viaje = await context.Viajes.FirstOrDefaultAsync(x => x.Id == Id);
            if (viaje == null)
                return NotFound();

            context.Remove(viaje);
            await context.SaveChangesAsync();
            await almacenadorArchivos.BorrarArchivo(viaje.Foto, contenedor);
            return NoContent();

        }




    }
}
