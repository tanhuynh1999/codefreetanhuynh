using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using ChiaSeCode_TH.Models;
using System.Data.Entity;

namespace ChiaSeCode_TH.Controllers
{
    
    public class BinhLuanController : Controller
    {
        FreeCodeEntities db = new FreeCodeEntities();
        // GET: BinhLuan
        public ActionResult BinhLuanCode(int ? page, int ? macode)
        {
            Session["BLCode"] = macode;
            int pagesize = 12;
            int pagenumber = (page ?? 1);
            var binhluan = db.BinhLuans.Where(n => n.MaCode == macode).Include(n=>n.TraLoiBinhLuans).OrderByDescending(n => n.NgayDang).ToList();
            return View(binhluan.ToPagedList(pagenumber, pagesize));
        }
        [HttpPost]
        public ActionResult BinhLuan(BinhLuan bl)
        {
            NguoiDung ses = (NguoiDung)Session["NguoiDung"];
            bl.MaNguoiDung = ses.MaNguoiDung;
            bl.NgayDang = DateTime.Now;
            bl.MaCode = Int32.Parse(Session["Code"].ToString());
            db.BinhLuans.Add(bl);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpPost]
        public ActionResult TraLoi(TraLoiBinhLuan tl, int idtl)
        {
            NguoiDung ses = (NguoiDung)Session["NguoiDung"];
            tl.MaNguoiDung = ses.MaNguoiDung;
            tl.MaBinhLuan = idtl;
            tl.NgayTraLoiBinhLuan = DateTime.Now;
            db.TraLoiBinhLuans.Add(tl);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}