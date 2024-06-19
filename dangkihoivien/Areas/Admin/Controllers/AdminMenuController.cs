using dangkihoivien.App_Start;
using dangkihoivien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using System.Net.Mail;
using dangkihoivien.Models.Action;

namespace dangkihoivien.Areas.Admin.Controllers
{
    public class AdminMenuController : Controller
    {
        private DangKiHoiVienDBContext _dbContext = new DangKiHoiVienDBContext();
        public ActionResult AdminError() 
        {
            return View();
        }
        public ActionResult Index()
        {
            if (Session["adminSession"] == null)
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            else
            {
                return View();
            }
        }

        public PartialViewResult ThongTinHoiVien()
        {
            HoiVienManager hvThongSo = new HoiVienManager(_dbContext);
            return PartialView("ThongTinHoiVien", hvThongSo);
        }
        public PartialViewResult BangHoiVienDoiDuyet()
        {
            List<HoiVien> dsHoivien = _dbContext.HoiVien
                .Include("ThongTinDoanhNghiep")
                .Include("NguoiDaiDien")
                .Include("NguoiLienHe")
                .Include("NguoiDuyet")
                .Where(hv => hv.IsActive==false && hv.IsDenided==false)
                .ToList();
            return PartialView("BangHoiVienDoiDuyet", dsHoivien);
        }
        public PartialViewResult BangHoiVienDaDuyet()
        {
            List<HoiVien> dsHoivien = _dbContext.HoiVien
                .Include("ThongTinDoanhNghiep")
                .Include("NguoiDaiDien")
                .Include("NguoiLienHe")
                .Include("NguoiDuyet")
                .Where(hv => hv.IsActive == true)
                .ToList();
            return PartialView("BangHoiVienDaDuyet", dsHoivien);
        }
        [CustomAuthorize("Manager")]
        [HttpPost]
        public JsonResult DuyetHoiVien(int idHoivien)
        {

            HoiVien hvFind = _dbContext.HoiVien
                .Include("ThongTinDoanhNghiep")
                .Include("NguoiDaiDien")
                .Include("NguoiLienHe")
                .Include("NguoiDuyet")
                .FirstOrDefault(hv => hv.Id == idHoivien);
            hvFind.IsActive = true;

            //gui email
            EmailOperation emailOperation = new EmailOperation();
            string subject = "Thông báo đăng ký thành công";
            string body = "Đăng ký đã được duyệt";
            emailOperation.SendEmail(hvFind.ThongTinDoanhNghiep.Email, subject, body);

            string adminUsername = (string)Session["adminSession"];
            QuanTriVien admin = _dbContext.Admins.FirstOrDefault(ad =>
                                    ad.Username == adminUsername);
            hvFind.QuanTriVienId = admin.QuanTriVienId;
            _dbContext.SaveChanges();
            HoiVienManager hvThongSo = new HoiVienManager(_dbContext);
            var response = new
            {
                permission = true,
                hvDaduyet = hvThongSo.HoiVienDaDuyetCount(),
                hvChuaduyet = hvThongSo.HoiVienChuaDuyetCount(),
                hvId = hvFind.Id,
                hvTendoanhnghiep = hvFind.ThongTinDoanhNghiep.Tendoanhnghiep,
                hvMasothue = hvFind.ThongTinDoanhNghiep.Masothue,
                hvDiachi = hvFind.ThongTinDoanhNghiep.Diachi,
                hvDienthoai = hvFind.ThongTinDoanhNghiep.Dienthoai,
                hvZalo = hvFind.ThongTinDoanhNghiep.Zalo,
                hvEmail = hvFind.ThongTinDoanhNghiep.Email,
                hvWebsite = hvFind.ThongTinDoanhNghiep.Website,
                nddHoten = hvFind.NguoiDaiDien.Hoten,
                nddChucvu = hvFind.NguoiDaiDien.Chucvu,
                nddNgaySinh = hvFind.NguoiDaiDien.Ngaysinh.ToShortDateString(),
                nddSdt = hvFind.NguoiDaiDien.Sdt,
                nddZalo = hvFind.NguoiDaiDien.Zalo,
                nddEmail = hvFind.NguoiDaiDien.Email,
                nlhHoten = hvFind.NguoiLienHe.Hoten,
                nlhChucvu = hvFind.NguoiLienHe.Chucvu,
                nlhNgaySinh = hvFind.NguoiLienHe.Ngaysinh.ToShortDateString(),
                nlhSdt = hvFind.NguoiLienHe.Sdt,
                nlhZalo = hvFind.NguoiLienHe.Zalo,
                nlhEmail = hvFind.NguoiLienHe.Email,
                admin = hvFind.NguoiDuyet.Username
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize("Manager")]
        [HttpPost]
        public JsonResult TuChoiHoiVien(int idHoivien)
        {
            HoiVien hvFind = _dbContext.HoiVien.FirstOrDefault(hv => hv.Id == idHoivien);
            hvFind.IsDenided = true;
            string adminUsername = (string)Session["adminSession"];
            QuanTriVien admin = _dbContext.Admins.FirstOrDefault(ad =>
                                    ad.Username == adminUsername);
            hvFind.QuanTriVienId = admin.QuanTriVienId;
            _dbContext.SaveChanges();
            HoiVienManager hvThongSo = new HoiVienManager(_dbContext);
            var response = new
            {
                permission = true,
                hvBituchoi = hvThongSo.HoiVienBiTuChoi(),
                hvChuaduyet = hvThongSo.HoiVienChuaDuyetCount()
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize("Manager")]
        [HttpPost]
        public JsonResult HuyDuyetHoiVien(int idHoivien)
        {
            HoiVien hvFind = _dbContext.HoiVien.FirstOrDefault(hv => hv.Id == idHoivien);
            hvFind.IsActive = false;
            hvFind.QuanTriVienId = null;
            _dbContext.SaveChanges();
            HoiVienManager hvThongSo = new HoiVienManager(_dbContext);
            var response = new
            {
                permission = true,
                hvDaduyet = hvThongSo.HoiVienDaDuyetCount(),
                hvChuaduyet = hvThongSo.HoiVienChuaDuyetCount(),
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}