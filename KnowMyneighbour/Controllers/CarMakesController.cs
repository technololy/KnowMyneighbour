using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KnowMyneighbour.Models;

namespace KnowMyneighbour.Controllers
{
    public class CarMakesController : Controller
    {
        private Entities db = new Entities();

        // GET: CarMakes
        public async Task<ActionResult> Index()
        {
            var carMakes = db.CarMakes.Include(c => c.Vehicle);
            return View(await carMakes.ToListAsync());
        }

        // GET: CarMakes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarMake carMake = await db.CarMakes.FindAsync(id);
            if (carMake == null)
            {
                return HttpNotFound();
            }
            return View(carMake);
        }

        // GET: CarMakes/Create
        public ActionResult Create()
        {
            ViewBag.VehicleTypeId = new SelectList(db.Vehicles, "Id", "VehicleType");
            return View();
        }

        // POST: CarMakes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CarMaker,VehicleTypeId")] CarMake carMake)
        {
            if (ModelState.IsValid)
            {
                db.CarMakes.Add(carMake);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.VehicleTypeId = new SelectList(db.Vehicles, "Id", "VehicleType", carMake.VehicleTypeId);
            return View(carMake);
        }

        // GET: CarMakes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarMake carMake = await db.CarMakes.FindAsync(id);
            if (carMake == null)
            {
                return HttpNotFound();
            }
            ViewBag.VehicleTypeId = new SelectList(db.Vehicles, "Id", "VehicleType", carMake.VehicleTypeId);
            return View(carMake);
        }

        // POST: CarMakes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CarMaker,VehicleTypeId")] CarMake carMake)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carMake).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.VehicleTypeId = new SelectList(db.Vehicles, "Id", "VehicleType", carMake.VehicleTypeId);
            return View(carMake);
        }

        // GET: CarMakes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarMake carMake = await db.CarMakes.FindAsync(id);
            if (carMake == null)
            {
                return HttpNotFound();
            }
            return View(carMake);
        }

        // POST: CarMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CarMake carMake = await db.CarMakes.FindAsync(id);
            db.CarMakes.Remove(carMake);
            await db.SaveChangesAsync();
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
