using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChiaSeCode_TH.Models;

namespace ChiaSeCode_TH.Controllers
{
    public class DangKyController : Controller
    {
        FreeCodeEntities db = new FreeCodeEntities();
        // GET: DangKy
        public ActionResult DangKy()
        {
            ViewBag.MaVaiTro = new SelectList(db.VaiTroes, "MaVaiTro", "TenVaiTro");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy([Bind(Include = "MaNguoiDung,Email,MatKhau,NhapLaiMatKhau,MaVaiTro,Ten,TenHienThi,SoDienThoai,Token")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                nguoiDung.Token = Guid.NewGuid().ToString();
                db.NguoiDungs.Add(nguoiDung);
                db.SaveChanges();
                return RedirectToAction("ThongBao");
            }

            ViewBag.MaVaiTro = new SelectList(db.VaiTroes, "MaVaiTro", "TenVaiTro", nguoiDung.MaVaiTro);
            return View(nguoiDung);
        }
        public ActionResult ThongBao()
        {
            return View();
        }
        public JsonResult ktraTK(string Email)
        {
            bool Exists = db.NguoiDungs.FirstOrDefault(t => t.Email == Email) != null;
            return Json(!Exists, JsonRequestBehavior.AllowGet);
        }
    }
}