using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewApplication;

namespace NewApplication.Controllers
{
    public class SessionsController : Controller
    {
        private DBModelContainer db = new DBModelContainer();

        // GET: Sessions
        public ActionResult Index()
        {
            var sessions = db.Sessions.Include(s => s.Activity).Include(s => s.SessionGroup);
            return View(sessions.ToList());
        }

        // GET: Sessions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }
        /*
        // GET: Sessions/Create
        public ActionResult Create()
        {
            ViewBag.ActivityActivityId = new SelectList(db.Activities, "ActivityId", "ActivityName");
            ViewBag.SessionGroupSessionGroupId = new SelectList(db.SessionGroups, "SessionGroupId", "SessionName");
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SessionId,ZoneZoneId,SessionGroupSessionGroupId,ActivityActivityId,SessionName")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Sessions.Add(session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityActivityId = new SelectList(db.Activities, "ActivityId", "ActivityName", session.ActivityActivityId);
            ViewBag.SessionGroupSessionGroupId = new SelectList(db.SessionGroups, "SessionGroupId", "SessionName", session.SessionGroupSessionGroupId);
            return View(session);
        }
       
        // GET: Sessions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityActivityId = new SelectList(db.Activities, "ActivityId", "ActivityName", session.ActivityActivityId);
            ViewBag.SessionGroupSessionGroupId = new SelectList(db.SessionGroups, "SessionGroupId", "SessionName", session.SessionGroupSessionGroupId);
            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SessionId,ZoneZoneId,SessionGroupSessionGroupId,ActivityActivityId,SessionName")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityActivityId = new SelectList(db.Activities, "ActivityId", "ActivityName", session.ActivityActivityId);
            ViewBag.SessionGroupSessionGroupId = new SelectList(db.SessionGroups, "SessionGroupId", "SessionName", session.SessionGroupSessionGroupId);
            return View(session);
        }

        // GET: Sessions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Session session = db.Sessions.Find(id);
            db.Sessions.Remove(session);
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
