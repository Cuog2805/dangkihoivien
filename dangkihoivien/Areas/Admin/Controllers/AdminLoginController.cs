using dangkihoivien.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dangkihoivien.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        private DangKiHoiVienDBContext dbContext = new DangKiHoiVienDBContext();
        public ActionResult Login()
        {
            QuanTriVien admin = new QuanTriVien();
            ViewBag.ErrorLogin = null;
            return View(admin);
        }
        [HttpPost]
        public ActionResult Login(QuanTriVien admin)
        {
            if(dbContext.Admins.Any(ad => 
                ad.Username == admin.Username && ad.Password == admin.Password))
            {
                Session["adminSession"] = admin.Username;
                return RedirectToAction("Index", "AdminMenu");
            }
            else
            {
                ViewBag.ErrorLogin = "Lỗi: Tên đăng nhập hoặc mật khẩu sai!";
                return View(admin);
            }
        }
        public ActionResult Logout()
        {
            Session["adminSession"] = null;
            return RedirectToAction("Login");
        }
    }
}