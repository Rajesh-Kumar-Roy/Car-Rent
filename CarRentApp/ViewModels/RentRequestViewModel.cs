using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CarRentApp.Models;

namespace CarRentApp.ViewModels
{
    public class RentRequestViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Provide  Journey Star Place!")]
        [Display(Name = "From Place")]
        public string FromPlace { get; set; }

        [Required(ErrorMessage = "Please Provide Your destination Place!")]
        [Display(Name = "Destination Place")]
        public string ToPlace { get; set; }
        [Display(Name = "Start Date Time")]
        public DateTime StartDateTime { get; set; }
        [Display(Name = "End Date Time")]
        public DateTime EndDateTime { get; set; }

        [Required (ErrorMessage = "Please Select  AC OR NON-AC")]
        [Display(Name = "Air Condition")]
        public string AirCondition { get; set; }

        [Required(ErrorMessage = "Please Input Vehicle Quantity!")]
        [Display(Name = "Vehicle Quantity")]
        public int VehicleQty { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

       [Display(Name = "Vehicle Type")]
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }



    }
}