using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarRentApp.Models
{
    public class RentAssign
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public double RentPrice { get; set; }

        [Required]
        [StringLength(100)]
        public string Status { get; set; }

        public DateTime RentAssignDateTime { get; set; }

        [ForeignKey("VehicleType")]
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }   

        [ForeignKey("RentRequest")]
        public int RentRequestId { get; set; }
        public RentRequest RentRequest { get; set; }
    }
}