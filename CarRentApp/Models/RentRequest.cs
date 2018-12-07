using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarRentApp.Models
{
    public class RentRequest
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(500)]
        public string FromPlace { get; set; }

        [Required,StringLength(500)]
        public string ToPlace { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        [Required]
        public string AirCondition { get; set; }
       
        [Required]
        public int VehicleQty { get; set; }
        public string Description { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId{ get; set; }
        public Customer Customer { get; set; }
      
        [ForeignKey("VehicleType")]
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }
        public bool IsDelete { get; set; }

        public List<RentAssign> RentAssigns { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<RentRequestHistory> RentRequestHistories { get; set; }


    }
}