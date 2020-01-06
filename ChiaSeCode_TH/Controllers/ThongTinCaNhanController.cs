using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChiaSeCode_TH.Models;

namespace ChiaSeCode_TH.Controllers
{
    
    public class ThongTinCaNhanController : Controller
    {
        FreeCodeEntities db = new FreeCodeEntities();
        // GET: ThongTinCaNhan
        public PartialViewResult ThongTinCaNhan()
        {   
            return PartialView();
        }
        public ActionResult SuaTT(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaVaiTro = new SelectList(db.VaiTroes, "MaVaiTro", "TenVaiTro", nguoiDung.MaVaiTro);
            return View(nguoiDung);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaTT([Bind(Include = "MaNguoiDung,Email,MatKhau,NhapLaiMatKhau,MaVaiTro,Ten,TenHienThi,SoDienThoai")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                Session["NguoiDung"] = nguoiDung;
                return RedirectToAction("Index", "HienThi");
            }
            ViewBag.MaVaiTro = new SelectList(db.VaiTroes, "MaVaiTro", "TenVaiTro", nguoiDung.MaVaiTro);
            return View(nguoiDung);
        }
    }
}