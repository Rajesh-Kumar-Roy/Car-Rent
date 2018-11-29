using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CarRentApp.Models;

namespace CarRentApp.ViewModels
{
    public class RentAssignViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Rent Price")]
        public double RentPrice { get; set; }

        public string Status { get; set; }

        public DateTime RentAssignDateTime { get; set; }

        [Display(Name = "Vehicle Type")]
        [Required]
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; } 

        [Display(Name = "Request Id")]
        [Required]
        public int RentRequestId { get; set; }
        public RentRequest RentRequest { get; set; }
    }
}