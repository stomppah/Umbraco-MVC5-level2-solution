﻿using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Umbraco.Course.Models
{
    public class RegisterModel 
    {

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        public string Biography { get; set; }

        public HttpPostedFileBase Avatar { get; set; }

    }


}