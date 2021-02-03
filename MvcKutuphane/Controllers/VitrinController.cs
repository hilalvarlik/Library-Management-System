using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class VitrinController : Controller
    {
        // GET: Vitrin
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        //sisteme giriş yapıcak ziyaretçiler için vitrin modülü
        [HttpGet]
        public ActionResult Index()
        {
            return View();

        }
        //iletişim bilgileri girerek istek,görüş ve önerilerin iletilmesi
        [HttpPost]
        public ActionResult Index(TBLILETISIM t)
        {
            db.TBLILETISIM.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}