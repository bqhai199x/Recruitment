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
        public ActionResult Index()
        {
            List<Position> plist = db.Position.ToList();
            ViewBag.PositionList = new SelectList(plist, "PositionId", "PositionName");

            List<Level> llist = db.Level.ToList();
            ViewBag.LevelList = new SelectList(llist, "LevelId", "LevelName");

            List<CandidateViewModel> listCandi = db.Candidate
                .Where(x => x.Status == Status.NhanCV || x.Status == Status.LoaiCV)
                .Select(x => new CandidateViewModel
                {
                    CandidateId = x.CandidateId,
                    LevelName = x.Level.LevelName,
                    PositionName = x.Position.PositionName,
                    FullName = x.FullName,
                    Birthday = x.Birthday,
                    Address = x.Address,
                    IntroduceName = x.IntroduceName,
                    CV = x.CV
                })
                .ToList();

            //if (!string.IsNullOrEmpty(namestr))
            //{
            //    listCandi = listCandi.Where(x => x.FullName.Contains(namestr)).ToList();
            //}

            ViewBag.CandidateList = listCandi;

            return View();
        }
    }
}