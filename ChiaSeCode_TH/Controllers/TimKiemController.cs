using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChiaSeCode_TH.Models;
using PagedList.Mvc;
using PagedList;

namespace ChiaSeCode_TH.Controllers
{
    public class TimKiemController : Controller
    {
        FreeCodeEntities db = new FreeCodeEntities();
        // GET: TimKiem
        [HttpPost]
        public ActionResult KqTimKiem(FormCollection f, int? page)
        {
            string sTuKhoa = f["txtTK"].ToString();
            ViewBag.TuKhoa = sTuKhoa;
            List<Code> KQ = db.Codes.Where(n => n.TenMaCode.Contains(sTuKhoa)).ToList();

            int pageNumber = (page ?? 1);
            int pageSize = 12;


            if (KQ.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(db.Codes.OrderBy(n => n.TenMaCode).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy " + KQ.Count + " kết quả!";


            return View(KQ.OrderBy(n => n.TenMaCode).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult KqTimKiem(int? page, string sTuKhoa)
        {
            ViewBag.TuKhoa = sTuKhoa;
            List<Code> KQ = db.Codes.Where(n => n.TenMaCode.Contains(sTuKhoa)).ToList();

            int pageNumber = (page ?? 1);
            int pageSize = 12;


            if (KQ.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(db.Codes.OrderBy(n => n.TenMaCode).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy " + KQ.Count + " kết quả!";


            return View(KQ.OrderBy(n => n.TenMaCode).ToPagedList(pageNumber, pageSize));
        }
    }
}