using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChiaSeCode_TH.Models;

namespace ChiaSeCode_TH.Controllers
{
    public class DangBaiController : Controller
    {
        // GET: DangBai
        private FreeCodeEntities db = new FreeCodeEntities();
        NguoiDung nd;
        public ActionResult ChiaSeCode()
        {
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaTheLoai = new SelectList(db.TheLoais, "MaTheLoai", "TenTheLoai");
            nd = (NguoiDung)Session["NguoiDung"];
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs.Where(n => n.MaNguoiDung == nd.MaNguoiDung), "MaNguoiDung", "Email");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult ChiaSeCode([Bind(Include = "MaCode,TenMaCode,HinhAnh,NoiDungCode,MaDanhMuc,NgayDang,MaTheLoai,MaNguoiDung,LuotXem,LuotTai,LinkTai,NoiDungCaiDat,DanhGia,LuotDanhGia,Duyet")] Code code, HttpPostedFileBase fileUpload)
        {
            NguoiDung nd = (NguoiDung)Session["NguoiDung"];
            //Lưu tên file ảnh
            var fileimg = Path.GetFileName(fileUpload.FileName);
            //Lưu file
            var pa = Path.Combine(Server.MapPath("~/HinhAnh"), fileimg);
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Chọn hình ảnh";
                return View();
            }
            else if (System.IO.File.Exists(pa))
            {
                ViewBag.ThongBao = "Hình ảnh đã tồn tại!";
            }
            else
            {
                fileUpload.SaveAs(pa);
            }
            code.HinhAnh = fileUpload.FileName;
            code.NgayDang = DateTime.Now;
            code.DanhGia = 0;
            code.LuotDanhGia = 0;
            code.LuotTai = 0;
            code.LuotXem = 0;
            code.Duyet = false;
            db.Codes.Add(code);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", code.MaDanhMuc);
            ViewBag.MaTheLoai = new SelectList(db.TheLoais, "MaTheLoai", "TenTheLoai", code.MaTheLoai);
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs.Where(n => n.MaNguoiDung == nd.MaNguoiDung), "MaNguoiDung", "Email", code.MaNguoiDung);
            db.SaveChanges();
            return RedirectToAction("QLCode", "QLCode", new { @manguoidung = nd.MaNguoiDung});
        }
        public ActionResult QLCode(int manguoidung = 0)
        {
            List<Code> code = db.Codes.Where(n => n.MaNguoiDung == manguoidung).ToList();
            if (code.Count == 0)
            {
                ViewBag.ThongBao = "Không có nội dung nào";
            }
            return View(code);
        }
    }
}