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

        [Required, StringLength(100)]
        public string Status { get; set; }

        [Display(Name = "Replay Message"),DataType(DataType.MultilineText)]
        public string Details { get; set; }
        public DateTime NotificatinDateTime { get; set; }

        public int RentRequestId { get; set; }
        public RentRequest RentRequest { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}