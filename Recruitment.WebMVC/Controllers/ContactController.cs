using Recruitment.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
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
                    EmployeeId = x.EmployeeId,
                    InterviewName = x.Employee.FullName,
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
                model.EmployeeId = candi.EmployeeId;
                model.InterviewName = candi.EmployeeId == null ? null : candi.Employee.FullName;
            }
            List<Employee> elist = db.Employee.ToList();
            ViewBag.EmployeeList = new SelectList(elist, "EmployeeId", "FullName");
            return PartialView("_ScheduleView", model);
        }

        public ActionResult Schedule(CandidateViewModel model)
        {
            var candi = db.Candidate.Find(model.CandidateId);
            candi.InterviewTime = model.InterviewTime;
            candi.InterviewLocation = model.InterviewLocation;
            candi.Note = model.Note;
            candi.IsContacted = true;
            candi.EmployeeId = model.EmployeeId;
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
            candi.Status = 2;
            candi.EmployeeId = null;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SendMailView(int CandidateId)
        {
            var candi = db.Candidate.Find(CandidateId);
            var n = Environment.NewLine;
            var email = new EmailModelView()
            {
                CandidateId = candi.CandidateId,
                To = candi.Email,
                Bcc = candi.Employee.Email,
                Subject = candi.LevelId == 1 ? "Saishunkan System Vietnam_Thư mời test" : "Vòng 1 - Saishunkan System Vietnam_Thư mời phỏng vấn",
                Body =
                    $"Chào bạn {candi.FullName},{n}" +
                    $"{n}" +
                    $"Chúng tôi là phụ trách nhân sự công ty Saishunkan System Vietnam.{n}" +
                    $"Cảm ơn bạn đã nộp hồ sơ ứng tuyển vị trí {candi.Level.LevelName} {candi.Position.PositionName} cho chúng tôi.{n}" +
                    $"{n}" +
                    $"Bên chúng tôi có kế hoạch phỏng vấn vào thời gian và địa điểm như sau:{n}" +
                    $"Thời gian: {candi.InterviewTime}{n}" +
                    $"Địa điểm: {candi.InterviewLocation}{n}" +
                    $"{n}" +
                    $"Thanks & Regards"
            };
            return PartialView("_SendMailView", email);
        }

        public ActionResult SendEmail(EmailModelView email)
        {
            try
            {
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(senderEmail);
                mailMessage.To.Add(email.To);
                mailMessage.Bcc.Add(email.Bcc);
                mailMessage.Subject = email.Subject;
                mailMessage.Body = email.Body.Replace("\r\n", "<br>");
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = Encoding.UTF8;
                client.Send(mailMessage);

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}