using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using penztarca.Models;
using Microsoft.AspNet.Identity;

namespace penztarca.Controllers
{
    public class penzTarcasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: penzTarcas
        public ActionResult Index()
        {
            //  string currentUserId = User.Identity.GetUserId();
            // ApplicationUser currentUser = db.Users.FirstOrDefault
            //       (x => x.Id == currentUserId);
            //  return View(db.penzTarca.ToList().Where(x => x.User == currentUser));
            return View();
        }

        private IEnumerable<penzTarca> GetMypenzTarca()
        {
             string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault
                (x => x.Id == currentUserId);
            if(currentUser.UserName == "admin@admin.hu") {
                return db.penzTarca.ToList();
            }
            return db.penzTarca.ToList().Where(x => x.User == currentUser);
        }


        public ActionResult BuildpenzTarcaTable()
        {
           
            return PartialView("_penzTarcaTable", GetMypenzTarca());
        }
        // GET: penzTarcas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            penzTarca penzTarca = db.penzTarca.Find(id);
            if (penzTarca == null)
            {
                return HttpNotFound();
            }
            return View(penzTarca);
        }

        // GET: penzTarcas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: penzTarcas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Count,IsDone")] penzTarca penzTarca)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(
                    x => x.Id == currentUserId);
                penzTarca.User = currentUser;
                db.penzTarca.Add(penzTarca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(penzTarca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AJAXCreate([Bind(Include = "Id,Description,Count")] penzTarca penzTarca)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(
                    x => x.Id == currentUserId);
                penzTarca.User = currentUser;
                penzTarca.IsDone = false;
                db.penzTarca.Add(penzTarca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView("_penzTarcaTable", GetMypenzTarca());
            // return View(penzTarca);
        }

        // GET: penzTarcas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            penzTarca penzTarca = db.penzTarca.Find(id);
            if (penzTarca == null)
            {
                return HttpNotFound();
            }
            return View(penzTarca);
        }

        // POST: penzTarcas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Count,IsDone")] penzTarca penzTarca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(penzTarca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(penzTarca);
        }

        // GET: penzTarcas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            penzTarca penzTarca = db.penzTarca.Find(id);
            if (penzTarca == null)
            {
                return HttpNotFound();
            }
            return View(penzTarca);
        }

        // POST: penzTarcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            penzTarca penzTarca = db.penzTarca.Find(id);
            db.penzTarca.Remove(penzTarca);
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
