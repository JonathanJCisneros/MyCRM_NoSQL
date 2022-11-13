#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace MyCRMNoSQL.Models
{
    public class NewBusinessForm
    {
        [Required(ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters long")]
        [Display(Name = "Business Name")]
        public string Name { get; set; }

        [RegularExpression(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$", ErrorMessage = "must be a valid website")]
        public string? Website { get; set; }

        [Display(Name = "Date of Acquisition")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "is required")]
        public string Industry { get; set; }

        [Required(ErrorMessage = "required")]
        [Display(Name = "Position")]
        public string Position { get; set; }

        [Required(ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters long")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters long")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "is required")]
        [Display(Name = "Phone Number")]
        public long PhoneNumber { get; set; }

        [Required(ErrorMessage = "is required")]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "must be valid")]
        [Display(Name = "Street")]
        public string? Street { get; set; }

        [Display(Name = "Apt/Suite")]
        public string? AptSuite { get; set; } 

        [Required(ErrorMessage = "is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "Must be at least 2 characters long")]
        public string State { get; set; }

        [Required(ErrorMessage = "is required")]
        [RegularExpression("^[0-9]{5}?$", ErrorMessage = "must be valid")]
        [Display(Name = "Zipcode")]
        public int ZipCode { get; set; }
    }
}
