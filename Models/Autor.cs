using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PruebeVC.Validations;

namespace PruebeVC.Models
{
    public class Autor
    {
        public int Id { get; set; }

        [Required]
        [UpperFirstLetter(ErrorMessage = "La letra ingresada al principio es minuscula")]
        public string Nombre { get; set; }

    }
}