using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChiaSeCode_TH.Controllers
{
    public class DangXuatController : Controller
    {
        // GET: DangXuat
        public ActionResult DangXuatNguoiDung()
        {
            Session["NguoiDung"] = null;
            return Redirect("/HienThi/Index");
        }
        public ActionResult DangXuatAdmin()
        {
            Session["Admin"] = null;
            return Redirect("/HienThi/Index");
        }
    }
}