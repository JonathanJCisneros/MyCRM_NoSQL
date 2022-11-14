#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using MyCRMNoSQL.CustomValidations;

namespace MyCRMNoSQL.Models
{
    public class Business
    {
        [Key]
        public string BusinessId { get; set; }

        [Required(ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters long")]
        [Display(Name = "Business Name")]
        public string Name { get; set; }

        [RegularExpression(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$", ErrorMessage = "must be a valid website")]
        public string Website { get; set; } = "None";

        [PastDate(ErrorMessage = "must be in the past")]
        [Display(Name = "Date of Acquisition")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "is required")]
        public string Industry { get; set; }

        public string? PocId { get; set; }

        public Staff? PointOfContact { get; set; }

        public BusinessActivity? LatestActivity { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [Required]
        public string UserId { get; set; }

        public List<BusinessActivity> ActivityList { get; set; } = new List<BusinessActivity>();

        public List<Staff> StaffList { get; set; } = new List<Staff>();

        public List<Address> AddressList { get; set; } = new List<Address>();

        public List<Note> NoteList { get; set; } = new List<Note>();

        public List<Purchase> PurchaseList { get; set; } = new List<Purchase>();

        public List<UpcomingTask> TaskList { get; set; } = new List<UpcomingTask>();
    }
}

