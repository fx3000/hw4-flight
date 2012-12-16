using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hw4FlightClient.Models;

namespace Hw4FlightClient.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        public PartialViewResult Search(string timefrom, string timeto, int direction, string from, string to, string datefrom, string dateto)
        {
            DateTime dateFrom = Convert.ToDateTime(datefrom);
            DateTime dateTo = Convert.ToDateTime(dateto);

            var flightInfos = DbContext.SearchFlight(timefrom, timeto, direction, from, to, dateFrom, dateTo);

            return PartialView("Partials/_SearchResultPartial", flightInfos.ToList());
        }
    }
}
