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

namespace CarRentApp.Controllers
{
     [Authorize (Roles ="Controller,AppAdmin")]
    public class RentRequestHistoryController : Controller
    {
        private RentDbContext db = new RentDbContext();

        // GET: /RentRequestHistory/
        public ActionResult Index()
        {
            var rentrequesthistorys = db.RentRequestHistorys.Include(r => r.RentRequest);
            return View(rentrequesthistorys.ToList());
        }

        // GET: /RentRequestHistory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentRequestHistory rentrequesthistory = db.RentRequestHistorys.Include(r => r.RentRequest).FirstOrDefault(m=>m.Id==id);
            if (rentrequesthistory == null)
            {
                return HttpNotFound();
            }
            return View(rentrequesthistory);
        }

        // GET: /RentRequestHistory/Create
        public ActionResult Create()
        {
            ViewBag.RentRequestId = new SelectList(db.RentRequests, "Id", "FromPlace");
            return View();
        }

        // POST: /RentRequestHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Text,Status,HistoryDateTime,RentRequestId")] RentRequestHistory rentrequesthistory)
        {
            if (ModelState.IsValid)
            {
                db.RentRequestHistorys.Add(rentrequesthistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RentRequestId = new SelectList(db.RentRequests, "Id", "FromPlace", rentrequesthistory.RentRequestId);
            return View(rentrequesthistory);
        }

        // GET: /RentRequestHistory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentRequestHistory rentrequesthistory = db.RentRequestHistorys.Find(id);
            if (rentrequesthistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.RentRequestId = new SelectList(db.RentRequests, "Id", "FromPlace", rentrequesthistory.RentRequestId);
            return View(rentrequesthistory);
        }

        // POST: /RentRequestHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Text,Status,HistoryDateTime,RentRequestId")] RentRequestHistory rentrequesthistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentrequesthistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RentRequestId = new SelectList(db.RentRequests, "Id", "FromPlace", rentrequesthistory.RentRequestId);
            return View(rentrequesthistory);
        }

        // GET: /RentRequestHistory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentRequestHistory rentrequesthistory = db.RentRequestHistorys.Include(r => r.RentRequest).FirstOrDefault(m => m.Id == id);
            if (rentrequesthistory == null)
            {
                return HttpNotFound();
            }
            return View(rentrequesthistory);
        }

        // POST: /RentRequestHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentRequestHistory rentrequesthistory = db.RentRequestHistorys.Find(id);
            if (rentrequesthistory != null)
            {
                 db.RentRequestHistorys.Remove(rentrequesthistory);
                 db.SaveChanges();
            }
           
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
