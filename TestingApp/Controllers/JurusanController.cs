using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingApp.Models;

namespace TestingApp.Controllers
{

    public class JurusanController : Controller
    {
        private DbStudentEntities _context = new DbStudentEntities();
        // GET: Jurusan
        public ActionResult Index(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                //ViewBag.Message = message;
                TempData["message"] = message;
            }

            var jurusans = _context.Jurusans.ToList();

            return View(jurusans);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Jurusan model)
        {
            _context.Jurusans.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index", "Jurusan", new { message = $"Data {model.Id_Jurusan} berhasil disimpan" });
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            ViewBag.Jrs = _context.Jurusans.Where(x => x.Id == id).FirstOrDefault();

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Jrs = _context.Jurusans.Where(x => x.Id == id).FirstOrDefault();

            return View();
        }

        [HttpPut]
        public ActionResult Edit(Jurusan model)
        {
           var data = _context.Jurusans.Where(x => x.Id == model.Id).FirstOrDefault();

            if(data != null)
            {
                data.Id_Jurusan = model.Id_Jurusan;
                data.Nama_Jurusan = model.Nama_Jurusan;
                _context.SaveChanges();
            } else
            {
                TempData["message-error"] = $"Data {model.Id_Jurusan} gagal diupdate!";
                return RedirectToAction("Index");
            }

            TempData["message"] = $"Data {model.Id_Jurusan} berhasil diupdate";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var data = _context.Jurusans.Where(x => x.Id == id).FirstOrDefault();
            _context.Jurusans.Remove(data);
            _context.SaveChanges();

            return RedirectToAction("Index", "Jurusan", new { message = $"Data {data.Nama_Jurusan} berhasil dihapus" });
        }
    }
}