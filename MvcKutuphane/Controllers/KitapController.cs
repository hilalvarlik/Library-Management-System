using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;


namespace MvcKutuphane.Controllers
{
    public class KitapController : Controller
    {
        // GET: Kitap
        //Veritabanı nesnesi oluştur
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        //Kütüphanede bulunan kitapların listelenmesi için
        public ActionResult Index(string p)
        {
            //kitap adına göre arama yapar
            var degerler = from k in db.TBLKITAP select k;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.AD.Contains(p));
            }
            //var degerler = db.TBLKITAP.ToList();
            return View(degerler.ToList());
        }
        //sayfa yüklendiğinde ekleme işlemini direkt olarak yapmaması için bu method kullanılır
        [HttpGet]
        public ActionResult KitapEkle()
        {
            //kitap eklerken kategori değerini dropdown listten alır
            List<SelectListItem> degerler = (from i in db.TBLKATEGORI.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.AD,
                                                 Value = i.ID.ToString()
                                             }).ToList();
            ViewBag.KAT = degerler;
            //kitap eklerken yazar değerini dropdown listten alır
            List<SelectListItem> degerler1 = (from i in db.TBLYAZAR.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.AD + ' ' + i.SOYAD,
                                                  Value = i.ID.ToString()
                                              }).ToList();

            ViewBag.YAZ = degerler1;
            
            return View();
        }
        //değer girildiğinde ekleme işlemi yapması için bu method kullanılır
        [HttpPost]
        public ActionResult KitapEkle(TBLKITAP h1)
        {
            var k = db.TBLKATEGORI.Where(m => m.ID == h1.TBLKATEGORI.ID).FirstOrDefault();
            h1.TBLKATEGORI = k;
            var c = db.TBLYAZAR.Where(m => m.ID == h1.TBLYAZAR.ID).FirstOrDefault();
            h1.TBLYAZAR = c;
            db.TBLKITAP.Add(h1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //kitap silme işlemi
        public ActionResult KitapSil(int id)
        {
            var y = db.TBLKITAP.Find(id);
            db.TBLKITAP.Remove(y);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //güncelleme yapılıcak kitabın detaylarına girildiğinde bilgilerinin gelmesi için
        public ActionResult KitapGetir(int id)
        {

            List<SelectListItem> degerler = (from i in db.TBLKATEGORI.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.AD,
                                                 Value = i.ID.ToString()
                                             }).ToList();
            ViewBag.KAT = degerler;
            List<SelectListItem> degerler1 = (from i in db.TBLYAZAR.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.AD + ' ' +  i.SOYAD,
                                                  Value = i.ID.ToString()
                                              }).ToList();

            ViewBag.YAZ = degerler1;
            var hs = db.TBLKITAP.Find(id);
            return View("KitapGetir", hs);

        }
        //güncelleme işlemi
        public ActionResult Guncelle(TBLKITAP yz)
        {
            if (!ModelState.IsValid)
            {
                return View("KitapGetir");
            }
            var y = db.TBLKITAP.Find(yz.ID);
            y.AD = yz.AD;
            y.KATEGORI = yz.KATEGORI;
            y.YAZAR = yz.YAZAR;
            y.BASIMYIL = yz.BASIMYIL;
            y.YAYINEVI = yz.YAYINEVI;
            y.SAYFA = yz.SAYFA;
            y.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}