using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class UyeController : Controller
    {
        // GET: Uye
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        //üye listesinin getirilmesi
        public ActionResult Index(int sayfa=1)
        {
            //verilerin sayfalama ile görüntülenmesi
            var degerler = db.TBLUYELER.ToList().ToPagedList(sayfa ,3);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult UyeEkle()
        {
            return View();
        }
        //üye ekleme
        [HttpPost]
        public ActionResult UyeEkle(TBLUYELER t)
        {
            if (!ModelState.IsValid)
            {
                return View("UyeEkle");
            }
            db.TBLUYELER.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index", "Uye");
        }
        //seçilen üyenin detay bilgilerini görmek için
        public ActionResult UyeGetir(int id)
        {
            var k = db.TBLUYELER.Find(id);
            return View("UyeGetir", k);
        }
        //üye güncelleme
        public ActionResult Guncelle(TBLUYELER t)
        {
            if (!ModelState.IsValid)
            {
                return View("UyeGetir");
            }
            var k = db.TBLUYELER.Find(t.ID);
            k.AD = t.AD;
            k.SOYAD = t.SOYAD;
            k.MAIL = t.MAIL;
            k.KULLANICIADI = t.KULLANICIADI;
            k.SIFRE = t.SIFRE;
            k.FOTOGRAF = t.FOTOGRAF;
            k.TELEFON = t.TELEFON;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //üyenin daha önce almış olduğu kitap listesi
        public ActionResult UyeKitapGecmis(int id)
        {
            //hareket tablosundan üye id ye göre alınmış olan kitapları listeler 
            var ktpgecmis = db.TBLHAREKET.Where(x => x.UYE == id).ToList();
            var uyekit = db.TBLUYELER.Where(y => y.ID == id).Select(z => z.AD + " " + z.SOYAD).FirstOrDefault();
            ViewBag.u1 = uyekit;
            return View(ktpgecmis);
        }
    }
}