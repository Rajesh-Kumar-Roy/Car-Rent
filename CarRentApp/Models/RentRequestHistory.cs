using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarRentApp.Models
{
    public class RentRequestHistory
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }

        [Required]
        [StringLength(100)]
        public string Status { get; set; }
        public DateTime HistoryDateTime { get; set; }

        [ForeignKey("RentRequest")]
        public int RentRequestId { get; set; }
        public RentRequest RentRequest { get; set; }

    }
}