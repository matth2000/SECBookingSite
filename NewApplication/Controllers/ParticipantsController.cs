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
    public class ParticipantsController : Controller
    {
        private DBModelContainer db = new DBModelContainer();
        /*
        // GET: Participants
        public ActionResult Index()
        {
            var participants = db.Participants.Include(p => p.Club);
            return View(participants.ToList());
        }
        */
        // GET: Participants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }

        public PartialViewResult Sessions(int? id)
        {
            ViewBag.ParticipantId = id;
            List<SessionGroup> sessionGroups = db.SessionGroups.ToList();
            return PartialView(sessionGroups);
        }

        public PartialViewResult SessionActivities(int? sessionGroupId, int participantId)
        {
            ViewBag.ParticipantId = participantId;
            Participant participant = db.Participants.Find(participantId);
            ViewBag.Participant = participant;
            int ageGroupId = participant.AgeGroup_AgeGroupId;
            List<Session> sessions = db.Sessions.Where(session => session.SessionGroupSessionGroupId == sessionGroupId).ToList();
            List<Session> unbookedSession = sessions.Where(session => !session.Participants.Contains(participant)).ToList();
            List<Session> validSessions = sessions.Where(session => session.Participants.Contains(participant)).ToList();
            if(validSessions.Count() > 0)
            {
                List<Session> newSessions = validSessions.Where(session => session.Activity.AgeGroupAgeGroupId == ageGroupId).ToList();
                IEnumerable<Session> allParticipantSessions = participant.Sessions;
                return PartialView(newSessions);
            }
            else
            {
                List<Session> testSession = db.GetParticipantSessions(participantId).Where(session => session.SessionGroupSessionGroupId == sessionGroupId).ToList();
                List<Session> newSessions = testSession.Where(session => session.Activity.AgeGroupAgeGroupId == ageGroupId).ToList();
                IEnumerable<Session> allParticipantSessions = participant.Sessions;
                return PartialView(newSessions);
            }
            
        }
        
        public bool AddParticpant(int sessionId, int participantId)
        {
            Session session = db.Sessions.Find(sessionId);
            Participant participant = db.Participants.Find(participantId);
            if (!session.Participants.Contains(participant))
            {
                session.Participants.Add(participant);
            }
            db.Entry(session).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public bool RemoveParticipant(int sessionId, int participantId)
        {
            Session session = db.Sessions.Find(sessionId);
            Participant participant = db.Participants.Find(participantId);
            if (session.Participants.Contains(participant))
            {
                session.Participants.Remove(participant);
            }
            db.Entry(session).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
        /*
        // GET: Participants/Create
        public ActionResult Create()
        {
            ViewBag.ClubClubId = new SelectList(db.Clubs, "ClubId", "ClubName");
            return View();
        }

        // POST: Participants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParticipantId,ClubClubId,ParticipantName")] Participant participant)
        {
            if (ModelState.IsValid)
            {
                db.Participants.Add(participant);
                db.SaveChanges();
                return RedirectToAction("Details", "Clubs",  new { id = participant.ClubClubId });
            }

            ViewBag.ClubClubId = new SelectList(db.Clubs, "ClubId", "ClubName", participant.ClubClubId);
            Club club = db.Clubs.Find(participant.ClubClubId);
            return View("Details", "Clubs", club);
        }
*/
        // GET: Participants/Edit/5
        public ActionResult Edit(int? id, int clubId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClubClubId = clubId;
            ViewBag.AgeGroup_AgeGroupId = new SelectList(db.AgeGroups, "AgeGroupId", "AgeGroupName", participant.AgeGroup_AgeGroupId);
            return View(participant);
        }

        // POST: Participants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParticipantId,ClubClubId,ParticipantName,AgeGroup_AgeGroupId")] Participant participant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(participant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Clubs", new { id = participant.ClubClubId });
            }
            ViewBag.AgeGroup_AgeGroupId = new SelectList(db.AgeGroups, "AgeGroupId", "AgeGroupName", participant.AgeGroup_AgeGroupId);
            Club club = db.Clubs.Find(participant.ClubClubId);
            return RedirectToAction("Details", "Clubs", new { id = participant.ClubClubId });
        }

        public ActionResult ParticipantSessions(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }
        /*
        // GET: Participants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }

        // POST: Participants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Participant participant = db.Participants.Find(id);
            db.Participants.Remove(participant);
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
