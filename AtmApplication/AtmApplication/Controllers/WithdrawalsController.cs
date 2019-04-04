using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AtmApplication;

namespace AtmApplication.Controllers
{
    public class WithdrawalsController : Controller
    {
        private ATMDBEntities db = new ATMDBEntities();

        // GET: Withdrawals
        public ActionResult Index()
        {
            return View(db.Withdrawals.ToList());
        }

        // GET: Withdrawals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Withdrawal withdrawal = db.Withdrawals.Find(id);
            if (withdrawal == null)
            {
                return HttpNotFound();
            }
            return View(withdrawal);
        }

        // GET: Withdrawals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Withdrawals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WithdrawalAmount,WithdrawalDate")] Withdrawal withdrawal)
        {
            if (ModelState.IsValid)
            {
                db.Withdrawals.Add(withdrawal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(withdrawal);
        }

        // GET: Withdrawals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Withdrawal withdrawal = db.Withdrawals.Find(id);
            if (withdrawal == null)
            {
                return HttpNotFound();
            }
            return View(withdrawal);
        }

        // POST: Withdrawals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WithdrawalAmount,WithdrawalDate")] Withdrawal withdrawal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(withdrawal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(withdrawal);
        }

        // GET: Withdrawals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Withdrawal withdrawal = db.Withdrawals.Find(id);
            if (withdrawal == null)
            {
                return HttpNotFound();
            }
            return View(withdrawal);
        }

        // POST: Withdrawals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Withdrawal withdrawal = db.Withdrawals.Find(id);
            db.Withdrawals.Remove(withdrawal);
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
