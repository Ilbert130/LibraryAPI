using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PruebeVC.Validations;

namespace PruebeVC.Models
{
    public class Autor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:50)]
        [UpperFirstLetter(ErrorMessage = "La letra ingresada al principio es minuscula")]
        public string Nombre { get; set; }
        public List<AutorLibro> AutoresLibros {get;set;}

    }
}