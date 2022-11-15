#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using MyCRMNoSQL.CustomExtensions;

namespace MyCRMNoSQL.Models
{
    public class Product
    {
        [Key]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters long")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "is required")]
        [Range(0.0, double.MaxValue, ErrorMessage = "must be greater than $0.00")]
        [Display(Name = "Price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "of product is required")]
        [MinLength(8, ErrorMessage = "of product must be at least 8 characters long")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public List<Purchase> ClientList { get; set; } = new List<Purchase>();

        public static Product DbPrep(Product p)
        {
            p.Name = MyExtensions.StringToUpper(p.Name);
            p.Description = p.Description.Trim();

            return p;
        }
    }
}
