#pragma warning disable CS8765
using System.ComponentModel.DataAnnotations;

namespace MyCRMNoSQL.CustomValidations
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.Now;
        }
    }

    public class PastDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d <= DateTime.Now;
        }
    }
}
