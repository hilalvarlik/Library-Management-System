using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class MesajlarController : Controller
    {
        // GET: Mesajlar
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        //giriş yapan üyenin mesajlarının listelenmesi
        public ActionResult Index()
        {
            //giriş yapan üyenin mail adresine göre mesajların listelenmesi
            var uyemail = (string)Session["MAIL"].ToString();
            var mesajlar = db.TBLMESAJLAR.Where(x => x.ALICI == uyemail.ToString()).ToList();
            return View(mesajlar);
        }
        //üyenin mesaj göndermesi
        public ActionResult Giden()
        {
            //alıcı mail adresine gönderen mail adresi bilgisiyle mesaj gönderme
            var uyemail = (string)Session["MAIL"].ToString();
            var mesajlar = db.TBLMESAJLAR.Where(x => x.GONDEREN == uyemail.ToString()).ToList();
            return View(mesajlar);
            
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            return View();
        }
        //yeni mesaj oluşturma
        [HttpPost]
        public ActionResult YeniMesaj(TBLMESAJLAR t)
        {
            var uyemail = (string)Session["MAIL"].ToString();
            t.GONDEREN = uyemail.ToString();
            t.TARIH = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TBLMESAJLAR.Add(t);
            db.SaveChanges();
            return RedirectToAction("Giden","Mesajlar");
        }
    }
}