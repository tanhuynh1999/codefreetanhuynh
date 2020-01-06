using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChiaSeCode_TH.Models;
using PagedList;
using PagedList.Mvc;

namespace ChiaSeCode_TH.Controllers
{
    
    public class LocCodeController : Controller
    {
        FreeCodeEntities db = new FreeCodeEntities();
        // GET: LocCode
        public PartialViewResult CodeMoiNhat()
        {
            return PartialView(db.Codes.Where(n=>n.Duyet == true).OrderByDescending(n=>n.NgayDang).Take(12).ToList());
        }
        public PartialViewResult NgonNgu()
        {
            return PartialView(db.DanhMucs.ToList());
        }
        public ActionResult LocTheoNgonNgu(int madanhmuc = 0)
        {
            DanhMuc dm = db.DanhMucs.SingleOrDefault(n => n.MaDanhMuc == madanhmuc);
            if(dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Session["NgonNgu"] = dm;
            List<Code> code = db.Codes.Where(n => n.MaDanhMuc == madanhmuc && n.Duyet == true).ToList();
            if(code.Count == 0)
            {
                ViewBag.ThongBao = "Không có code nào";
            }
            return View(code);
        }
        public ActionResult LocTheoTheLoai(int matheloai = 0)
        {
            TheLoai tl = db.TheLoais.SingleOrDefault(n => n.MaTheLoai == matheloai);
            if (tl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Session["TheLoai"] = tl;
            List<Code> code = db.Codes.Where(n => n.MaTheLoai == matheloai && n.Duyet == true).ToList();
            if (code.Count == 0)
            {
                ViewBag.ThongBao = "Không có code nào";
            }
            return View(code);
        }
        public ActionResult CodeXemNhieu(int? page)
        {
            int pagesize = 12;
            int pagenumber = (page ?? 1);
            return View(db.Codes.Where(n => n.Duyet == true).OrderByDescending(n => n.LuotXem).ToPagedList(pagenumber, pagesize));
        }
        public ActionResult CodeTaiNhieu(int? page)
        {
            int pagesize = 12;
            int pagenumber = (page ?? 1);
            return View(db.Codes.Where(n => n.Duyet == true).OrderByDescending(n => n.LuotTai).ToPagedList(pagenumber, pagesize));
        }
        public ActionResult CodeMoi(int? page)
        {
            int pagesize = 12;
            int pagenumber = (page ?? 1);
            return View(db.Codes.Where(n => n.Duyet == true).OrderByDescending(n => n.NgayDang).ToPagedList(pagenumber, pagesize));
        }
        public PartialViewResult TatCaCodeGame()
        {
            return PartialView(db.Codes.Where(n => n.Duyet == true).Where(n => n.MaTheLoai == 6).OrderByDescending(n => n.LuotXem).Take(8).ToList());
        }
        public PartialViewResult TatCaCodeUngDung()
        {
            return PartialView(db.Codes.Where(n => n.Duyet == true).Where(n => n.MaTheLoai == 4).OrderByDescending(n => n.LuotXem).Take(8).ToList());
        }
        public PartialViewResult TatCaCodeWebsite()
        {
            return PartialView(db.Codes.Where(n => n.Duyet == true).Where(n => n.MaDanhMuc == 4 || n.MaDanhMuc == 6 || n.MaDanhMuc == 11 || n.MaTheLoai == 2 || n.MaTheLoai == 1).OrderByDescending(n => n.NgayDang).Take(8).ToList());
        }
        public PartialViewResult TheLoai()
        {
            return PartialView(db.TheLoais.ToList());
        }
    }
}