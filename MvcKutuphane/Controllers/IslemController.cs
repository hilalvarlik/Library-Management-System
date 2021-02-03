using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class IslemController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        // GET: Islem
        //işlem durumu true olan yani rafta olan kitapları listeler
        public ActionResult Index()
        {
            var degerler = db.TBLHAREKET.Where(x => x.ISLEMDURUM == true).ToList();
            return View(degerler);
            
        }
    }
}