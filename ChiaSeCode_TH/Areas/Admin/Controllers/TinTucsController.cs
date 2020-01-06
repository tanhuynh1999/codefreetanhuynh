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
    public class TinTucsController : Controller
    {
        private FreeCodeEntities db = new FreeCodeEntities();

        // GET: Admin/TinTucs
        public ActionResult Index(int? page)
        {
            int pagesize = 12;
            int pagenumber = (page ?? 1);
            var tinTucs = db.TinTucs.Include(t => t.NguoiDung).Include(t => t.TheLoaiTinTuc);
            return View(tinTucs.OrderBy(n => n.TenTinTuc).ToPagedList(pagenumber, pagesize));
        }

        // GET: Admin/TinTucs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // GET: Admin/TinTucs/Create
        public ActionResult Create()
        {
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "Email");
            ViewBag.MaTheLoaiTinTuc = new SelectList(db.TheLoaiTinTucs, "MaTheLoaiTinTuc", "TenTheLoaiTinTuc");
            return View();
        }

        // POST: Admin/TinTucs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTinTuc,TenTinTuc,NgayDangTinTuc,NoiDungTinTuc,MaNguoiDung,MaTheLoaiTinTuc,AnhTinTuc,LuotXemTinTuc")] TinTuc tinTuc, HttpPostedFileBase fileUpload)
        {
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
            tinTuc.AnhTinTuc = fileUpload.FileName;
            tinTuc.NgayDangTinTuc = DateTime.Now;
            db.TinTucs.Add(tinTuc);
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "Email", tinTuc.MaNguoiDung);
            ViewBag.MaTheLoaiTinTuc = new SelectList(db.TheLoaiTinTucs, "MaTheLoaiTinTuc", "TenTheLoaiTinTuc", tinTuc.MaTheLoaiTinTuc);
            db.SaveChanges();
            return View(tinTuc);
        }

        // GET: Admin/TinTucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "Email", tinTuc.MaNguoiDung);
            ViewBag.MaTheLoaiTinTuc = new SelectList(db.TheLoaiTinTucs, "MaTheLoaiTinTuc", "TenTheLoaiTinTuc", tinTuc.MaTheLoaiTinTuc);
            return View(tinTuc);
        }

        // POST: Admin/TinTucs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTinTuc,TenTinTuc,NgayDangTinTuc,NoiDungTinTuc,MaNguoiDung,MaTheLoaiTinTuc,AnhTinTuc,LuotXemTinTuc")] TinTuc tinTuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "Email", tinTuc.MaNguoiDung);
            ViewBag.MaTheLoaiTinTuc = new SelectList(db.TheLoaiTinTucs, "MaTheLoaiTinTuc", "TenTheLoaiTinTuc", tinTuc.MaTheLoaiTinTuc);
            return View(tinTuc);
        }

        // GET: Admin/TinTucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // POST: Admin/TinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TinTuc tinTuc = db.TinTucs.Find(id);
            db.TinTucs.Remove(tinTuc);
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
