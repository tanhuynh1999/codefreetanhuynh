using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChiaSeCode_TH.Models;

namespace ChiaSeCode_TH.Controllers
{
    public class DanhGiaController : Controller
    {
        FreeCodeEntities db = new FreeCodeEntities();
        // GET: DanhGia
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult danhGia(int n)
        {
            Code cd = db.Codes.Find(Int32.Parse(Session["Code"].ToString()));
            db.Codes.Find(Int32.Parse(Session["Code"].ToString())).DanhGia = ((cd.DanhGia * cd.LuotDanhGia) + n) / (cd.LuotDanhGia + 1);
            db.Codes.Find(Int32.Parse(Session["Code"].ToString())).LuotDanhGia++;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}