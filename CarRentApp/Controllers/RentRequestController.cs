﻿using System;
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

namespace CarRentApp.Controllers
{
    public class RentRequestController : Controller
    {
        private RentDbContext db = new RentDbContext();

        // GET: /RentRequest/
        public ActionResult Index()
        {
            var rentrequests = db.RentRequests.Include(r => r.Customer).Include(r => r.VehicleType);
           List< RentRequestViewModel> rentrequestViewModel = Mapper.Map<List<RentRequestViewModel>>(rentrequests);


            return View(rentrequestViewModel);
        }

        // GET: /RentRequest/Details/5
        public ActionResult Details(int? id)
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
            RentRequestViewModel rentrequestViewMOdel = Mapper.Map<RentRequestViewModel>(rentrequest);
            return View(rentrequestViewMOdel);
        }

        // GET: /RentRequest/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
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
                RentRequest rentRequest = Mapper.Map<RentRequest>(rentRequestViewModel);
                db.RentRequests.Add(rentRequest);
                db.SaveChanges();
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
            RentRequest rentrequest = db.RentRequests.Find(id);
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
            db.RentRequests.Remove(rentrequest);
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