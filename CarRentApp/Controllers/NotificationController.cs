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
    public class NotificationController : Controller
    {
        private RentDbContext db = new RentDbContext();

        // GET: /Notification/
        public ActionResult Index()
        {
            var notifications = db.Notifications.Include(n => n.Customer).Include(n => n.RentRequest);
            return View(notifications.ToList());
        }

        // GET: /Notification/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // GET: /Notification/Replay
        public ActionResult Replay(int? rentRqId)
        {
            Notification notification = db.Notifications.Include(d => d.RentRequest).Include(d => d.RentRequest.VehicleType).Include(d => d.Customer).FirstOrDefault(c => c.RentRequestId == rentRqId);
            NotificationViewModel notificationViewModel = Mapper.Map<NotificationViewModel>(notification);
            List<Notification> notificationList = db.Notifications.Include(d => d.Customer).Where(c => c.RentRequestId == rentRqId).ToList();
            //ViewBag.Notification = notificationViewModel;
            ViewBag.NotificationList = notificationList;
            //ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            //ViewBag.RentRequestId = new SelectList(db.RentRequests, "Id", "FromPlace");
            return View(notificationViewModel);
        }

        // POST: /Notification/Replay
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Replay([Bind(Include = "Id,Status,Details,NotificatinDateTime,RentRequestId,CustomerId,ReplayText")] NotificationViewModel notificationViewModel)
        {
            if (ModelState.IsValid)
            {
                //Notification notification = db.Notifications.FirstOrDefault(c => c.RentRequestId == notificationViewModel.RentRequestId);
                //if (notification != null)
                //{
                //}
                Notification notificationObj = new Notification();
                    notificationObj.Status = "New";
                    notificationObj.Details = notificationViewModel.ReplayText;
                    notificationObj.NotificatinDateTime = DateTime.Now;
                    notificationObj.CustomerId = notificationViewModel.CustomerId;
                    notificationObj.RentRequestId = notificationViewModel.RentRequestId;
                    db.Notifications.Add(notificationObj);
                    db.SaveChanges();
                    return RedirectToAction("Replay", new { rentRqId = notificationViewModel.RentRequestId });

                
            }
         
            List<Notification> notificationList = db.Notifications.Include(d => d.Customer).Where(c => c.RentRequestId == notificationViewModel.RentRequestId).ToList();
            ViewBag.NotificationList = notificationList;
            return RedirectToAction("Replay", "Notification", new { rentRqId = notificationViewModel.RentRequestId });
        }
        // GET: /Notification/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            ViewBag.RentRequestId = new SelectList(db.RentRequests, "Id", "FromPlace");
            return View();
        }

        // POST: /Notification/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Status,Details,NotificatinDateTime,RentRequestId,CustomerId")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                db.Notifications.Add(notification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", notification.CustomerId);
            ViewBag.RentRequestId = new SelectList(db.RentRequests, "Id", "FromPlace", notification.RentRequestId);
            return View(notification);
        }

        // GET: /Notification/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", notification.CustomerId);
            ViewBag.RentRequestId = new SelectList(db.RentRequests, "Id", "FromPlace", notification.RentRequestId);
            return View(notification);
        }

        // POST: /Notification/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Status,Details,NotificatinDateTime,RentRequestId,CustomerId")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", notification.CustomerId);
            ViewBag.RentRequestId = new SelectList(db.RentRequests, "Id", "FromPlace", notification.RentRequestId);
            return View(notification);
        }

        // GET: /Notification/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // POST: /Notification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notification notification = db.Notifications.Find(id);
            db.Notifications.Remove(notification);
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
