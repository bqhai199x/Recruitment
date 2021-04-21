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

        public ActionResult ScheduleView(int CandidateId)
        {
            var candi = db.Candidate.Find(CandidateId);
            var model = new CandidateViewModel();
            {
                model.CandidateId = candi.CandidateId;
                model.InterviewTime = candi.InterviewTime;
                model.InterviewLocation = candi.InterviewLocation;
                model.Note = candi.Note;
            }
            return PartialView("_Schedule", model);
        }

        public ActionResult Schedule(CandidateViewModel model)
        {
            var candi = db.Candidate.Find(model.CandidateId);
            candi.InterviewTime = model.InterviewTime;
            candi.InterviewLocation = model.InterviewLocation;
            candi.Note = model.Note;
            candi.IsContacted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult NotBeContacted(int CandidateId)
        {
            var candi = db.Candidate.Find(CandidateId);
            candi.InterviewTime = null;
            candi.InterviewLocation = null;
            candi.Note = null;
            candi.IsContacted = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}