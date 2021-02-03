using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        //personel listeleme
        public ActionResult Index()
        {
            var degerler = db.TBLPERSONEL.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            return View();
        }
        //personel ekleme
        [HttpPost]
        public ActionResult PersonelEkle(TBLPERSONEL t)
        {
            if(!ModelState.IsValid)
            {
                return View("PersonelEkle");
            }
            db.TBLPERSONEL.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index", "Personel");
        }
        //seçilen personel bilgilerinin detay bilgilerini görme
        public ActionResult PersonelGetir(int id)
        {
            var k = db.TBLPERSONEL.Find(id);
            return View("PersonelGetir", k);
        }
        //seçilen personelin bilgilerini güncelleme
        public ActionResult Guncelle(TBLPERSONEL per)
        {
            if (!ModelState.IsValid)
            {
                return View("PersonelGetir");
            }
            var k = db.TBLPERSONEL.Find(per.ID);
            k.PERSONEL = per.PERSONEL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}