#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace MyCRMNoSQL.Models
{
    public class ClientActivityFormModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string BusinessId { get; set; }

        [Required(ErrorMessage = "Please choose one of the following")]
        [Display(Name = "Action")]
        public string Type { get; set; }

        [MinLength(3, ErrorMessage = "Notes must be at least 3 characters long")]
        public string Note { get; set; } = "None";

        [Required]
        [Display(Name = "Spoke with")]
        public string StaffId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public static ClientActivityFormModel DbPrep(ClientActivityFormModel a)
        {
            a.Type = a.Type.Trim();
            a.Note = a.Note.Trim();

            return a;
        }
    }
}
