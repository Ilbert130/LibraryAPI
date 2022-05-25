using System.ComponentModel.DataAnnotations;
using PruebeVC.Validations;

namespace PruebeVC.Models
{
    public class Libro
    {
        public int Id { get; set; }
        [UpperFirstLetter]
        [StringLength(maximumLength:250)]
        public string Titulo {get; set;}
        public List<Comentario> Comentarios {get;set;}
        public List<AutorLibro> AutoresLibros {get;set;}
    }
}