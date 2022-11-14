﻿#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using MyCRMNoSQL.CustomExtensions;

namespace MyCRMNoSQL.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "is required")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public static UserLogin DbPrep(UserLogin u)
        {
            u.Email = u.Email.Trim().ToLower();
            u.Password = u.Password.Trim();

            return u;
        }
    }
}
