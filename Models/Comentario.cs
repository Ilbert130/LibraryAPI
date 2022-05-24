using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PruebeVC.Validations;

namespace PruebeVC.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Contenido {get; set;}
        public int LibroId {get; set;}
        public Libro Libro {get; set;}
    }
}