#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace MyCRMNoSQL.Models
{
    public class NoteFormModel
    {
        [Required(ErrorMessage = "Notes are required")]
        [MinLength(5, ErrorMessage = "Notes must be at least 5 characters long")]
        public string Details { get; set; }

        [Required]
        public string BusinessId { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public static NoteFormModel DbPrep(NoteFormModel n)
        {
            n.Details = n.Details.Trim();

            return n;
        }
    }
}
