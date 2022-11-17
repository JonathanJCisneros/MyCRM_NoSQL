#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using MyCRMNoSQL.CustomValidations;

namespace MyCRMNoSQL.Models
{
    public class UpcomingTaskFormModel
    {
        public string UserId { get; set; }

        public string BusinessId { get; set; }

        [Required]
        [Display(Name = "With")]
        public string StaffId { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Details")]
        public string Details { get; set; } = "None";

        [Required]
        [FutureDate(ErrorMessage = "must be in the future")]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Required]
        public string Status { get; set; } = "Not Completed";

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public static UpcomingTaskFormModel DbPrep(UpcomingTaskFormModel t)
        {
            t.Details = t.Details.Trim();

            return t;
        }
    }
}
