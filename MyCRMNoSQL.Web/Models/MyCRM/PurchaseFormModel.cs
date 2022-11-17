﻿#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace MyCRMNoSQL.Models
{
    public class PurchaseFormModel
    {
        public string BusinessId { get; set; }

        [Required(ErrorMessage = "is required")]
        [Display(Name = "Product")]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "is required")]
        [Display(Name = "Business Location")]
        public string AddressId { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
