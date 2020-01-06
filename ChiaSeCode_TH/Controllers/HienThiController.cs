using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChiaSeCode_TH.Controllers
{
    public class HienThiController : Controller
    {
        // GET: HienThi
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ThanhCongCu()
        {
            return PartialView();
        }
        public PartialViewResult Menu()
        {
            return PartialView();
        }
        public PartialViewResult TrangChu2()
        {
            return PartialView();
        }
        public PartialViewResult ChanTK()
        {
            return PartialView();
        }
        public PartialViewResult MenuAll()
        {
            return PartialView();
        }
        public PartialViewResult MenuTinTuc()
        {
            return PartialView();
        }
        public PartialViewResult MenuNo()
        {
            return PartialView();
        }
        public PartialViewResult ChanTrang()
        {
            return PartialView();
        }
    }
}