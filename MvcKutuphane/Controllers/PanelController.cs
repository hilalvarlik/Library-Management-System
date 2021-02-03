using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcKutuphane.Controllers
{
    public class PanelController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        // GET: Panel
        //yetkilendirme yapar
        [Authorize]
        public ActionResult Index()
        {
            //giriş yapan üyenin kullanıcı adına göre bilgilerini getirir
            var uye = Session["KULLANICIADI"].ToString();
            var degerler = db.TBLUYELER.Where(x => x.KULLANICIADI == uye).FirstOrDefault();
            //ad ve soyad başlığı koyar
            ViewBag.u = degerler.AD +" "+degerler.SOYAD;
            return View(degerler);
        }
        //üyenin bilgilerini güncellemesi
        [HttpPost]
        public ActionResult Index2(TBLUYELER p)
        {
            var kullanici = Session["KULLANICIADI"].ToString();
            var uye = db.TBLUYELER.FirstOrDefault(x => x.KULLANICIADI == kullanici);
            string passw = p.SIFRE;
            //üyenin şifresinin md5 ile hashlenerek veritabanına kaydedilmesi
            string password = FormsAuthentication.HashPasswordForStoringInConfigFile(passw, "MD5").ToLower();
            p.SIFRE = password;
            uye.SIFRE = p.SIFRE;
            uye.AD = p.AD; 
            uye.SOYAD = p.SOYAD;
            uye.TELEFON = p.TELEFON;
            uye.FOTOGRAF = p.FOTOGRAF;
            db.SaveChanges();
            return RedirectToAction("Index", "Panel");
        }
        //giriş yapan üyenin daha önce ödünç aldığı kitaplar
        public ActionResult Kitaplarim()
        {
            var uye = (string)Session["MAIL"];
            var id = db.TBLUYELER.Where(x => x.MAIL == uye.ToString()).Select(z => z.ID).FirstOrDefault();
            var degerler = db.TBLHAREKET.Where(x => x.UYE == id).ToList();
            return View(degerler);
        }
        //duyuru listesi
        public ActionResult Duyurular()
        {
            var duyurulistesi = db.TBLDUYURU.ToList();
            return View(duyurulistesi);
        }
        //çıkış yap
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");
        }
    }
}