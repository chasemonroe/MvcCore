﻿using Microsoft.AspNetCore.Http;
using SocialHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialHub.ViewModels
{
    public class UserCreateViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [MaxLength(30, ErrorMessage = "Name cannot exceed 30 characters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(30, ErrorMessage = "Name cannot exceed 30 characters")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        public IFormFile Photo { get; set; }

    }
}
