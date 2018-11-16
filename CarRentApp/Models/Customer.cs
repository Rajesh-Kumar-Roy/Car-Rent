using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRentApp.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(250)]
        public string Name { get; set; }

        [Required,StringLength(250)]
        public string Email { get; set; }

        [Required,StringLength(14)]
        public string ContactNo { get; set; }

        [Required,StringLength(500)]
        public string Address { get; set; } 
    }
}