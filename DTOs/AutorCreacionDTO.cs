using System.ComponentModel.DataAnnotations;
using PruebeVC.Validations;

namespace PruebeVC.DTOs
{
    public class AutorCreacionDTO
    {
        [Required]
        [StringLength(maximumLength:50)]
        [UpperFirstLetter(ErrorMessage = "La letra ingresada al principio es minuscula")]
        public string Nombre {get;set;}
    }
}