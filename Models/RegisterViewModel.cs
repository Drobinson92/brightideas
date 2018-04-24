using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Mvc;
using cbelt.Models;

namespace cbelt.Models{
    public class RegisterViewModel : BaseEntity{
        [Required(ErrorMessage="Name is required.")]
        [MinLength(2, ErrorMessage="Name too short.")]
        public string Name {get; set;}
        [Required(ErrorMessage="Alias is required.")]
        [MinLength(2, ErrorMessage="Alias too short.")]
        public string Alias {get; set;}
        [Required(ErrorMessage="Email is required")]
        [EmailAddress(ErrorMessage="Must match valid email format.")]
        [Remote("DoesExist", "Home", HttpMethod="POST", ErrorMessage="User already exists.")]
        public string Email {get; set;}
        [Required(ErrorMessage="Password is required.")]
        [MinLength(8, ErrorMessage="Passwords must have a minimum length of 8 characters.")]
        public string Password {get; set;}
        [Compare("Password", ErrorMessage="Passwords must match.")]
        [Display(Name="Confirm Password")]
        public string PasswordCheck {get; set;}
    }
}