using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class YazarController : Controller
    {
        // GET: Yazar
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        //yazra listeleme
        public ActionResult Index()
        {
            var degerler = db.TBLYAZAR.ToList();
            return View(degerler);
        }
        
        [HttpGet]
        public ActionResult YazarEkle()
        {
            if (!ModelState.IsValid)
            {
                return View("YazarEkle");
            }
            return View();
        }
        //yazar ekleme
        [HttpPost]
        public ActionResult YazarEkle(TBLYAZAR d1)
        {
            if (!ModelState.IsValid)
            {
                return View("YazarEkle");
            }
            db.TBLYAZAR.Add(d1);
            db.SaveChanges();
            return RedirectToAction("Index", "Yazar");
        }
        //yazar silme
        public ActionResult YazarSil(int id)
        {
            var y = db.TBLYAZAR.Find(id);
            db.TBLYAZAR.Remove(y);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //güncellenecek yazar detayına girildiğinde bilgilerini getirme
        public ActionResult YazarGetir(int id)
        {
            var k = db.TBLYAZAR.Find(id);
            return View("YazarGetir", k);
        }
        //yazar güncelleme
        public ActionResult Guncelle(TBLYAZAR yz)
        {
            if (!ModelState.IsValid)
            {
                return View("YazarGetir");
            }
            var y = db.TBLYAZAR.Find(yz.ID);
            y.AD = yz.AD;
            y.SOYAD = yz.SOYAD;
            y.DETAY = yz.DETAY;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //yazara ait tüm kitapların listelenmesi
        public ActionResult YazarKitaplar(int id)
        {
            var yazar = db.TBLKITAP.Where(x => x.YAZAR == id).ToList();
            var yzrad = db.TBLYAZAR.Where(y => y.ID == id).Select(z => z.AD + " " + z.SOYAD).FirstOrDefault();
            ViewBag.y1 = yzrad;
            return View(yazar);
        }
    }
}
