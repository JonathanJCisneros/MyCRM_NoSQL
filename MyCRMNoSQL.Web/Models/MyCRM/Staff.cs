#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using MyCRMNoSQL.CustomExtensions;

namespace MyCRMNoSQL.Models
{
    public class Staff
    {
        [Key]
        public string StaffId { get; set; }

        [Required(ErrorMessage = "is required")]
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

        public string BusinessId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public static Staff DbPrep(Staff s)
        {
            s.Position = MyExtensions.StringToUpper(s.Position);
            s.FirstName = MyExtensions.StringToUpper(s.FirstName);
            s.LastName = MyExtensions.StringToUpper(s.LastName);
            s.Email = s.Email.Trim().ToLower();

            return s;
        }
    }
}
