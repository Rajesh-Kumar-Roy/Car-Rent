using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRentApp.ViewModels
{
    public class VehicleTypeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a vehicle name!")]
        public string Name { get; set; }
    }
}