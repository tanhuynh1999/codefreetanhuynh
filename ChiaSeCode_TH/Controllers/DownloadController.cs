using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChiaSeCode_TH.Models;

namespace ChiaSeCode_TH.Controllers
{
    public class DownloadController : Controller
    {
        FreeCodeEntities db = new FreeCodeEntities();
        // GET: Download
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult downLoad()
        {
            db.Codes.Find(Int32.Parse(Session["Code"].ToString())).LuotTai++;
            db.SaveChanges();
            string tmp = db.Codes.Find(Int32.Parse(Session["Code"].ToString())).LinkTai;
            return Redirect("" + tmp);
        }
    }
}