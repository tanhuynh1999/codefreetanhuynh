using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChiaSeCode_TH.Models;

namespace ChiaSeCode_TH.Controllers
{
    public class DemController : Controller
    {
        FreeCodeEntities db = new FreeCodeEntities();
        // GET: Dem
        public PartialViewResult DemCode()
        {
            List<Code> DEMCODE = db.Codes.ToList();
            ViewBag.ThongBao = DEMCODE.Count;
            return PartialView();
        }
        public PartialViewResult DemCodeGame()
        {
            List<Code> DEMCODE = db.Codes.Where(n=>n.MaTheLoai == 6).ToList();
            ViewBag.ThongBao = DEMCODE.Count;
            return PartialView();
        }
        public PartialViewResult DemCodeAppWindow()
        {
            List<Code> DEMCODE = db.Codes.Where(n => n.MaTheLoai == 3).ToList();
            ViewBag.ThongBao = DEMCODE.Count;
            return PartialView();
        }
        public PartialViewResult DemCodeAppMobile()
        {
            List<Code> DEMCODE = db.Codes.Where(n => n.MaTheLoai == 4).ToList();
            ViewBag.ThongBao = DEMCODE.Count;
            return PartialView();
        }
        public PartialViewResult DemCodeWebsite()
        {
            List<Code> DEMCODE = db.Codes.Where(n => n.MaTheLoai == 2 || n.MaTheLoai == 1).ToList();
            ViewBag.ThongBao = DEMCODE.Count;
            return PartialView();
        }
    }
}