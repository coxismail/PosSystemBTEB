using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PosSystem
{
    public partial class ModelValidator
    {
        public static List<ValidationResult> Results { get; set; }
        public static bool IsValid(Object T)
        {
            var context = new ValidationContext(T, serviceProvider: null, items: null);
            Results = new List<ValidationResult>();
            return Validator.TryValidateObject(T, context, Results, true);
        }
    }
}