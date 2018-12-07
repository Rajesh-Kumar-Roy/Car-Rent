using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CarRentApp.Models;

namespace CarRentApp.ViewModels
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public string Status { get; set; }

        [NotMapped]
        [Display(Name = "Replay Message")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string ReplayText { get; set; }
        public string Details { get; set; }
        public DateTime NotificatinDateTime { get; set; }

        public int RentRequestId { get; set; }
        public RentRequest RentRequest { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}