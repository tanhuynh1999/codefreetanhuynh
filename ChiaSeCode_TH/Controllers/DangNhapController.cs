using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChiaSeCode_TH.Models;

namespace ChiaSeCode_TH.Controllers
{
    public class DangNhapController : Controller
    {
        FreeCodeEntities db = new FreeCodeEntities();
        // GET: DangNhap
        [HttpGet]
        public PartialViewResult DangNhap()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult DangNhap(FormCollection f)
        {
            string sTaiKhoan = f["txtTaiKhoan"].ToString();
            string sMatKhau = f["txtMatKhau"].ToString();
            NguoiDung nd = db.NguoiDungs.Where(n => n.VaiTro == null || n.VaiTro.MaVaiTro == 1).SingleOrDefault(n => n.Email == sTaiKhoan && n.MatKhau == sMatKhau);
            NguoiDung ad = db.NguoiDungs.Where(n => n.VaiTro.MaVaiTro == 2).SingleOrDefault(n => n.Email == sTaiKhoan && n.MatKhau == sMatKhau);
            if (nd != null)
            {
                Session["NguoiDung"] = nd;
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else if (ad != null)
            {
                Session["Admin"] = ad;
                Response.Redirect("/Admin/HienThiAdmin/IndexAdmin");
            }
            else
            {
                ViewBag.ThongBao = "Sai Email hoặc mật khẩu!";
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            return PartialView();
        }
        public PartialViewResult HienThiDangNhap()
        {
            return PartialView();
        }
    }
}