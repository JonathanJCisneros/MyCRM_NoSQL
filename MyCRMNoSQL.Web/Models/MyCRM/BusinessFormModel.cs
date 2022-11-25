#pragma warning disable CS8618
#pragma warning disable CS8602
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using MyCRMNoSQL.CustomExtensions;
using MyCRMNoSQL.CustomValidations;

namespace MyCRMNoSQL.Models
{
    public class BusinessFormModel
    {
        [Required(ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters long")]
        [Display(Name = "Business Name")]
        public string Name { get; set; }

        [RegularExpression(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$", ErrorMessage = "must be a valid website")]
        public string Website { get; set; } = "None";

        [PastDate(ErrorMessage = "must be in the past")]
        [Display(Name = "Date of Acquisition")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "is required")]
        public string Industry { get; set; }

        public string? PocId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [Required]
        public string UserId { get; set; }

        public static BusinessFormModel DbPrep(BusinessFormModel b)
        {
            b.Name = MyExtensions.StringToUpper(b.Name);
            b.Industry = MyExtensions.StringToUpper(b.Industry);
            b.Website = b.Website.Trim();
            b.PocId = b.PocId.Trim();
            b.UserId = b.UserId.Trim();

            return b;
        }
    }
}

