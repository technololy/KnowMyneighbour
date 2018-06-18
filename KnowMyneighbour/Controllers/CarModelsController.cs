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
    public class CarModelsController : Controller
    {
        private Entities db = new Entities();

        // GET: CarModels
        public async Task<ActionResult> Index()
        {
            var carModels = db.CarModels.Include(c => c.CarMake);
            return View(await carModels.ToListAsync());
        }

        // GET: CarModels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModel carModel = await db.CarModels.FindAsync(id);
            if (carModel == null)
            {
                return HttpNotFound();
            }
            return View(carModel);
        }

        // GET: CarModels/Create
        public ActionResult Create()
        {
            ViewBag.CarMaker = new SelectList(db.CarMakes, "Id", "CarMaker");
            return View();
        }

        // POST: CarModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CarMaker")] CarModel carModel)
        {
            if (ModelState.IsValid)
            {
                db.CarModels.Add(carModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CarMaker = new SelectList(db.CarMakes, "Id", "CarMaker", carModel.CarMaker);
            return View(carModel);
        }

        // GET: CarModels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModel carModel = await db.CarModels.FindAsync(id);
            if (carModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarMaker = new SelectList(db.CarMakes, "Id", "CarMaker", carModel.CarMaker);
            return View(carModel);
        }

        // POST: CarModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CarMaker")] CarModel carModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CarMaker = new SelectList(db.CarMakes, "Id", "CarMaker", carModel.CarMaker);
            return View(carModel);
        }

        // GET: CarModels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModel carModel = await db.CarModels.FindAsync(id);
            if (carModel == null)
            {
                return HttpNotFound();
            }
            return View(carModel);
        }

        // POST: CarModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CarModel carModel = await db.CarModels.FindAsync(id);
            db.CarModels.Remove(carModel);
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
