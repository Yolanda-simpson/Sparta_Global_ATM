using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AtmApplication.Models;

namespace AtmApplication.Controllers
{
    public class LoginController : Controller
    {
        private ATMDBEntities1 db = new ATMDBEntities1();

        // GET: Login
        public ActionResult Login()
        {
            
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        //POST: Login
        [HttpPost]
        public ActionResult Login(User user)
        {
            User usr = db.Users.FirstOrDefault(u => u.CustomerNumber == (user.CustomerNumber) && u.Pin == (user.Pin));
            if (usr == null || usr.Pin == null)
            {
                return RedirectToAction("Create", "Users");
            }


            //user directed to own details 
            return View("Details", usr);


        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");

        }

        // GET: Login/Create
        public ActionResult Create()
        {
            ViewBag.CardID = new SelectList(db.Cards, "CardId", "CardType");
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsersId,FirstName_,LastName,TelephoneNumber,AddressLine1,AddressLine2,AddressLine3,CustomerNumber,Pin,CardID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CardID = new SelectList(db.Cards, "CardId", "CardType", user.CardID);
            return View(user);
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CardID = new SelectList(db.Cards, "CardId", "CardType", user.CardID);
            return View(user);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsersId,FirstName_,LastName,TelephoneNumber,AddressLine1,AddressLine2,AddressLine3,CustomerNumber,Pin,CardID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CardID = new SelectList(db.Cards, "CardId", "CardType", user.CardID);
            return View(user);
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
