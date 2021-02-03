using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class OduncController : Controller
    {
        // GET: Odunc
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        //işlemi durumu false olan yani ödünç verilen kitapları listeler
        public ActionResult Index()
        {
            var degerler = db.TBLHAREKET.Where(x => x.ISLEMDURUM == false).ToList();
            return View(degerler);
        }
        //kitabı ödünç vermek için üye,kitap,personel bilgilerinin dropdown listten seçilerek eklenmesi
        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> degerler = (from i in db.TBLUYELER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.AD +' '+ i.SOYAD,
                                                 Value = i.ID.ToString()
                                             }).ToList();
            ViewBag.UYE = degerler;
            List<SelectListItem> degerler1 = (from i in db.TBLKITAP.Where(x=>x.DURUM==true).ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.AD,
                                                  Value = i.ID.ToString()
                                              }).ToList();

            ViewBag.KIT = degerler1;

            List<SelectListItem> degerler2 = (from i in db.TBLPERSONEL.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.PERSONEL,
                                                  Value = i.ID.ToString()
                                              }).ToList();

            ViewBag.PER = degerler2;

            return View();
        }
        //kitabı ödünç verme
        [HttpPost]
        public ActionResult OduncVer(TBLHAREKET t)
        {

            if (!ModelState.IsValid)
            {
                return View("OduncVer");
            }
            db.TBLHAREKET.Add(t);
            db.SaveChanges();
            return RedirectToAction("OduncVer", "Odunc");
        }
        //kitabın iade alınması
        public ActionResult OduncIade(TBLHAREKET p)
        {
            var odn = db.TBLHAREKET.Find(p.ID);
            //ödünç verilirken hesaplanan iade tarihi
            DateTime d1 = DateTime.Parse(odn.IADETARIH.ToString());
            //bugünün tarihi
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            //ceza için hesaplanan geciken gün sayısı
            TimeSpan d3 = d2 - d1;
            ViewBag.dgr = d3.TotalDays;
            return View("OduncIade", odn);
        }
        //işlemleri güncelleme
        public ActionResult Guncelle(TBLHAREKET p)
        {
            var hrk = db.TBLHAREKET.Find(p.ID);
            hrk.UYEGETIRTARIH = p.UYEGETIRTARIH;
            hrk.ISLEMDURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }


    }
}