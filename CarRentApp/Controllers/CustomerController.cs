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

namespace CarRentApp.Controllers
{
    public class CustomerController : Controller
    {
        private RentDbContext db = new RentDbContext();

        // GET: /Customer/
        public ActionResult Index()
        {
            List<Customer> customer = db.Customers.ToList();
            List<CustomerViewModel> customerViewModels=new List<CustomerViewModel>();
            foreach (var cmr in customer)
            {
                CustomerViewModel cmrViewModel=new CustomerViewModel();
                cmrViewModel.Id = cmr.Id;
                cmrViewModel.Name = cmr.Name;
                cmrViewModel.ContactNo = cmr.ContactNo;
                cmrViewModel.Email = cmr.Email;
                cmrViewModel.Address = cmr.Address;
                customerViewModels.Add(cmrViewModel);
            }
            return View(customerViewModels);
        }

        // GET: /Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            CustomerViewModel customerViewModel=new CustomerViewModel();
            customerViewModel.Id = customer.Id;
            customerViewModel.Name = customer.Name;
            customerViewModel.ContactNo = customer.ContactNo;
            customerViewModel.Email = customer.Email;
            customerViewModel.Address = customer.Address;
            return View(customerViewModel);
        }

        // GET: /Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Email,ContactNo,Address")] CustomerViewModel customerViewModel)
        {
            if (ModelState.IsValid)
            {
                Customer customer=new Customer();
                customer.Id = customerViewModel.Id;
                customer.Name = customerViewModel.Name;
                customer.ContactNo = customerViewModel.ContactNo;
                customer.Email = customerViewModel.Email;
                customer.Address = customerViewModel.Address;
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerViewModel);
        }

        // GET: /Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();

            }
            CustomerViewModel customerViewModel = new CustomerViewModel();
            customerViewModel.Id = customer.Id;
            customerViewModel.Name = customer.Name;
            customerViewModel.ContactNo = customer.ContactNo;
            customerViewModel.Email = customer.Email;
            customerViewModel.Address = customer.Address;
            return View(customerViewModel);
        }

        // POST: /Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,ContactNo,Address")] CustomerViewModel customerViewModel)
        {
            if (ModelState.IsValid)
            {

                Customer customer = new Customer();
                customer.Id = customerViewModel.Id;
                customer.Name = customerViewModel.Name;
                customer.ContactNo = customerViewModel.ContactNo;
                customer.Email = customerViewModel.Email;
                customer.Address = customerViewModel.Address;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerViewModel);
        }

        // GET: /Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            CustomerViewModel customerViewModel = new CustomerViewModel();
            customerViewModel.Id = customer.Id;
            customerViewModel.Name = customer.Name;
            customerViewModel.ContactNo = customer.ContactNo;
            customerViewModel.Email = customer.Email;
            customerViewModel.Address = customer.Address;
            return View(customerViewModel);
        }

        // POST: /Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer != null)
            {
                db.Customers.Remove(customer);
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
