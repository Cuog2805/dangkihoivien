using dangkihoivien.Models;
using dangkihoivien.Models.Action;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dangkihoivien.Controllers
{
    public class DangKiHoiVienController : Controller
    {
        static private DangKiHoiVienDBContext dBContext = new DangKiHoiVienDBContext();
        // GET: DangKiHoiVien
        public ActionResult DangKi()
        {
            HoiVien model = new HoiVien(new ThongTinDoanhNghiep("", "", "", "", "", "", ""), 
                                        new NguoiDaiDien("", "", DateTime.Now, "", "", ""), 
                                        new NguoiLienHe("", "", DateTime.Now, "", "", ""));
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKi(HoiVien hoivien)
        {
            if (ModelState.IsValid)
            {
                if (hoivien.DongYDieuKhoan == true)
                {
                    //gui email
                    EmailOperation emailOperation = new EmailOperation();
                    string subject = "Thông báo đăng ký thành công";
                    string body = "Đã gửi đơn đăng ký";
                    emailOperation.SendEmail(hoivien.ThongTinDoanhNghiep.Email, subject, body);

                    dBContext.HoiVien.Add(hoivien);
                    dBContext.SaveChanges();
                    return RedirectToAction("DanhSach");
                }
                else
                {
                    ViewBag.XacNhanDieuKhoan = "Xác nhận điều khoản!";
                }
            }
            return View(hoivien);
        }
        public ActionResult DanhSach()
        {
            return View();
        }
    }
}