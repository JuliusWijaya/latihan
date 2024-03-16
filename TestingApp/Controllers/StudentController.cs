using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingApp.Models;

namespace TestingApp.Controllers
{
    public class StudentController : Controller
    {
        public DbStudentEntities db = new DbStudentEntities(); 
        // GET: Student

        [HttpGet]
        public ActionResult Index(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                //ViewBag.Message = message;
                TempData["message"] = message;
            }

            var data = db.Students.ToList();

            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            DbStudentEntities myEntity = new DbStudentEntities();
            var getJurusans = myEntity.Jurusans.ToList();
            SelectList list = new SelectList(getJurusans, "Id", "Nama_Jurusan");
            ViewData["Jurusans"] = list;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Student model)
        {
            db.Students.Add(model);
            db.SaveChanges();

            return RedirectToAction("Index", "Student", new { message = $"Mahasiswa {model.Name} baru berhasil ditambahkan!"});
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            ViewBag.Student = db.Students.Where(x => x.Id == id).FirstOrDefault();

            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id) 
        {
            var data = db.Students.Where(x => x.Id == id).FirstOrDefault();
            db.Students.Remove(data);
            db.SaveChanges();

            return RedirectToAction("Index", "Student", new { message = $"Mahasiswa {data.Name} berhasil didelete!" });
        }
    }
}