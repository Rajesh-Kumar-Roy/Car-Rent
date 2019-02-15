using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRentApp.Views.Report
{
    public class RentAssignedReportVm
    {
        public int Id { get; set; }
        public DateTime RentAssignDateTime { get; set; }
        public int RentRequestId { get; set; }
        public int VehicleTypeId { get; set; }  
    }
}