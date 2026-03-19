using System.ComponentModel.DataAnnotations;

namespace Backend.Validaciones
{
    public class ValidacionesNit : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            //este método no verifica si es un campo vacio si no que verifica las primeras letras mayúsculas
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            string? Nit = value.ToString();

            if (Nit == "CF" || Nit == "cf")
            {
                if (Nit != Nit.ToUpper())
                {
                    return new ValidationResult("Nit como consumidor final debe de ser CF en mayúsculas");
                }
                return ValidationResult.Success;
            }
            if (Nit.ToString().Contains("-"))
            {
                return new ValidationResult("Nit debe de ir sin guión");
            }
            if(Nit.ToString().Contains("K") || Nit.ToString().Contains("k"))
            {
                if (Nit != Nit.ToUpper())
                {
                    return new ValidationResult("Nit con letra K debe de ir mayúscula");
                }

                return ValidationResult.Success;
            }
            
            return ValidationResult.Success;
        }
    }
}
