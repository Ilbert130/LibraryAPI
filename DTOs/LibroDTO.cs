using PruebeVC.Models;

namespace PruebeVC.DTOs
{
    public class LibroDTO
    {
        public int Id { get; set; }
        public string Titulo {get; set;}
        //public List<ComentarioDTO> Comentarios {get;set;}
        public List<AutorDTO> Autores {get; set;}
    }
}