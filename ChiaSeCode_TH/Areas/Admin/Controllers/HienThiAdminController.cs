using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChiaSeCode_TH.Models;

namespace ChiaSeCode_TH.Areas.Admin.Controllers
{
    public class HienThiAdminController : Controller
    {
        FreeCodeEntities db = new FreeCodeEntities();
        // GET: Admin/HienThiAdmin
        public ActionResult IndexAdmin()
        {
            return View();
        }
        public PartialViewResult MenuChon()
        {
            return PartialView();
        }
        public PartialViewResult DemCodeAD()
        {
            List<Code> DEMCODE = db.Codes.ToList();
            ViewBag.ThongBao = DEMCODE.Count;
            return PartialView();
        }
        public PartialViewResult MenuAD()
        {
            return PartialView();
        }
        public PartialViewResult DemNguoiDungAD()
        {
            List<NguoiDung> DEMND = db.NguoiDungs.ToList();
            ViewBag.ThongBao = DEMND.Count;
            return PartialView();
        }
        public PartialViewResult DemTinTuc()
        {
            List<TinTuc> DEMTT = db.TinTucs.ToList();
            ViewBag.ThongBao = DEMTT.Count;
            return PartialView();
        }
        public ActionResult DanhMucTheLoai()
        {
            return View();
        }

        public PartialViewResult DemDanhMuc()
        {
            List<DanhMuc> DEMDM = db.DanhMucs.ToList();
            ViewBag.ThongBao = DEMDM.Count;
            return PartialView();

        }
        public PartialViewResult DemTheLoai()
        {
            List<TheLoai> DEMTL = db.TheLoais.ToList();
            ViewBag.ThongBao = DEMTL.Count;
            return PartialView();
        }
    }
}