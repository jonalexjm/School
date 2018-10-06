using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using School.Backend.Models;
using School.Common.Models;
using School.Backend.Helpers;

namespace School.Backend.Controllers
{
    public class UsersController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/User";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }

                var user = this.ToUser(view, pic);

                db.Users.Add(user);
                await db.SaveChangesAsync();

                if (view.IsStudent)
                {
                    //  UsersHelper.CreateUserASP(view.UserName, "Student", view.Password, view.FirstName, view.LastName, pic );
                    UsersHelper.CreateUserASPClaim(view.UserName, "Student", view.Password, view.FirstName, view.LastName, pic);

                }
                if (view.IsTeacher)
                {
                    //UsersHelper.CreateUserASP(view.UserName, "Teacher", view.Password, view.FirstName, view.LastName, pic);
                    UsersHelper.CreateUserASPClaim(view.UserName, "Teacher", view.Password, view.FirstName, view.LastName, pic);
                }



                return RedirectToAction("Index");
            }

            return View(view);
        }

        private User ToUser(UserView view, string pic)
        {
            return new User
            {
                UserName = view.UserName,
                FirstName = view.FirstName,
                Photo = pic,
                LastName = view.LastName,
                Phone = view.Phone,
                UserId = view.UserId,
                IsStudent = view.IsStudent,
                IsTeacher = view.IsTeacher,
            };
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserId,UserName,FirstName,LastName,Phone,Address,IsStudent,IsTeacher,Photo")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
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
