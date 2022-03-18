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
    public class PromocionController : ControllerBase
    {
        private readonly ILogger<PromocionController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PromocionController(ILogger<PromocionController> logger,
            ApplicationDbContext context,
             IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpGet("listaPromociones")]
        public async Task<ActionResult<List<PromocionViewModel>>> Get([FromQuery] PaginacionViewModel paginacionVM)
        {
            var queryable = context.Promociones.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var promocions = await queryable.OrderBy(x => x.Nombre).Paginar(paginacionVM).ToListAsync();
            return mapper.Map<List<PromocionViewModel>>(promocions);
        }


        [HttpGet("todos")]
        [AllowAnonymous]
        public async Task<ActionResult<List<PromocionViewModel>>> Todos()
        {
            var promocions = await context.Promociones.OrderBy(x => x.Nombre).ToListAsync();
            return mapper.Map<List<PromocionViewModel>>(promocions);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PromocionCreacionViewModel promocionCreacionVM)
        {
            var promocion = mapper.Map<Promocion>(promocionCreacionVM);
            context.Add(promocion);
            var lineasAfectadas = await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int Id, [FromBody] PromocionCreacionViewModel promocionCreacionVM)
        {
            var promocion = await context.Promociones.FirstOrDefaultAsync(x => x.Id == Id);

            if (promocion == null)
                return NotFound();

            promocion = mapper.Map(promocionCreacionVM, promocion);
            await context.SaveChangesAsync();
            return NoContent();

        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var existe = await context.Promociones.AnyAsync(x => x.Id == Id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Promocion() { Id = Id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<PromocionViewModel>> Get(int Id)
        {
            var promocion = await context.Promociones.FirstOrDefaultAsync(x => x.Id == Id);

            if (promocion == null)
                return NotFound();

            return mapper.Map<PromocionViewModel>(promocion);
        }

    }
}
