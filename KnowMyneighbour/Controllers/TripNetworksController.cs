using KnowMyneighbour.Magic;
using KnowMyneighbour.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace KnowMyneighbour.Controllers
{
    public class TripNetworksController : Controller
    {
        private Entities db = new Entities();

        // GET: TripNetworks
        public ActionResult Index()
        {

            var tripNetworks = db.TripNetworks.Include(t => t.AspNetUser);
            //TempData["tripAdmin"] = tripNetworks
            var TripAdmin = db.TripNetworks.Select(t => t.Trip.TripAdmin).FirstOrDefault();
            var currentUser = User.Identity.GetUserId();
            int? tripsNetworkID = db.TripNetworks.Select(t => t.TripsId).FirstOrDefault();
            ViewBag.CurrentTripNetworkID = tripsNetworkID;
            return View(tripNetworks.ToList());
        }

        // GET: TripNetworks
        public ActionResult IndexById(int? id)
        {
            //var tripNetworks = db.TripNetworks.Include(t => t.AspNetUser);
            var tripNetworks = db.TripNetworks.Where(t => t.TripsId == id);
            //TempData["tripAdmin"] = tripNetworks

            //extract some key columns from the model so this columns can bbe displayed to the user
            var TripAdminID = db.Trips.Where(t => t.Id == id).Select(t => t.TripAdmin).FirstOrDefault();
            ViewBag.TripAdmin = TripAdminID;
            var TripAdminName = db.AspNetUsers.Where(t => t.Id == TripAdminID).Select(t => t.Email).FirstOrDefault();
            ViewBag.TripAdminName = TripAdminName;
            var chargePerTrip = db.Trips.Where(n => n.Id == id).Select(n => n.Charge).FirstOrDefault();
            ViewBag.ChargePerTrip = chargePerTrip;

            var currentUser = User.Identity.GetUserId();
            int? tripsNetworkID = id;
            ViewBag.CurrentTripNetworkID = tripsNetworkID;

            //function to disable pay button
            CheckIfPayButtonShouldBeDisabled(currentUser, TripAdminID, tripsNetworkID);

            return View("Index", tripNetworks.ToList());
        }
        public ActionResult GoToUser(int? id)
        {
            return RedirectToAction("aa");
        }

        public void CheckIfPayButtonShouldBeDisabled(string currentUser, string tripAdmin, int? tripNetworkID)
        {
            if (BLL.hasCurrentUserHasPaidForThisNetwork(currentUser, tripNetworkID))
            {
                ViewBag.DisablePayButtonCosUserHasPaid = true;
            }
            if (BLL.IsCurrentUserTripAdmin(currentUser, tripAdmin))
            {
                ViewBag.DisablePayButtonCosUserIsAnAdmin = true;

            }
        }

        public ActionResult RejectRequest(int? id, int tripsId)
        {
            BLL.RejectRequest(id);
            return RedirectToAction("IndexById", new { id = tripsId });
        }

        public ActionResult AcceptRequest(int? id, int tripsId)
        {
            BLL.AcceptRequest(id);
            return RedirectToAction("IndexById", new { id = tripsId });
        }

        private bool CurrentUserHasPaid()
        {
            throw new NotImplementedException();
        }

        // GET: TripNetworks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TripNetwork tripNetwork = db.TripNetworks.Find(id);
            if (tripNetwork == null)
            {
                return HttpNotFound();
            }
            return View(tripNetwork);
        }

        // GET: TripNetworks/Create
        public ActionResult Create()
        {
            ViewBag.TripMembers = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        public ActionResult CheckPaymentResponse(string tripId, string reference)
        {

            string a = tripId;
            string b = reference;
            if (!string.IsNullOrEmpty(reference))
            {
                //RequestToJoin(Convert.ToInt32(tripId));
                return RedirectToAction("RequestToJoin", "TripNetworks", new { id = Convert.ToInt32(tripId), payRef = reference });
            }
            else
            {
                TempData["success"] = true;
                TempData["AlertMessage"] = "Oh Snap!! That action was not successful";
                return RedirectToAction("IndexById", "TripNetworks", new { id = Convert.ToInt32(tripId) });

            }
        }

        // GET: TripNetworks/Create
        public ActionResult RequestToJoin(int? id, string payRef)
        {
            //          var testOrLiveSecret = ConfigurationManager.AppSettings["PayStackSecret"];
            //          var api = new PayStackApi(testOrLiveSecret);
            //          // Initializing a transaction
            //          var response = api.Transactions.Initialize("loladeking@gmail.com", 5000000);
            //          if (response.Status)
            //          {

            //          }
            //// use response.Data
            //          else
            //          {

            //          }
            // show response.Message



            string currentUser = User.Identity.GetUserId();
            TripNetwork tripNetwork = new TripNetwork() { TripsId = id, TripMembers = currentUser, PaymentReference = payRef, DatePaid = DateTime.Now, Paid = true };
            DAL.TripNetwk dt = new DAL.TripNetwk();
            var returnedElement = dt.CreateTripNetwork(tripNetwork);
            //var tripnetworkbyId = db.TripNetworks.Where(y => y.TripsId == id);
            //RedirectToAction("RequestToJoin", new { id = id });
            return RedirectToAction("IndexById", new { id = id });
            //return View("Index", tripnetworkbyId);
        }

        // POST: TripNetworks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TripsId,TripMembers,DateAdded,ActiveStatus,DateModified")] TripNetwork tripNetwork)
        {
            if (ModelState.IsValid)
            {
                string currentUser = User.Identity.GetUserId();
                tripNetwork.TripMembers = currentUser;
                DAL.TripNetwk dt = new DAL.TripNetwk();
                TempData["AlertMessage"] = dt.CreateTripNetwork(tripNetwork);
                //db.TripNetworks.Add(tripNetwork);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TripMembers = new SelectList(db.AspNetUsers, "Id", "Email", tripNetwork.TripMembers);
            return View(tripNetwork);
        }

        // GET: TripNetworks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TripNetwork tripNetwork = db.TripNetworks.Find(id);
            if (tripNetwork == null)
            {
                return HttpNotFound();
            }
            ViewBag.TripMembers = new SelectList(db.AspNetUsers, "Id", "Email", tripNetwork.TripMembers);
            return View(tripNetwork);
        }

        // POST: TripNetworks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TripsId,TripMembers,DateAdded,ActiveStatus,DateModified")] TripNetwork tripNetwork)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tripNetwork).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TripMembers = new SelectList(db.AspNetUsers, "Id", "Email", tripNetwork.TripMembers);
            return View(tripNetwork);
        }

        // GET: TripNetworks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TripNetwork tripNetwork = db.TripNetworks.Find(id);
            if (tripNetwork == null)
            {
                return HttpNotFound();
            }
            return View(tripNetwork);
        }

        // POST: TripNetworks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TripNetwork tripNetwork = db.TripNetworks.Find(id);
            db.TripNetworks.Remove(tripNetwork);
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
