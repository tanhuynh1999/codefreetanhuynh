using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChiaSeCode_TH.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        public PartialViewResult TTCaNhan()
        {
            return PartialView();
        }
    }
}