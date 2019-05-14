using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApplication.DataAccess;
using StoreApplication.DataAccess.Entities;
using StoreApplication.Library;
using StoreApplication.Library.Interfaces;
using StoreApplicationWeb.Models;

namespace StoreApplicationWeb.Controllers
{
    public class CustomerController : Controller
    {
        public StoreApplicationContext dbContext = StoreRepository.CreateDbContext();
        public IStoreRepository StoreRepo { get; set; } 

        // GET: Customer    
        public ActionResult Index()
        {
            StoreRepo = new StoreRepository(dbContext);
            var items = StoreRepo.GetNames();
            var viewModels = items.Select(c => new ModelCustomer
            {
                CustomerId = c.CustomerId,
                StoreId = c.StoreId,
                FName = c.FName,
                LName = c.LName,
                DefaultLocation = c.DefaultLocation,
                Orders = c.Orders,
                State = c.State,
               
            }).ToList();

           // var viewModels = customers.ToList();
            ViewBag.numOfCustomers = items.Count();
            return View(viewModels);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}