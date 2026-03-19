using System.ComponentModel.DataAnnotations;

namespace Backend.Validaciones
{
    public class PrimeraLetraMayusculaAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            //este método no verifica si es un campo vacio si no que verifica las primeras letras mayúsculas
            if (value == null || string.IsNullOrWhiteSpace(value.ToString())) 
            {
                return ValidationResult.Success;
            }

            var primeraletra = value.ToString()[0].ToString();
            if (primeraletra != primeraletra.ToUpper())
            {
                return new ValidationResult("La primera letra debe ser mayúscula");
            }

            return ValidationResult.Success;
        }
    }
}
