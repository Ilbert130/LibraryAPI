using System.ComponentModel.DataAnnotations;
using PruebeVC.Validations;

namespace PruebeVC.DTOs
{
    public class LibroCreacionDTO
    {
        [UpperFirstLetter]
        [StringLength(maximumLength:250)]
        public string Titulo {get; set;}
        public List<int> AutoresIds {get;set;}
    }
}