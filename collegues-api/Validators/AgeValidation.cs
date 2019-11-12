using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace collegues_api.Validators
{
    public class AgeValidation
    {
        public static ValidationResult AgeValidate(DateTime date)
        {
            try
            {
                TimeSpan span = DateTime.Now - date;
                var years = (new DateTime(1, 1, 1) + span).Year - 1;
                return years >= 18 ? ValidationResult.Success : new ValidationResult("Age incorrect");
            }
            catch(Exception)
            {
                return new ValidationResult("Une erreur liée à l'âge est survenue");
            }
        }
    }
}
