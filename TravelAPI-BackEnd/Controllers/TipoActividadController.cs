using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    public class TipoActividadController : ControllerBase
    {
        private readonly ILogger<TipoActividadController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TipoActividadController(ILogger<TipoActividadController> logger,
            ApplicationDbContext context,
             IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpGet("list")]
        public async Task<ActionResult<List<TipoActividadViewModel>>> Get([FromQuery] PaginacionViewModel paginacionVM)
        {
            var queryable = context.TipoActividad.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var tipoActividads = await queryable.OrderBy(x => x.Nombre).Paginar(paginacionVM).ToListAsync();
            return mapper.Map<List<TipoActividadViewModel>>(tipoActividads);
        }


        [HttpGet("todos")]
        [AllowAnonymous]
        public async Task<ActionResult<List<TipoActividadViewModel>>> Todos()
        {
            var tipoActividads = await context.TipoActividad.OrderBy(x => x.Nombre).ToListAsync();
            return mapper.Map<List<TipoActividadViewModel>>(tipoActividads);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoActividadCreacionViewModel tipoActividadCreacionVM)
        {
            var tipoActividad = mapper.Map<TipoActividad>(tipoActividadCreacionVM);
            context.Add(tipoActividad);
            var lineasAfectadas = await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int Id, [FromBody] TipoActividadCreacionViewModel tipoActividadCreacionVM)
        {
            var tipoActividad = await context.TipoActividad.FirstOrDefaultAsync(x => x.Id == Id);

            if (tipoActividad == null)
                return NotFound();

            tipoActividad = mapper.Map(tipoActividadCreacionVM, tipoActividad);
            await context.SaveChangesAsync();
            return NoContent();

        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var existe = await context.TipoActividad.AnyAsync(x => x.Id == Id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new TipoActividad() { Id = Id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<TipoActividadViewModel>> Get(int Id)
        {
            var tipoActividad = await context.TipoActividad.FirstOrDefaultAsync(x => x.Id == Id);

            if (tipoActividad == null)
                return NotFound();

            return mapper.Map<TipoActividadViewModel>(tipoActividad);
        }



    }
}
