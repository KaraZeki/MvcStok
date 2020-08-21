using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class SatisController : Controller
    {
        MvcDbStokEntities1 db = new MvcDbStokEntities1();
        // GET: Satis
        TBLSATISLAR tblsatislar = new TBLSATISLAR();
        public ActionResult Index()
        {
            //var degerler = db.TBLSATISLAR.ToList();
            tblsatislar.tBLSATISLARs = db.TBLSATISLAR.ToList();
            return View(tblsatislar);
        }

        [HttpGet]
        public ActionResult SatisYap()
        {
            
            return View("Index");

        }


        [HttpPost]
        public ActionResult SatisYap(TBLSATISLAR p)
        {
            var ktg = db.TBLMUSTERI.Where(m => m.MUSTERIID == p.TBLMUSTERI.MUSTERIID).FirstOrDefault();
            p.TBLMUSTERI = ktg;


            db.TBLSATISLAR.Add(p);
            db.SaveChanges();
            return View("Index");
        }
    }

}