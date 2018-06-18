using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KnowMyneighbour.Models;

namespace KnowMyneighbour.Controllers
{
    public class SocialMediaRequestsController : Controller
    {
        private Entities db = new Entities();

        // GET: SocialMediaRequests
        public ActionResult Index()
        {
            var socialMediaRequests = db.SocialMediaRequests.Include(s => s.AspNetUser).Include(s => s.Trip);
            return View(socialMediaRequests.ToList());
        }

        public ActionResult inviteSocialMediaFriends(int? id)
        {
            ViewBag.tripid = id;
            return RedirectToAction("SocialMediaChoice");
        }

        public ActionResult SocialMediaChoice()
        {
            return View("ChooseSM");
        }


        public ActionResult GetGoogleContacts()
        {
            List<SocialMediaRequest> sm = new List<SocialMediaRequest>();
            SocialMediaRequest smr = new SocialMediaRequest() { FriendName="ololade ahmed oyebanji",FriendsEmail="loladeking@gmail.com"};
            sm.Add(smr);
            ViewBag.EmailMessage = "   Hi there Damilola, this is lolade. I am driving from Okota to CMS tomorrow 21 January 2017. Let's share this trip together. Click on this link, pay 200 naira and meet me at the pick up LandMark, Church Roundabout. Destination LandMark is CMS. Join my trip network so we go together.";
            return View("index",sm);
        }


        public ActionResult InviteGoogleFriend(int? id)
        {
            return View();
        }

        // GET: SocialMediaRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMediaRequest socialMediaRequest = db.SocialMediaRequests.Find(id);
            if (socialMediaRequest == null)
            {
                return HttpNotFound();
            }
            return View(socialMediaRequest);
        }

        // GET: SocialMediaRequests/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.TripId = new SelectList(db.Trips, "Id", "TripAdmin");
            return View();
        }

        // POST: SocialMediaRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "refid,SocialMedia,FriendName,FriendsEmail,dateAdded,dateModified,UserId,TripId")] SocialMediaRequest socialMediaRequest)
        {
            if (ModelState.IsValid)
            {
                db.SocialMediaRequests.Add(socialMediaRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", socialMediaRequest.UserId);
            ViewBag.TripId = new SelectList(db.Trips, "Id", "TripAdmin", socialMediaRequest.TripId);
            return View(socialMediaRequest);
        }

        // GET: SocialMediaRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMediaRequest socialMediaRequest = db.SocialMediaRequests.Find(id);
            if (socialMediaRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", socialMediaRequest.UserId);
            ViewBag.TripId = new SelectList(db.Trips, "Id", "TripAdmin", socialMediaRequest.TripId);
            return View(socialMediaRequest);
        }

        // POST: SocialMediaRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "refid,SocialMedia,FriendName,FriendsEmail,dateAdded,dateModified,UserId,TripId")] SocialMediaRequest socialMediaRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(socialMediaRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", socialMediaRequest.UserId);
            ViewBag.TripId = new SelectList(db.Trips, "Id", "TripAdmin", socialMediaRequest.TripId);
            return View(socialMediaRequest);
        }

        // GET: SocialMediaRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMediaRequest socialMediaRequest = db.SocialMediaRequests.Find(id);
            if (socialMediaRequest == null)
            {
                return HttpNotFound();
            }
            return View(socialMediaRequest);
        }

        // POST: SocialMediaRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SocialMediaRequest socialMediaRequest = db.SocialMediaRequests.Find(id);
            db.SocialMediaRequests.Remove(socialMediaRequest);
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
    }
}
