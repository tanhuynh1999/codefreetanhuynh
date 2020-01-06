using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChiaSeCode_TH.Models;
using PagedList;

namespace ChiaSeCode_TH.Areas.Admin.Controllers
{
    public class CodesController : Controller
    {
        private FreeCodeEntities db = new FreeCodeEntities();

        // GET: Admin/Codes
        public ActionResult Index(int? page)
        {
            int pagesize = 12;
            int pagenumber = (page ?? 1);
            var codes = db.Codes.Include(c => c.DanhMuc).Include(c => c.NguoiDung).Include(c => c.TheLoai);
            return View(codes.OrderByDescending(n=>n.NgayDang).ToPagedList(pagenumber, pagesize));
        }

        // GET: Admin/Codes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Code code = db.Codes.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }
            return View(code);
        }

        // GET: Admin/Codes/Create
        public ActionResult Create()
        {
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "Email");
            ViewBag.MaTheLoai = new SelectList(db.TheLoais, "MaTheLoai", "TenTheLoai");
            return View();
        }

        // POST: Admin/Codes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCode,TenMaCode,HinhAnh,NoiDungCode,MaDanhMuc,NgayDang,MaTheLoai,MaNguoiDung,LuotXem,LuotTai,LinkTai,NoiDungCaiDat,Duyet")] Code code, HttpPostedFileBase fileUpload)
        {
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
            code.Duyet = true;
            db.Codes.Add(code);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", code.MaDanhMuc);
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "Email", code.MaNguoiDung);
            ViewBag.MaTheLoai = new SelectList(db.TheLoais, "MaTheLoai", "TenTheLoai", code.MaTheLoai);
            db.SaveChanges();
            return View(code);
        }

        // GET: Admin/Codes/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Code code = db.Codes.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", code.MaDanhMuc);
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "Email", code.MaNguoiDung);
            ViewBag.MaTheLoai = new SelectList(db.TheLoais, "MaTheLoai", "TenTheLoai", code.MaTheLoai);
            return View(code);
        }

        // POST: Admin/Codes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCode,TenMaCode,HinhAnh,NoiDungCode,MaDanhMuc,NgayDang,MaTheLoai,MaNguoiDung,LuotXem,LuotTai,LinkTai,NoiDungCaiDat,Duyet")] Code code, HttpPostedFileBase fileUpload)
        {
            db.Entry(code).State = EntityState.Modified;
            code.DanhGia = 0;
            code.LuotDanhGia = 0;
            db.SaveChanges();
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", code.MaDanhMuc);
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "Email", code.MaNguoiDung);
            ViewBag.MaTheLoai = new SelectList(db.TheLoais, "MaTheLoai", "TenTheLoai", code.MaTheLoai);
            return View(code);
        }

        // GET: Admin/Codes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Code code = db.Codes.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }
            return View(code);
        }

        // POST: Admin/Codes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Code code = db.Codes.Find(id);
            db.Codes.Remove(code);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
