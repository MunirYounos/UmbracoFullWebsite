using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace AWebDev.ViewModels
{
    public class ContactForm
    {
        [Required(ErrorMessage ="Please enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please insert a valid Email Address")]
        [Display(Name ="Email")]
        [RegularExpression(@"^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,6})+$", 
         ErrorMessage ="Please insert a correct email address")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please enter a subject")]
        public string Subject { get; set; }
        [Required(ErrorMessage ="Please write your message")]
        public string Message { get; set; }
    }
}