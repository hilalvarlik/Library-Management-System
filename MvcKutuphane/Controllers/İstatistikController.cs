using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class İstatistikController : Controller
    {
        // GET: İstatistik
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        //istatistik değerlerinin listelenmesi
        public ActionResult Index()
        {
            //sistemdeki üye sayısı
            var deger1 = db.TBLUYELER.Count();
            //kütüphanedeki kitap sayısı
            var deger2 = db.TBLKITAP.Count();
            //ödünç verilen kitap sayısı
            var deger3 = db.TBLKITAP.Where(x => x.DURUM == false).Count();
            //geç gelen kitapardan alınan cezalarla kasada biriken toplam tutar
            var deger4 = db.TBLCEZALAR.Sum(x => x.PARA);
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger3;
            return View();
        }

        public ActionResult Galeri()
        {
            return View();
        }
        //cihazdan kütüphanenin resimlerini yükleme
        public ActionResult ResimYukle(HttpPostedFileBase dosya)
        {
            if (dosya.ContentLength > 0)
            {
                string dosyayolu = Path.Combine(Server.MapPath("~/web2/resimler/"), Path.GetFileName(dosya.FileName));
                dosya.SaveAs(dosyayolu);
            }
            return RedirectToAction("Galeri");
        }
        public ActionResult LinqKart()
        {
            //Toplam kitap sayısını getirir.
            var deger1 = db.TBLKITAP.Count();
            //Toplam üye sayısını getirir.
            var deger2 = db.TBLUYELER.Count();
            //Cezadan dolayı kasada oluşan tutarı getirir.
            var deger3 = db.TBLCEZALAR.Sum(x => x.PARA);
            //Kitaplar listesinde aktif olmayan kitapları getirir.
            var deger4 = db.TBLKITAP.Where(x => x.DURUM == false).Count();
            //Toplam kategori sayısını getirir.
            var deger5 = db.TBLKATEGORI.Count();
            //En fazla kitap işlemi yapan yani en aktif üye
            var deger6 = db.EnAktifUye().FirstOrDefault();
            //En çok tercih edilen kitap
            var deger7 = db.EnCokOkunanKitap().FirstOrDefault();
            //Kütüphanede en çok kitabı bulunan yazar
            var deger8=db.EnFazlaKitapYazar().FirstOrDefault();
            //Kütüphanede en çok bulunan yayınevi
            var deger9 = db.TBLKITAP.GroupBy(x => x.YAYINEVI).OrderByDescending(z => z.Count()).Select
                (y => new { y.Key }).FirstOrDefault();
            var deger10 = db.EnBasariliPersonel().FirstOrDefault();
            //Toplam mesaj sayısını getirir.
            var deger11 = db.TBLILETISIM.Count();
            //Kitaplar listesinde aktif olan kitapları getirir.
            var deger12 = db.TBLKITAP.Where(x => x.DURUM == true).Count();

            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;
            ViewBag.dgr5 = deger5;
            ViewBag.dgr6 = deger6;
            ViewBag.dgr7 = deger7;
            ViewBag.dgr8 = deger8;
            ViewBag.dgr9 = deger9;
            ViewBag.dgr10 = deger10;
            ViewBag.dgr11 = deger11;
            ViewBag.dgr12 = deger12;
            return View();
        }
    }
}