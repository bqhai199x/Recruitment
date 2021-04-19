using Recruitment;
using Recruitment.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CandidateMGMT.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        CandidateDbContext db = new CandidateDbContext();

        public ActionResult Index(string namestr)
        {
            List<CandidateViewModel> listCandi = db.Candidate
                .Select(x => new CandidateViewModel
                {
                    CandidateId = x.CandidateId,
                    LevelName = x.Level.LevelName,
                    PositionName = x.Position.PositionName,
                    FullName = x.FullName,
                    Birthday = x.Birthday,
                    Address = x.Address,
                    Phone = x.Phone,
                    Email = x.Email,
                    IntroduceName = x.IntroduceName,
                    CV = x.CV,
                    IsApplied = x.IsApplied,
                    Status = x.Status
                })
                .ToList();

            if (!string.IsNullOrEmpty(namestr))
            {
                listCandi = listCandi.Where(x => x.FullName.Contains(namestr)).ToList();
            }

            ViewBag.CandidateList = listCandi;

            return View();
        }

        [HttpPost]
        public ActionResult Index(CandidateViewModel model)
        {
            try
            {
                var f = Request.Files["CV"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Server.MapPath("~/wwwroot/CV/" + FileName);
                    f.SaveAs(UploadPath);
                    model.CV = FileName;
                }

                if (model.CandidateId > 0)
                {
                    //Update
                    Candidate emp = db.Candidate.FirstOrDefault(x => x.CandidateId == model.CandidateId);

                    emp.LevelId = model.LevelId;
                    emp.PositionId = model.PositionId;
                    emp.FullName = model.FullName;
                    emp.Birthday = model.Birthday;
                    emp.Address = model.Address;
                    emp.Phone = model.Phone;
                    emp.Email = model.Email;
                    emp.IntroduceName = model.IntroduceName;
                    emp.CV = model.CV;
                    db.SaveChanges();
                }
                else
                {
                    //Insert
                    Candidate emp = new Candidate();
                    emp.LevelId = model.LevelId;
                    emp.PositionId = model.PositionId;
                    emp.FullName = model.FullName;
                    emp.Birthday = model.Birthday;
                    emp.Address = model.Address;
                    emp.Phone = model.Phone;
                    emp.Email = model.Email;
                    emp.IntroduceName = model.IntroduceName;
                    emp.CV = model.CV;
                    db.Candidate.Add(emp);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AddOrEdit(int CandidateId)
        {
            List<Position> plist = db.Position.ToList();
            ViewBag.PositionList = new SelectList(plist, "PositionId", "PositionName");

            List<Level> llist = db.Level.ToList();
            ViewBag.LevelList = new SelectList(llist, "LevelId", "LevelName");

            CandidateViewModel model = new CandidateViewModel();
            if (CandidateId > 0)
            {
                Candidate emp = db.Candidate.FirstOrDefault(x => x.CandidateId == CandidateId);
                model.CandidateId = emp.CandidateId;
                model.LevelId = emp.LevelId;
                model.PositionId = emp.PositionId;
                model.FullName = emp.FullName;
                model.Birthday = emp.Birthday;
                model.Address = emp.Address;
                model.Phone = emp.Phone;
                model.Email = emp.Email;
                model.IntroduceName = emp.IntroduceName;
                model.CV = emp.CV;
            }
            return PartialView("_Modal", model);
        }

        public ActionResult Delete(int id)
        {
            Candidate emp = db.Candidate.FirstOrDefault(x => x.CandidateId == id);
            db.Candidate.Remove(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}