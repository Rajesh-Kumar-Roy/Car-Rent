using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using CarRentApp.Models;
using CarRentApp.Context;
using CarRentApp.ViewModels;
using Microsoft.Reporting.WebForms;

namespace CarRentApp.Controllers
{
    [Authorize(Roles = "Controller,AppAdmin")]
    public class RentAssignController : Controller
    {
        private RentDbContext db = new RentDbContext();

        // GET: /RentAssign/
        public ActionResult Index()
        {
            var rentassigns = db.RentAssigns.Include(r => r.RentRequest).Include(r=>r.VehicleType).ToList();
            List<RentAssignViewModel> rentAssignViewModel = Mapper.Map<List<RentAssignViewModel>>(rentassigns);
            return View(rentAssignViewModel);
        }

        // GET: /RentAssign/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentAssign rentassign = db.RentAssigns.Include(c => c.VehicleType).Include(c => c.RentRequest).FirstOrDefault(d => d.Id == id);
            
            if (rentassign == null)
            {
                return HttpNotFound();
            }
            RentAssignViewModel rentAssignViewModel = Mapper.Map<RentAssignViewModel>(rentassign);
            return View(rentAssignViewModel);
        }

        // GET: /RentAssign/Assign
        public ActionResult Assign(int? rentRqId)
        {
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Name");
            ViewBag.RentRequestId = rentRqId;
            return View();
        }

        // POST: /RentAssign/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assign([Bind(Include = "Id,RentPrice,Status,RentAssignDateTime,VehicleTypeId,RentRequestId")] RentAssignViewModel rentAssignViewModel)
        {
            if (ModelState.IsValid)
            {
                RentAssign rentAssign = Mapper.Map<RentAssign>(rentAssignViewModel);
                rentAssign.Status = "Assigned";
                rentAssign.RentAssignDateTime=DateTime.Now;
                db.RentAssigns.Add(rentAssign);
                var count=db.SaveChanges()>0;
                if (count)
                {
                    var rentRequest = db.RentRequests.FirstOrDefault(c => c.Id == rentAssign.RentRequestId);
                    if (rentRequest!=null)
                    {
                        Notification notification = new Notification();
                        notification.Status = rentAssign.Status;
                        notification.Details = "Your rent vehicle is assigned";
                        notification.NotificatinDateTime = DateTime.Now;
                        notification.RentRequestId = rentAssign.RentRequestId;
                        notification.CustomerId = rentRequest.CustomerId;
                        db.Notifications.Add(notification);
                        db.SaveChanges();
                    }
                    
                }

                return RedirectToAction("Index");
            }

            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Name", rentAssignViewModel.VehicleTypeId);
            ViewBag.RentRequestId = rentAssignViewModel.RentRequestId;
            return View(rentAssignViewModel);
        }

        // GET: /RentAssign/Create
        public ActionResult Create()
        {
            ViewBag.RentRequestId = new SelectList(db.RentRequests, "Id", "FromPlace");
            return View();
        }

        // POST: /RentAssign/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,RentPrice,Status,RentAssignDateTime,RentRequestId")] RentAssign rentassign)
        {
            if (ModelState.IsValid)
            {
                db.RentAssigns.Add(rentassign);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RentRequestId = new SelectList(db.RentRequests, "Id", "FromPlace", rentassign.RentRequestId);
            return View(rentassign);
        }

        // GET: /RentAssign/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentAssign rentassign = db.RentAssigns.Find(id);
            if (rentassign == null)
            {
                return HttpNotFound();
            }
            ViewBag.RentRequestId = new SelectList(db.RentRequests, "Id", "FromPlace", rentassign.RentRequestId);
            return View(rentassign);
        }

        // POST: /RentAssign/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,RentPrice,Status,RentAssignDateTime,RentRequestId")] RentAssign rentassign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentassign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RentRequestId = new SelectList(db.RentRequests, "Id", "FromPlace", rentassign.RentRequestId);
            return View(rentassign);
        }

        // GET: /RentAssign/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentAssign rentassign = db.RentAssigns.Find(id);
            if (rentassign == null)
            {
                return HttpNotFound();
            }
            return View(rentassign);
        }

        // POST: /RentAssign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentAssign rentAssign = db.RentAssigns.Find(id);
            db.RentAssigns.Remove(rentAssign ?? throw new InvalidOperationException()); 
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult RentReport()
        {
            var rentReport = db.GetRentAssignedReport();
            string reportPath = Request.MapPath(Request.ApplicationPath) +
                                @"\Reports\RentAssigned\RentAssignedReportrdlc.rdlc";
            var reportViewer = new ReportViewer()
            {
                KeepSessionAlive = true,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                ProcessingMode = ProcessingMode.Local
            };

            reportViewer.LocalReport.ReportPath = reportPath;
            ReportDataSource rds=new ReportDataSource("DS_RentAssignedSummary",rentReport);
            reportViewer.LocalReport.DataSources.Add(rds);
            ViewBag.ReportViewer = reportViewer;

            return View();
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
