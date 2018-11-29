using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CarRentApp.Models;

namespace CarRentApp.Context
{
    public class RentDbContext : DbContext
    {
        public RentDbContext() :base("DefaultConnection")
        {
            //
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<RentRequest> RentRequests { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<RentRequestHistory> RentRequestHistorys { get; set; }
        public DbSet<RentAssign> RentAssigns { get; set; }
    }

    

}