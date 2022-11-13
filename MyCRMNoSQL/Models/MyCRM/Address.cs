#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace MyCRMNoSQL.Models
{
    public class Address
    {
        [Key]
        public string AddressId { get; set; }

        [MinLength(6, ErrorMessage = "must be valid")]
        [Display(Name = "Street")]
        public string? Street { get; set; }

        [Display(Name = "Apt/Suite")]
        public string? AptSuite { get; set; }

        [Required(ErrorMessage = "is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "is required")]
        [RegularExpression("^[0-9]{5}?$", ErrorMessage = "must be valid")]
        [Display(Name = "Zipcode")]
        public int ZipCode { get; set; }

        [Required]
        public string BusinessId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
