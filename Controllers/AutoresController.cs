using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebeVC.Models;
using PruebeVC.Servicios;

namespace PruebeVC.Controllers
{
    [ApiController]
    [Route("api/autores")] // api/autores => Route
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IServicio servicio;
        private readonly ServicioTransient servicioTransient;
        private readonly ServicioScoped servicioScoped;
        private readonly ServicioSingleton servicioSingleton;
        private readonly ILogger<AutoresController> logger;

        public AutoresController(ApplicationDbContext context, IServicio servicio, ServicioTransient servicioTransient,
                                 ServicioScoped servicioScoped, ServicioSingleton servicioSingleton, ILogger<AutoresController> logger)
        {
            this.logger = logger;
            this.context = context;
            this.servicio = servicio;
            this.servicioTransient = servicioTransient;
            this.servicioScoped = servicioScoped;
            this.servicioSingleton = servicioSingleton;
        }

        [HttpGet("Guid")]
        public ActionResult ObtenerGuids()
        {
            logger.LogInformation("Estamos obteniendo los GUIDS");
            return Ok(new
            {
                AutoresControllerTransient = servicioTransient.Guid,
                ServcioA_Transiente = servicio.ObtenerTransient(),
                AutoresControllerScoped = servicioScoped.Guid,
                ServcioA_Scoped = servicio.ObtenerScoped(),
                AutoresControllerSingleton = servicioSingleton.Guid,
                ServcioA_Singleton = servicio.ObtenerSingleton()
            });
        }

        [HttpGet("{id:int}/{name?}")] // api/autores/id => Route
        public async Task<ActionResult<Autor>> Get(int id, string name)
        {
            return await context.Autores.Include(a => a.Libros).FirstOrDefaultAsync(a => a.Id == id || a.Nombre.Contains(name));
        }

        [HttpGet]
        [HttpGet("listado")] // api/autores/listado => Route
        [HttpGet("/listado")] // listtado => Route
        public async Task<ActionResult<List<Autor>>> Get()
        {
            var listAutor = await context.Autores.Include(a => a.Libros).ToListAsync();
            return listAutor;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            var exiteAutorConElMismoNombre = await context.Autores.AnyAsync(a => a.Nombre == autor.Nombre);

            if (exiteAutorConElMismoNombre)
            {
                return BadRequest("Ya exite el registro a crear");
            }

            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Autor autor)
        {
            var exist = await context.Autores.AnyAsync(a => a.Id == autor.Id);

            if (!exist)
            {
                return NotFound("The register inserted not exist");
            }

            context.Autores.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.Autores.AnyAsync(a => a.Id == id);

            if (!exist)
            {
                return NotFound("The register inserted not exist");
            }

            context.Autores.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}