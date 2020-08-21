using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller 
    {
        // GET: Musteri
        MvcDbStokEntities1 db = new MvcDbStokEntities1();

        public ActionResult Index(string p)
        {
            var degerler = from d in db.TBLMUSTERI select d;

            //bu if kontrolu search kısmı için yapılıyor. Eğer parametreden gelen değer boş değilse parametreden gelen değeri listeler
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MUSTERIAD.Contains(p));
            }
            return View(degerler.ToList());

            // var degerler = db.TBLMUSTERI.ToList();
            //return View(degerler);
        }
        [HttpGet]
        public ActionResult MusteriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MusteriEkle(TBLMUSTERI p2)
        {

            //Bu kısım textbox kısmında validation (boş geçilemez-50 den fazla karakter girilemez ) kontrolü yapılır.
            if (!ModelState.IsValid)
            {
                return View("MusteriEkle");
            }
            db.TBLMUSTERI.Add(p2);
            db.SaveChanges();
            return View();
        }
        public ActionResult SIL(int id)
        {
            var musteri = db.TBLMUSTERI.Find(id);
            db.TBLMUSTERI.Remove(musteri);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var mus = db.TBLMUSTERI.Find(id);
            return View("MusteriGetir", mus);
        }

        public ActionResult Guncelle(TBLMUSTERI p1)
        {
            var ktgr = db.TBLMUSTERI.Find(p1.MUSTERIID);
            ktgr.MUSTERIAD = p1.MUSTERIAD;
            ktgr.MUSTERISOYAD = p1.MUSTERISOYAD;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}