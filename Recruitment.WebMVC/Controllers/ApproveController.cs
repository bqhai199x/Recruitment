using Recruitment.Core;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Recruitment.WebMVC.Controllers
{
    public class ApproveController : Controller
    {
        // GET: Approve
        CandidateDbContext db = new CandidateDbContext();
        public ActionResult Index(string searchstr, int? PositionId, int? LevelId, string sortOrder)
        {
            List<Position> plist = db.Position.ToList();
            ViewBag.PositionList = new SelectList(plist, "PositionId", "PositionName");

            List<Level> llist = db.Level.ToList();
            ViewBag.LevelList = new SelectList(llist, "LevelId", "LevelName");

            var listCandi = db.Candidate
                .Where(x => x.Status == 0 || x.Status == 1)
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
                    IntroduceName = x.IntroduceName==null?"":x.IntroduceName,
                    CV = x.CV,
                    IsPDF = x.IsPDF,
                    IsApplied = x.IsApplied
                })
                .ToList();

            if (!string.IsNullOrEmpty(searchstr))
            {
                listCandi = listCandi
                    .Where(x => x.FullName.Contains(searchstr) || x.PositionName.Contains(searchstr) || x.LevelName.Contains(searchstr) ||
                    x.Birthday.Contains(searchstr) || x.Address.Contains(searchstr) || x.IntroduceName.Contains(searchstr))
                    .ToList();
            }

            if(PositionId != null)
            {
                listCandi = listCandi.Where(x => x.PositionId == PositionId).ToList();
            }

            if(LevelId != null)
            {
                listCandi = listCandi.Where(x => x.LevelId == LevelId).ToList();
            }

            ViewBag.SortByName = string.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewBag.SortByPosition = sortOrder == "position" ? "position_desc" : "position";
            ViewBag.SortByLevel = sortOrder == "level" ? "level_desc" : "level";

            switch (sortOrder)
            {
                case "name":
                    listCandi = listCandi.OrderBy(s => s.FullName).ToList();
                    break;
                case "position":
                    listCandi = listCandi.OrderBy(s => s.PositionId).ToList();
                    break;
                case "position_desc":
                    listCandi = listCandi.OrderByDescending(s => s.PositionId).ToList();
                    break;
                case "level":
                    listCandi = listCandi.OrderBy(s => s.LevelId).ToList();
                    break;
                case "level_desc":
                    listCandi = listCandi.OrderByDescending(s => s.LevelId).ToList();
                    break;
                default:
                    listCandi = listCandi.OrderByDescending(s => s.FullName).ToList();
                    break;
            }

            ViewBag.CandidateList = listCandi;

            return View();
        }

        public ActionResult ShowCV(int CandidateId) 
        {
            Candidate emp = db.Candidate.FirstOrDefault(x => x.CandidateId == CandidateId);
            ViewBag.CV = emp.CV;
            ViewBag.Name = emp.FullName;
            ViewBag.Id = emp.CandidateId;
            return PartialView("_ShowCV");
        }

        public ActionResult ApplyCV(int candidateId, int apply)
        {
            Candidate emp = db.Candidate.FirstOrDefault(x => x.CandidateId == candidateId);
            if(apply == 0)
            {
                emp.Status = 1;
                emp.IsApplied = 0;
            }
            else
            {
                emp.Status = 2;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}