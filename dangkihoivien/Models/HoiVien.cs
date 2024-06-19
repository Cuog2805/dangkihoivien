using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dangkihoivien.Models
{
    public class HoiVien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ThongTinDoanhNghiepId { get; set; }
        [ForeignKey("ThongTinDoanhNghiepId")]
        public ThongTinDoanhNghiep ThongTinDoanhNghiep { get; set; }
        public int NguoiDaiDienId { get; set; } 

        [ForeignKey("NguoiDaiDienId")]
        public NguoiDaiDien NguoiDaiDien { get; set; }
        public int NguoiLienHeId { get; set; }

        [ForeignKey("NguoiLienHeId")]
        public NguoiLienHe NguoiLienHe { get; set; }
        public int? QuanTriVienId { get; set; }

        [ForeignKey("QuanTriVienId")]
        public QuanTriVien NguoiDuyet { get; set; }
        public bool DongYDieuKhoan { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public bool IsDenided { get; set; } = false;
        public HoiVien() { }
        public HoiVien(ThongTinDoanhNghiep thongTinDoanhNghiep, NguoiDaiDien nguoiDaiDien, NguoiLienHe nguoiLienHe)
        {
            ThongTinDoanhNghiep = thongTinDoanhNghiep;
            NguoiDaiDien = nguoiDaiDien;
            NguoiLienHe = nguoiLienHe;
        }
    }
}