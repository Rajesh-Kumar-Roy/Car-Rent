using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRentApp.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Provide Your Name!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Provide Your Email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Provide Your Contact No!")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Please Provide your Address!")]
        public string Address { get; set; } 
    }
}