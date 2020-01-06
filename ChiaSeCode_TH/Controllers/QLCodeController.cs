using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChiaSeCode_TH.Models;

namespace ChiaSeCode_TH.Controllers
{
    public class QLCodeController : Controller
    {
        FreeCodeEntities db = new FreeCodeEntities();
        // GET: QLCode
        public ActionResult QLCode(int manguoidung = 0)
        {
            List<Code> code = db.Codes.Where(n => n.MaNguoiDung == manguoidung).ToList();
            if (code.Count == 0)
            {
                ViewBag.ThongBao = "Không có nội dung nào";
            }
            return View(code);
        }
        public PartialViewResult DaChiaSe(int manguoidung = 0)
        {
            List<Code> dacs = db.Codes.Where(n => n.MaNguoiDung == manguoidung).ToList();
            ViewBag.ThongBao = dacs.Count;
            return PartialView();
        }

    }
}