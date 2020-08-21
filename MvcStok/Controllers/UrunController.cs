using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun  

        MvcDbStokEntities1 db = new MvcDbStokEntities1();

        public ActionResult Index(int sayfa=1)
        {
            // var degerler = db.TBLURUNLER.ToList();
            var degerler = db.TBLURUNLER.ToList().ToPagedList(sayfa,10);
            return View(degerler);


        }
        [HttpGet]
        public ActionResult UrunEkle()
        {

            // Not: Burada linq sorgusu yapılmıştır. Linq sorgu yapısı araştırılacaktır.
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();


        }

        [HttpPost]
        public ActionResult UrunEkle(TBLURUNLER p1)
        {
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORILER = ktg;

            //if (!ModelState.IsValid)
            //{
            //    return View("UrunEkle");
            //}

            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();

            return RedirectToAction("Index");

        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            

            return View("UrunGetir", urun);

        }
        public ActionResult Guncelle(TBLURUNLER p1)
        {
            var urun = db.TBLURUNLER.Find(p1.URUNID);
            urun.URUNAD = p1.URUNAD;
            urun.MARKA = p1.MARKA;
            //urun.TBLKATEGORILER.KATEGORIAD = p1.TBLKATEGORILER.KATEGORIAD;
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;

            urun.FIYAT = p1.FIYAT;
            urun.STOK = p1.STOK;
            
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }
    }
}