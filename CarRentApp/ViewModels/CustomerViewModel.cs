using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace CarRentApp.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Provide Your Name!")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Please provide a correct email address!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Provide Your Contact No!")]
        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Please Provide your Address!")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; } 
    }
}