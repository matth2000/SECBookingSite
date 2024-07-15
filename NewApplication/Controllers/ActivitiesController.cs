using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewApplication;
using OfficeOpenXml;
using Renci.SshNet;
using Rotativa;

namespace NewApplication.Controllers
{
    public class ActivitiesController : Controller
    {
        private DBModelContainer db = new DBModelContainer();


        public ActionResult activPassGet()
        {
            if (Session["VerifiedActiv"] != null)
            {
                var sessionClub = Session["VerifiedActiv"] as string;
                if (sessionClub == true.ToString())
                {
                    return RedirectToAction("Index", "Activities");
                }
            }
            return View();
        }

        public string activPassPost(string pass)
        {
            if(pass == "24pchj0bsv95")
            {
                Session["VerifiedActiv"] = true.ToString();
                return Url.Action("Index", "Activities").ToString();
            }
            else
            {
                Session["VerifiedActiv"] = null;
                return Url.Action("activPassGet", "Activities").ToString();
            }

        }

        // GET: Activities
        public ActionResult Index()
        {
            if (Session["VerifiedActiv"] != null)
            {
                var sessionClub = Session["VerifiedActiv"] as string;
                if (sessionClub == true.ToString())
                {
                    var activities = db.Activities.Include(a => a.AgeGroup);
                    return View(activities.ToList());
                }
            }
            return RedirectToAction("activPassGet", "Activities");
        }


        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            if (Session["VerifiedActiv"] != null)
            {
                var sessionClub = Session["VerifiedActiv"] as string;
                if (sessionClub == true.ToString())
                {   
                    
                    return View(activity);
                }
            }
            return RedirectToAction("activPassGet", "Activities");
        }

        public void ExcelDownload(int activId)
        {
            Activity activity = db.Activities.Find(activId);
            List<Session> sessions = activity.Sessions.ToList();
            ExcelPackage Ep = new ExcelPackage();
            foreach (var session in sessions)
            {
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add((activity.ActivityName + session.SessionGroup.SessionName));
                Sheet.Cells["A1"].Value = "Activity";
                Sheet.Cells["B1"].Value = "First Name";
                Sheet.Cells["C1"].Value = "Last Name";
                Sheet.Cells["D1"].Value = "Club";
                Sheet.Cells["E1"].Value = "Age Group";
                int row = 2;
                foreach (var participant in session.Participants)
                {
                    Sheet.Cells[string.Format("A{0}", row)].Value = activity.ActivityName;
                    Sheet.Cells[string.Format("B{0}", row)].Value = participant.ParticipantFirstName;
                    Sheet.Cells[string.Format("C{0}", row)].Value = participant.ParticipantLastName;
                    Sheet.Cells[string.Format("D{0}", row)].Value = participant.Club.ClubName;
                    Sheet.Cells[string.Format("E{0}", row)].Value = participant.AgeGroup.AgeGroupName;
                    row++;
                }
                Sheet.Cells["A:AZ"].AutoFitColumns();
            }
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        public void ExcelDownloadAdmin()
        {
            List<SessionGroup> sessionGroups = db.SessionGroups.ToList();
            ExcelPackage Ep = new ExcelPackage();
            foreach (var sessionGroup in sessionGroups)
            {                
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add(sessionGroup.SessionName);
                Sheet.Cells["A1"].Value = "Activity";
                Sheet.Cells["B1"].Value = "First Name";
                Sheet.Cells["C1"].Value = "Last Name";
                Sheet.Cells["D1"].Value = "Club";
                Sheet.Cells["E1"].Value = "Age Group";
                int row = 2;
                foreach(var session in sessionGroup.Sessions)
                {
                    foreach (var participant in session.Participants)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = session.Activity.ActivityName;
                        Sheet.Cells[string.Format("B{0}", row)].Value = participant.ParticipantFirstName;
                        Sheet.Cells[string.Format("C{0}", row)].Value = participant.ParticipantLastName;
                        Sheet.Cells[string.Format("D{0}", row)].Value = participant.Club.ClubName;
                        Sheet.Cells[string.Format("E{0}", row)].Value = participant.AgeGroup.AgeGroupName;
                        row++;
                    }
                }
                Sheet.Cells["A:AZ"].AutoFitColumns();
            }
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }
        /*
       public ActionResult PrintActivity(int ActivityId)
        {
            Activity activity = db.Activities.Find(ActivityId);
            return new ActionAsPdf("Details", new { id = ActivityId }) { FileName = (activity.ActivityName + "ParticipantList.pdf") };
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            ViewBag.AgeGroupAgeGroupId = new SelectList(db.AgeGroups, "AgeGroupId", "AgeGroupName");
            ViewBag.Zone_ZoneId = new SelectList(db.Zones, "ZoneId", "ZoneName");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityId,AgeGroupAgeGroupId,ActivityName,Zone_ZoneId")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgeGroupAgeGroupId = new SelectList(db.AgeGroups, "AgeGroupId", "AgeGroupName", activity.AgeGroupAgeGroupId);
            ViewBag.Zone_ZoneId = new SelectList(db.Zones, "ZoneId", "ZoneName", activity.Zone_ZoneId);
            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgeGroupAgeGroupId = new SelectList(db.AgeGroups, "AgeGroupId", "AgeGroupName", activity.AgeGroupAgeGroupId);
            ViewBag.Zone_ZoneId = new SelectList(db.Zones, "ZoneId", "ZoneName", activity.Zone_ZoneId);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityId,AgeGroupAgeGroupId,ActivityName")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgeGroupAgeGroupId = new SelectList(db.AgeGroups, "AgeGroupId", "AgeGroupName", activity.AgeGroupAgeGroupId);
            ViewBag.Zone_ZoneId = new SelectList(db.Zones, "ZoneId", "ZoneName", activity.Zone_ZoneId);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        */
    }
}
