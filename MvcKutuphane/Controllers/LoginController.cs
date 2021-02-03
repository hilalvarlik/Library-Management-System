using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class LoginController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        // GET: Login
        Context c = new Context();
        // GET: Guvenlik
        public ActionResult GirisYap()
        {
            return View();
        }
        //giriş yapma
        [HttpPost]
        public ActionResult GirisYap(TBLUYELER t)
        {
            string passw = t.SIFRE;
            //texte girilen şifrenin hashlenmesi
            string password = FormsAuthentication.HashPasswordForStoringInConfigFile(passw, "MD5").ToLower();
            t.SIFRE = password;
            //kullanıcı adı ve hashli şifrenin veritabanındaki veri ile karşılaştırılması
            var bilgiler = db.TBLUYELER.FirstOrDefault(s => s.KULLANICIADI == t.KULLANICIADI && s.SIFRE == t.SIFRE);
            //eğer bilgiler boş değilse giriş yapma
            if (bilgiler != null)
            {
                //session ile bilgiler panelde tutulur
                FormsAuthentication.SetAuthCookie(bilgiler.KULLANICIADI, false);
                Session["KULLANICIADI"] = bilgiler.KULLANICIADI.ToString();
                //TempData["AD"] = bilgiler.AD.ToString();
                //TempData["SOYAD"] = bilgiler.SOYAD.ToString();
                //TempData["MAIL"] = bilgiler.MAIL.ToString();
                //TempData["TELEFON"] = bilgiler.TELEFON.ToString();
                Session["AD"] = bilgiler.AD.ToString();
                Session["SOYAD"] = bilgiler.SOYAD.ToString();
                Session["MAIL"] = bilgiler.MAIL.ToString();
                Session["KULLANICIADI"] = bilgiler.KULLANICIADI.ToString();
                Session["TELEFON"] = bilgiler.TELEFON.ToString();

                return RedirectToAction("Index", "Panel");

                
            }
            //boş ise
            else
            {
                return View();
            }
        }
    }
}