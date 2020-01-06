using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChiaSeCode_TH.Models;

namespace ChiaSeCode_TH.Controllers
{
    public class XemChiTietController : Controller
    {
        FreeCodeEntities db = new FreeCodeEntities();
        // GET: XemChiTiet
        public ActionResult XemChiTiet(int macode = 0)
        {
            Code code = db.Codes.SingleOrDefault(n => n.MaCode == macode);
            if (code == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Session["Code"] = macode;
            db.Codes.Find(macode).LuotXem++;
            db.SaveChanges();
            return View(code);
        }
    }
}