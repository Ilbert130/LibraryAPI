using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PruebeVC.Validations;

namespace PruebeVC.Models
{
    public class Autor : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [UpperFirstLetterAttribute(ErrorMessage = "La letra ingresada al principio es minuscula")]
        [StringLength(maximumLength: 5, ErrorMessage = "El string introducido es mayor")]
        public string Nombre { get; set; }
        [Range(18, 120)]
        [NotMapped] //para que no mappe
        public int Edad { get; set; }
        [CreditCard]
        [NotMapped]
        public string CrediCard { get; set; }
        public int Menor { get; set; }
        public int Mayor { get; set; }
        public List<Libro> Libros { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayuscula", new string[] { nameof(Nombre) });
                }
            }

            if(Menor > Mayor)
            {
                yield return new ValidationResult("Este valor no puede ser mas grande que el campo mayor", new string[] {nameof(Menor)});
            }
        }
    }
}