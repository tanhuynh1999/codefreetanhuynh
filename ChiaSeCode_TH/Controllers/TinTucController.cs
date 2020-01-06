using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChiaSeCode_TH.Models;

namespace ChiaSeCode_TH.Controllers
{
    public class TinTucController : Controller
    {
        FreeCodeEntities db = new FreeCodeEntities();
        // GET: TinTuc
        public ActionResult TrangChuTinTin()
        {
            return View();
        }
        public PartialViewResult TinTucXemNhieu()
        {
            return PartialView(db.TinTucs.OrderByDescending(n=>n.LuotXemTinTuc).Take(1).ToList());
        }
        public PartialViewResult TinTucMoiNhat()
        {
            return PartialView(db.TinTucs.OrderByDescending(n => n.NgayDangTinTuc).Take(2).ToList());
        }
        public PartialViewResult TinTucXemNhieu2()
        {
            return PartialView(db.TinTucs.OrderByDescending(n => n.LuotXemTinTuc).Take(8).ToList());
        }
        public PartialViewResult TinTuc()
        {
            return PartialView(db.TinTucs.Where(n=>n.MaTheLoaiTinTuc == 3).OrderByDescending(n => n.NgayDangTinTuc).Take(4).ToList());
        }
        public PartialViewResult ThuThuat()
        {
            return PartialView(db.TinTucs.Where(n => n.MaTheLoaiTinTuc == 2).OrderByDescending(n => n.NgayDangTinTuc).Take(4).ToList());
        }
        public PartialViewResult KienThuc()
        {
            return PartialView(db.TinTucs.Where(n => n.MaTheLoaiTinTuc == 1).OrderByDescending(n => n.NgayDangTinTuc).Take(4).ToList());
        }
        public ViewResult XemChiTietTinTuc(int matintuc = 0)
        {
            TinTuc tt = db.TinTucs.SingleOrDefault(n => n.MaTinTuc == matintuc);
            if(tt == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tt);
        }
        public PartialViewResult DanhSachTheLoai()
        {
            return PartialView(db.TheLoaiTinTucs.ToList());
        }
        public ActionResult LocTheoTheLoaiTinTuc(int matheloaitintuc = 0)
        {
            TheLoaiTinTuc tt = db.TheLoaiTinTucs.SingleOrDefault(n => n.MaTheLoaiTinTuc == matheloaitintuc);
            if (tt == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Session["TheLoaiTinTuc"] = tt;
            List<TinTuc> tintuc = db.TinTucs.Where(n => n.MaTheLoaiTinTuc == matheloaitintuc).ToList();
            if (tintuc.Count == 0)
            {
                ViewBag.ThongBao = "Không có code nào";
            }
            return View(tintuc);
        }
    }
}