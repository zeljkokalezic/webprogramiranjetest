using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Students
        public ActionResult Index(string sortBy = "Name", string sortOrder = "ASC", int page = 1, int pageSize = 10, string search = "")
        {
            ////reset paging when query changes
            //if (Request.UrlReferrer != null)
            //{
            //    var oldSearch = HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["Search"];
            //    if (search != oldSearch)
            //    {
            //        page = 1;
            //    }
            //}

            //This is not a production ready code - we need edge case handling and exception handling
            //first we filter and order the list
            var students = db.Students
                            .Where(x => x.Name.Contains(search))
                            .OrderBy(string.Format("{0} {1}", sortBy, sortOrder));
            //then we get total count
            var count = students.Count();
            //then we page the records
            students = students.Skip((page - 1) * pageSize).Take(pageSize);

            var model = new StudentGridViewModel()
            {
                SortOrder = sortOrder,
                SortBy = sortBy,
                Page = page,
                PageSize = pageSize,
                Search = search,
                Students = students.ToList(),
                Count = count
            };

            return View(model);
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            SetCoursesVariable();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,Graduated,YearOfStudy")] Student student, int[] courses)
        {
            if (ModelState.IsValid)
            {
                student.Courses = db.Courses.Where(x => courses.Contains(x.Id)).ToList();
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            SetCoursesVariable();
            return View(student);
        }

        private void SetCoursesVariable()
        {
            ViewBag.Courses = db.Courses.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList<SelectListItem>();
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.Entry(student.Address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Entry(student).State = EntityState.Deleted;
            //db.Students.Remove(student);
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
