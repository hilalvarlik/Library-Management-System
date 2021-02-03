using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class RegisterController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        // GET: Register
        [HttpGet]
        public ActionResult Kayit()
        {
            return View();
        }
        //üye kayıt olma 
        [HttpPost]
        public ActionResult Kayit(TBLUYELER p)
        {
            string passw = p.SIFRE;
            //girilen şifre md5 ile hashlenerek veri tabanına kaydedilir.
            string password = FormsAuthentication.HashPasswordForStoringInConfigFile(passw, "MD5").ToLower();
            p.SIFRE = password;
            db.TBLUYELER.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}