using AutoMapper;
using CarRentApp.Models;
using CarRentApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CarRentApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


           
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Customer, CustomerViewModel>();
                cfg.CreateMap<CustomerViewModel, Customer>(); 

                cfg.CreateMap<VehicleType, VehicleTypeViewModel>();
                cfg.CreateMap<VehicleTypeViewModel, VehicleType>();

                cfg.CreateMap<RentRequest, RentRequestViewModel>();
                cfg.CreateMap<RentRequestViewModel, RentRequest>();
                cfg.CreateMap<Notification, NotificationViewModel>();
                cfg.CreateMap<NotificationViewModel, Notification>();
            });
          
        }
    }
}
