using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRentApp.Models
{
    public class VehicleType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsDelete { get; set; }

        public List<RentRequest> RentRequests { get; set; }
        public List<RentAssign> RentAssigns { get; set; }
    }
}