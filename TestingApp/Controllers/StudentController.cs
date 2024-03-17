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

            return RedirectToAction("Index", "Student", new { message = $"Mahasiswa baru {model.Name} berhasil ditambahkan!"});
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            ViewBag.Student = db.Students.Where(x => x.Id == id).FirstOrDefault();

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Mhs  = db.Students.Find(id);

            var myEntity = db;
            var getJurusans = myEntity.Jurusans.ToList();
            SelectList list = new SelectList(getJurusans, "Id", "Nama_Jurusan");
            ViewData["Jurusans"] = list;

            return View();
        }

        [HttpPost]
        public ActionResult Edit(Student student) 
        {
            var mhs = db.Students.Where(x => x.Id == student.Id).FirstOrDefault();

            mhs.Name = student.Name;
            mhs.Jk = student.Jk;
            mhs.Id_jurusan = student.Id_jurusan;
            db.SaveChanges();

            TempData["message"] = $"Data {student.Name} berhasil diupdate!";

            return RedirectToAction("Index", "Student");
        }


        [HttpPost]
        public ActionResult Delete(int id) 
        {   
            //Cara 1 menghapus data berdasarkan where kemudian dicocokan
            //Lalu di ambil recordnya
            //var data = db.Students.Where(x => x.Id == id).FirstOrDefault();

            //Cara 2 menghapus record berdasarkan id dengan method find
            var data = db.Students.Find(id);
            db.Students.Remove(data);
            db.SaveChanges();

            return RedirectToAction("Index", "Student", new { message = $"Mahasiswa {data.Name} berhasil didelete!" });
        }
    }
}