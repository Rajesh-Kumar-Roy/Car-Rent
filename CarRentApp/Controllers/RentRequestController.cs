using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarRentApp.Models;
using CarRentApp.Context;
using CarRentApp.ViewModels;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace CarRentApp.Controllers
{
    [Authorize(Roles = "Controller,AppAdmin,Customer")]
    public class RentRequestController : Controller
    {
        private RentDbContext db = new RentDbContext();

            
        // GET: /RentRequest/
        public ActionResult Index()
        {
            List<RentRequestViewModel> rentrequestViewModel = new List<RentRequestViewModel>();
            if (User.IsInRole("AppAdmin") || User.IsInRole("Controller"))
            {
                var rentrequests = db.RentRequests.Include(r => r.Customer).Include(r => r.VehicleType).Where(c => c.IsDelete == false).ToList();
                rentrequestViewModel = Mapper.Map<List<RentRequestViewModel>>(rentrequests);
            }
            if (User.IsInRole("Customer"))
            {
                var userId = User.Identity.GetUserId();
                var user = db.Customers.FirstOrDefault(c => c.UserId == userId);
                if (user!=null)
                {
                    var rentrequests = db.RentRequests.Include(r => r.Customer).Where(x=>x.Id==user.Id).Include(r => r.VehicleType).Where(c => c.IsDelete == false).ToList();
                    rentrequestViewModel = Mapper.Map<List<RentRequestViewModel>>(rentrequests);
                }
                
            }
            return View(rentrequestViewModel);



        }

        // GET: /RentRequest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentRequest rentrequest = db.RentRequests.Include(c=>c.VehicleType).Include(c=>c.Customer).SingleOrDefault(d=>d.Id==id);
            if (rentrequest == null)
            {
                return HttpNotFound();
            }
            RentRequestViewModel rentrequestViewMOdel = Mapper.Map<RentRequestViewModel>(rentrequest);
            return View(rentrequestViewMOdel);
        }

        // GET: /RentRequest/Create
        public ActionResult Create()
        {
            //ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Name");
            return View();
        }

        // POST: /RentRequest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,FromPlace,ToPlace,StartDateTime,EndDateTime,AirCondition,VehicleQty,Description,CustomerId,VehicleTypeId")] RentRequestViewModel rentRequestViewModel)
        {
            if (ModelState.IsValid)
            {
                var loginCustomerId = User.Identity.GetUserId();
                var customer = db.Customers.FirstOrDefault(c => c.UserId == loginCustomerId);
                
                if (customer == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RentRequest rentRequest = Mapper.Map<RentRequest>(rentRequestViewModel);
                rentRequest.CustomerId = customer.Id;
                db.RentRequests.Add(rentRequest);
                var isSave=db.SaveChanges()>0;

                if (isSave)
                {
                    Notification notification=new Notification();
                    notification.Status = "New";
                    notification.Details = "New rent request ";
                    notification.NotificatinDateTime=DateTime.Now;
                    notification.CustomerId = rentRequest.CustomerId;
                    notification.RentRequestId = rentRequest.Id;
                    db.Notifications.Add(notification);
                    db.SaveChanges();

                    RentRequestHistory rentRequestHistory =new RentRequestHistory();
                    rentRequestHistory.Status = "New";
                    rentRequestHistory.Text = "New rent request";
                    rentRequestHistory.HistoryDateTime=DateTime.Now;
                    rentRequestHistory.RentRequestId = rentRequest.Id;
                    db.RentRequestHistorys.Add(rentRequestHistory);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", rentRequestViewModel.CustomerId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Name", rentRequestViewModel.VehicleTypeId);
            return View(rentRequestViewModel);
        }

        // GET: /RentRequest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentRequest rentrequest = db.RentRequests.Find(id);
            if (rentrequest == null)
            {
                return HttpNotFound();
            }

            RentRequestViewModel rentRequestViewModel = Mapper.Map<RentRequestViewModel>(rentrequest);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", rentrequest.CustomerId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Name", rentrequest.VehicleTypeId);
            return View(rentRequestViewModel);
        }

        // POST: /RentRequest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,FromPlace,ToPlace,StartDateTime,EndDateTime,AirCondition,VehicleQty,Description,CustomerId,VehicleTypeId")] RentRequestViewModel rentrequestviewModel)
        {
            if (ModelState.IsValid)
            {
                RentRequest rentrequest = Mapper.Map<RentRequest>(rentrequestviewModel);
                db.Entry(rentrequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", rentrequestviewModel.CustomerId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Name", rentrequestviewModel.VehicleTypeId);
            return View(rentrequestviewModel);
        }

        // GET: /RentRequest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentRequest rentrequest = db.RentRequests.Include(c => c.VehicleType).Include(c => c.Customer).SingleOrDefault(d => d.Id == id);
            if (rentrequest == null)
            {
                return HttpNotFound();
            }

            RentRequestViewModel rentRequestviewModel = Mapper.Map<RentRequestViewModel>(rentrequest);
            return View(rentRequestviewModel);
        }

        // POST: /RentRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentRequest rentrequest = db.RentRequests.Find(id);
            if (rentrequest!=null)
            {
                rentrequest.IsDelete = true;
                db.Entry(rentrequest).State=EntityState.Modified;
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
