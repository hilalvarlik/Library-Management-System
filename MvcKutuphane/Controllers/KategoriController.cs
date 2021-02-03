using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLKATEGORI.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        //kategori ekleme
        [HttpPost]
        public ActionResult KategoriEkle(TBLKATEGORI d1)
        {
            if (!ModelState.IsValid)
            {
                return View("KategoriEkle");
            }
            db.TBLKATEGORI.Add(d1);
            db.SaveChanges();
            return RedirectToAction("Index", "Kategori");
        }
        //kategori silme
        public ActionResult KategoriSil(int id)
        {
            var kategori = db.TBLKATEGORI.Find(id);
            db.TBLKATEGORI.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //güncellenecek kategorinin detaylarına girildiğinde bilgilerin getirilmesi için
        public ActionResult KategoriGetir(int id)
        {
            //idye göre kitap bilgileri
            var k = db.TBLKATEGORI.Find(id);
            return View("KategoriGetir", k);
        }
        //kategori güncelleme
        public ActionResult Guncelle(TBLKATEGORI ktg)
        {
            if (!ModelState.IsValid)
            {
                return View("KategoriGetir");
            }
            var k = db.TBLKATEGORI.Find(ktg.ID);
            k.AD = ktg.AD;
            db.SaveChanges();
            //güncelleme yaptıktan sonra bir önceki sayfaya geri döner
            return RedirectToAction("Index");
        }
    }
}