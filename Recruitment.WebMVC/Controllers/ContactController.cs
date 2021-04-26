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
        public ActionResult Index(string searchstr, int? PositionId, int? LevelId, int? Contacted, DateTime? FromDate, DateTime? ToDate, string Location, string sortOrder)
        {
            List<Position> plist = db.Position.ToList();
            ViewBag.PositionList = new SelectList(plist, "PositionId", "PositionName");

            List<Level> llist = db.Level.ToList();
            ViewBag.LevelList = new SelectList(llist, "LevelId", "LevelName");

            List<CandidateViewModel> listCandi = db.Candidate
                .Where(x => x.Status == 2)
                .Select(x => new CandidateViewModel
                {
                    CandidateId = x.CandidateId,
                    PositionId = x.PositionId,
                    EmployeeId = x.EmployeeId,
                    InterviewName = x.EmployeeId==null?"":x.Employee.FullName,
                    LevelId = x.LevelId,
                    LevelName = x.Level.LevelName,
                    PositionName = x.Position.PositionName,
                    FullName = x.FullName,
                    Email = x.Email,
                    Phone = x.Phone,
                    IsContacted = x.IsContacted,
                    InterviewTime = x.InterviewTime,
                    InterviewLocation = x.InterviewLocation == null?"": x.InterviewLocation,
                    Note = x.Note,
                    Status = x.Status,
                })
                .ToList();

            if (!string.IsNullOrEmpty(searchstr))
            {
                listCandi = listCandi
                    .Where(x => x.FullName.Contains(searchstr) || x.PositionName.Contains(searchstr) || x.LevelName.Contains(searchstr) ||
                    x.InterviewLocation.Contains(searchstr) || x.InterviewName.Contains(searchstr))
                    .ToList();
            }

            if (PositionId != null)
            {
                listCandi = listCandi.Where(x => x.PositionId == PositionId).ToList();
            }

            if (LevelId != null)
            {
                listCandi = listCandi.Where(x => x.LevelId == LevelId).ToList();
            }

            if (Contacted != null)
            {
                switch (Contacted)
                {
                    case 1:
                        listCandi = listCandi.Where(x => x.IsContacted == null).ToList();
                        break;
                    case 2:
                        listCandi = listCandi.Where(x => x.IsContacted == true).ToList();
                        break;
                    default:
                        listCandi = listCandi.Where(x => x.IsContacted == false).ToList();
                        break;
                }
            }

            if (FromDate != null)
            {
                listCandi = listCandi.Where(x => x.InterviewTime > FromDate).ToList();
            }

            if (ToDate != null)
            {
                listCandi = listCandi.Where(x => x.InterviewTime < ToDate).ToList();
            }

            if (!string.IsNullOrEmpty(Location))
            {
                listCandi = listCandi.Where(x => x.InterviewLocation == Location).ToList();
            }


            ViewBag.SortByName = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SortByPosition = sortOrder == "position" ? "position_desc" : "position";
            ViewBag.SortByLevel = sortOrder == "level" ? "level_desc" : "level";
            ViewBag.SortByContacted = sortOrder == "contacted" ? "contacted_desc" : "contacted";
            ViewBag.SortByTime = sortOrder == "time" ? "time_desc" : "time";
            ViewBag.SortByLocation = sortOrder == "location" ? "location_desc" : "location";
            ViewBag.SortByInterviewName = sortOrder == "ivname" ? "ivname_desc" : "ivname";

            switch (sortOrder)
            {
                case "name_desc":
                    listCandi = listCandi.OrderByDescending(s => s.FullName).ToList();
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
                case "contacted":
                    listCandi = listCandi.OrderBy(s => s.IsContacted).ToList();
                    break;
                case "contacted_desc":
                    listCandi = listCandi.OrderByDescending(s => s.IsContacted).ToList();
                    break;
                case "time":
                    listCandi = listCandi.OrderBy(s => s.InterviewTime).ToList();
                    break;
                case "time_desc":
                    listCandi = listCandi.OrderByDescending(s => s.InterviewTime).ToList();
                    break;
                case "location":
                    listCandi = listCandi.OrderBy(s => s.InterviewLocation).ToList();
                    break;
                case "location_desc":
                    listCandi = listCandi.OrderByDescending(s => s.InterviewLocation).ToList();
                    break;
                case "ivname":
                    listCandi = listCandi.OrderBy(s => s.InterviewName).ToList();
                    break;
                case "ivname_desc":
                    listCandi = listCandi.OrderByDescending(s => s.InterviewName).ToList();
                    break;
                default:
                    listCandi = listCandi.OrderBy(s => s.FullName).ToList();
                    break;
            }

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
            candi.IsContacted = model.IsContacted;
            candi.EmployeeId = model.EmployeeId;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Cancel(int CandidateId)
        {
            var candi = db.Candidate.Find(CandidateId);
            candi.InterviewTime = null;
            candi.InterviewLocation = null;
            candi.Note = null;
            candi.IsContacted = null;
            candi.EmployeeId = null;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SendMailView(int CandidateId)
        {
            var candi = db.Candidate.Find(CandidateId);
            var n = Environment.NewLine;
            var email = Session[$"Mail{CandidateId}"] as EmailModelView;
            return PartialView("_SendMailView", email);
        }

        public ActionResult MultiSend(int[] CandidateId)
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

                var maillist = new List<EmailModelView>();
                for (int i = 0; i < CandidateId.Length; i++)
                {
                    maillist.Add(Session[$"Mail{CandidateId[i]}"] as EmailModelView);
                }


                foreach (var item in maillist)
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(senderEmail);
                    mailMessage.To.Add(item.To);
                    mailMessage.Bcc.Add(item.Bcc);
                    mailMessage.Subject = item.Subject;
                    mailMessage.Body = item.Body.Replace("\r\n", "<br>");
                    mailMessage.IsBodyHtml = true;
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    client.Send(mailMessage);
                }

                for (int i = 0; i < CandidateId.Length; i++)
                {
                    db.Candidate.Find(CandidateId[i]).Status = db.Candidate.Find(CandidateId[i]).LevelId == 1 ? 3 : 5;
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult CreateMail(int[] CandidateId)
        {
            try
            {
                var emplst = new List<Candidate>();
                for (int i = 0; i < CandidateId.Length; i++)
                {
                    emplst.Add(db.Candidate.Find(CandidateId[i]));
                }

                var n = Environment.NewLine;
                foreach (var item in emplst)
                {
                    EmailModelView mail = new EmailModelView();
                    mail.CandidateId = item.CandidateId;
                    mail.To = item.Email;
                    mail.Bcc = item.Employee.Email;
                    mail.Subject = item.LevelId == 1 ? "Saishunkan System Vietnam_Thư mời test" : "Vòng 1 - Saishunkan System Vietnam_Thư mời phỏng vấn";
                    mail.Body =
                        ($"Chào bạn {item.FullName},{n}" +
                        $"{n}" +
                        $"Chúng tôi là phụ trách nhân sự công ty Saishunkan System Vietnam.{n}" +
                        $"Cảm ơn bạn đã nộp hồ sơ ứng tuyển vị trí {item.Level.LevelName} {item.Position.PositionName} cho chúng tôi.{n}" +
                        $"{n}" +
                        $"Bên chúng tôi có kế hoạch phỏng vấn vào thời gian và địa điểm như sau:{n}" +
                        $"Thời gian: {item.InterviewTime}{n}" +
                        $"Địa điểm: {item.InterviewLocation}{n}" +
                        $"{n}" +
                        $"Thanks & Regards");

                    Session[$"Mail{item.CandidateId}"] = mail;
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult UpdateMail(EmailModelView model)
        {
            var mail = Session[$"Mail{model.CandidateId}"] as EmailModelView;
            mail.To = model.To;
            mail.Bcc = model.Bcc;
            mail.Subject = model.Subject;
            mail.Body = model.Body;

            return RedirectToAction("Index");
        }
    }
}