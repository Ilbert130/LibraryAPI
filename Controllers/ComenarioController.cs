using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebeVC.DTOs;
using PruebeVC.Models;

namespace PruebeVC.Controllers
{
    [ApiController]
    [Route("api/libros/{libroId:int}/comentario")] // api/autores => Route
    public class ComentarioController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ComentarioController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ComentarioDTO>>> Get(int libroId)
        {
            var exitLibro = await context.Libros.AnyAsync( l => l.Id == libroId);

            if(!exitLibro)
            {
                return NotFound("The libroid inserted doesn't exit");
            }

            var listComentario = await context.Comentarios.Where(c => c.LibroId == libroId).ToListAsync();
            return mapper.Map<List<ComentarioDTO>>(listComentario);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int libroId, ComentarioCreacionDTO comentarioCreacionDTO)
        {
            var exitLibro = await context.Libros.AnyAsync( l => l.Id == libroId);

            if(!exitLibro)
            {
                return NotFound("The libroid inserted doesn't exit");
            }

            var comentario = mapper.Map<Comentario>(comentarioCreacionDTO);
            comentario.LibroId = libroId;
            context.Comentarios.Add(comentario);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}