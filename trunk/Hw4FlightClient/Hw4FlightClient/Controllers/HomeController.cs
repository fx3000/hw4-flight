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
            ViewBag.Menu = "Home";
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
            ViewBag.Menu = "Home";

            DateTime dateFrom = Convert.ToDateTime(datefrom);
            DateTime dateTo = Convert.ToDateTime(dateto);
            string[] f = from.Split('(');
            string[] t = to.Split('(');
            if(f.Count() == 2)
            {
                from = f[1].TrimEnd(')');
            }

            if (t.Count() == 2)
            {
                to = t[1].TrimEnd(')');
            }

            var flightInfos = DbContext.SearchFlight(timefrom, timeto, direction, from, to, dateFrom, dateTo);

            return PartialView("Partials/_SearchResultPartial", flightInfos.ToList());
        }

        public PartialViewResult Sort(int type)
        {
            ViewBag.Menu = "Home";

            var flightInfos = DbContext.Sort(type);
            return PartialView("Partials/_SearchResultPartial", flightInfos.ToList());
        }

        public PartialViewResult Filter(string type)
        {
            ViewBag.Menu = "Home";

            var flightInfos = DbContext.Filter(type);
            return PartialView("Partials/_SearchResultPartial", flightInfos.ToList());
        }

        public ActionResult PutTicket(int id)
        {
            ViewBag.Menu = "Home";

            var result = DbContext.GetFlight(id);
            return View(result);
        }

        public ActionResult PutTicketSuccess()
        {
            ViewBag.Menu = "Home";
            return View();
        }
    }
}
