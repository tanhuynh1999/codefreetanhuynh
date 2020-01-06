using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ChiaSeCode_TH.Models;

namespace ChiaSeCode_TH.Controllers
{
    public class QuenMatKhauController : Controller
    {
        FreeCodeEntities db = new FreeCodeEntities();
        // GET: QuenMatKhau
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GuiMail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GuiMail(QuenMatKhau qmk)
        {
            try
            {
                // Định cấu hình lớp webMail để gửi email  
                // máy chủ gmail smtp  
                WebMail.SmtpServer = "smtp.gmail.com";
                // cổng gmail để gửi email  
                WebMail.SmtpPort = 587;
                WebMail.SmtpUseDefaultCredentials = true;
                // gửi email với giao thức bảo mật  
                WebMail.EnableSsl = true;
                // EmailId được sử dụng để gửi email từ ứng dụng  
                WebMail.UserName = "huynhminhtan4019@gmail.com";
                WebMail.Password = "01662696211a";

                // Địa chỉ email người gửi.  
                WebMail.From = "huynhminhtan4019@gmail.com";
                NguoiDung nd = db.NguoiDungs.FirstOrDefault(t => t.Email == qmk.EmailNhan);
                qmk.ChuDe = "Xác nhận đổi mật khẩu trang freecode";
                qmk.NoiDung = "Xác nhận:'https://localhost:44301/QuenMatKhau/Thaydoimatkhau/" + nd.MaNguoiDung + "?Token=" + nd.Token;

                //Gửi email  
                WebMail.Send(to: qmk.EmailNhan, subject: qmk.ChuDe , body: qmk.NoiDung, cc: qmk.Cc, bcc: qmk.Bcc, isBodyHtml: true);
                ViewBag.Status = "Email được gửi thành công.";
            }
            catch(Exception)
            {
                ViewBag.Status = "Sự cố trong khi gửi email, vui lòng kiểm tra chi tiết.";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Thaydoimatkhau()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Thaydoimatkhau(string matkhaumoi, string nhaplaimatkhau, int? id, string Token)
        {
            if(matkhaumoi != nhaplaimatkhau)
            {
                ViewBag.TB = "Mật khẩu không đúng! ";
                return View();
            }
            else
            {
                db.NguoiDungs.Find(id).MatKhau = matkhaumoi;
                db.NguoiDungs.Find(id).Token = Guid.NewGuid().ToString();
                db.SaveChanges();
                Session["NguoiDung"] = db.NguoiDungs.Find(id);
                return Redirect("/HienThi/Index");
            }
        }
    }
}