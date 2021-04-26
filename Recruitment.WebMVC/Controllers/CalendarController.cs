using Recruitment.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recruitment.WebMVC.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calender
        CandidateDbContext db = new CandidateDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var events = db.Candidate.Where(x => x.IsContacted == true).ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}