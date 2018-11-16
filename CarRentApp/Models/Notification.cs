using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace CarRentApp.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
       
        [Required,StringLength(100)]
        public string Status { get; set; }

        public string Details { get; set; }
        public DateTime NotificatinDateTime  { get; set; }

        [ForeignKey("RentRequest")]
        public int RentRequestId { get; set; }
        public RentRequest RentRequest { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}