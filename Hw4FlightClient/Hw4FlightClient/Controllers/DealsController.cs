using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hw4FlightClient.Controllers
{
    public class DealsController : Controller
    {
        //
        // GET: /Deals/

        public ActionResult Index()
        {
            ViewBag.Menu = "Deals";
            return View();
        }

        //
        // GET: /Deals/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Deals/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Deals/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Deals/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Deals/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Deals/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Deals/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
