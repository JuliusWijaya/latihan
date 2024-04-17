using TestingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace TestingApp.Controllers
{
    public class StudentController : Controller
    {
        public DbStudentEntities _context = new DbStudentEntities(); 

        [HttpGet]
        public ActionResult Index(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                //ViewBag.Message = message;
                TempData["message"] = message;
            }

            var data = _context.Students.ToList();
            ViewBag.data = data;

            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var jurusanList = _context.Jurusans.ToList();
            ViewBag.jurusanList = jurusanList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Student model)
        {
            _context.Students.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index", "Student", new { message = $"Mahasiswa baru {model.Name} berhasil ditambahkan!"});
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            ViewBag.Student = _context.Students.Where(x => x.Id == id).FirstOrDefault();
       
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Mhs  = _context.Students.Find(id);

            var myEntity = _context;
            var getJurusans = myEntity.Jurusans.ToList();
            SelectList list = new SelectList(getJurusans, "Id", "Nama_Jurusan");
            ViewData["Jurusans"] = list;

            return View();
        }

        [HttpPost]
        public ActionResult Edit(Student student) 
        {
            var mhs = _context.Students.Where(x => x.Id == student.Id).FirstOrDefault();

            mhs.Name = student.Name;
            mhs.Jk = student.Jk;
            mhs.Id_jurusan = student.Id_jurusan;
            _context.SaveChanges();

            TempData["message"] = $"Data {student.Name} berhasil diupdate!";

            return RedirectToAction("Index", "Student");
        }


        [HttpPost]
        public ActionResult Delete(int id) 
        {   
            // Cara 1 menghapus data berdasarkan where kemudian dicocokan
            // Lalu di ambil recordnya
            //var data = db.Students.Where(x => x.Id == id).FirstOrDefault();

            // Cara 2 menghapus record berdasarkan id dengan method find
            var data = _context.Students.Find(id);
            _context.Students.Remove(data);
            _context.SaveChanges();

            return RedirectToAction("Index", "Student", new { message = $"Mahasiswa {data.Name} berhasil didelete!" });
        }
    }
}