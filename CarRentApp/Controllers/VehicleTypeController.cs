using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CarRentApp.Models;
using CarRentApp.Context;
using CarRentApp.ViewModels;

namespace CarRentApp.Controllers
{
    public class VehicleTypeController : Controller
    {
        private RentDbContext db = new RentDbContext();

        // GET: /VehicleType/
        public ActionResult Index()
        {   
            
            List<VehicleType> vehicle = db.VehicleTypes.Where(c=>c.IsDelete==false).ToList();
            List<VehicleTypeViewModel> vehicleTypeViewModels = Mapper.Map<List<VehicleTypeViewModel>>(vehicle);
           

            //foreach (var vcle in vehicle)
           // {
             //   VehicleTypeViewModel vmrTypeViewModel = new VehicleTypeViewModel();
              //  vmrTypeViewModel.Id = vcle.Id;
              //  vmrTypeViewModel.Name = vcle.Name;
               // vehicleTypeViewModels.Add(vmrTypeViewModel);
           // }
            return View(vehicleTypeViewModels);
        }

        // GET: /VehicleType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleType vehicletype = db.VehicleTypes.Find(id);
            if (vehicletype == null)
            {
                return HttpNotFound();
            }

            VehicleTypeViewModel vehicleTypeViewModel = Mapper.Map<VehicleTypeViewModel>(vehicletype);

            return View(vehicleTypeViewModel);
        }

        // GET: /VehicleType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /VehicleType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] VehicleTypeViewModel vehicleTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                VehicleType vehicleType = Mapper.Map<VehicleType>(vehicleTypeViewModel);

                db.VehicleTypes.Add(vehicleType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicleTypeViewModel);
        }

        // GET: /VehicleType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleType vehicletype = db.VehicleTypes.Find(id);

            if (vehicletype == null)
            {
                return HttpNotFound();
            }
            VehicleTypeViewModel vehicleTypeViewModel = Mapper.Map<VehicleTypeViewModel>(vehicletype);

            return View(vehicleTypeViewModel);
        }

        // POST: /VehicleType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] VehicleTypeViewModel vehicleTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                VehicleType vehicleType = Mapper.Map<VehicleType>(vehicleTypeViewModel);
                db.Entry(vehicleType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicleTypeViewModel);
        }

        // GET: /VehicleType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleType vehicletype = db.VehicleTypes.Find(id);
            if (vehicletype == null)
            {
                return HttpNotFound();
            }

            VehicleTypeViewModel vehicleTypeViewModel = Mapper.Map<VehicleTypeViewModel>(vehicletype);



            return View(vehicleTypeViewModel);
        }

        // POST: /VehicleType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleType vehicletype = db.VehicleTypes.Find(id);
            if (vehicletype != null)
            {
                vehicletype.IsDelete = true;
                db.Entry(vehicletype).State = EntityState.Modified;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
