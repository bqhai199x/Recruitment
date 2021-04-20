using Recruitment.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recruitment.WebMVC.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        CandidateDbContext db = new CandidateDbContext();
        public ActionResult Index()
        {
            List<Position> plist = db.Position.ToList();
            ViewBag.PositionList = new SelectList(plist, "PositionId", "PositionName");

            List<Level> llist = db.Level.ToList();
            ViewBag.LevelList = new SelectList(llist, "LevelId", "LevelName");

            List<CandidateViewModel> listCandi = db.Candidate
                .Where(x => x.Status != 0 && x.Status != 1)
                .Select(x => new CandidateViewModel
                {
                    CandidateId = x.CandidateId,
                    PositionId = x.PositionId,
                    LevelId = x.LevelId,
                    LevelName = x.Level.LevelName,
                    PositionName = x.Position.PositionName,
                    FullName = x.FullName,
                    Birthday = x.Birthday,
                    Address = x.Address,
                    Email = x.Email,
                    Phone = x.Phone,
                    IsContacted = x.IsContacted,
                    InterviewTime = x.InterviewTime,
                    InterviewLocation = x.InterviewLocation,
                    Note = x.Note,
                    Status = x.Status,
                    IntroduceName = x.IntroduceName,
                    CV = x.CV,
                    IsPDF = x.IsPDF,
                    IsApplied = x.IsApplied
                })
                .ToList();

            ViewBag.CandidateList = listCandi;

            return View();

        }
    }
}