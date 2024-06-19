using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace dangkihoivien.Models
{
    public class DangKiHoiVienDBContext : DbContext
    {
        public DbSet<NguoiDaiDien> NguoiDaiDiens { get; set; }
        public DbSet<NguoiLienHe> NguoiLienHes { get; set; }
        public DbSet<ThongTinDoanhNghiep> ThongTinDoanhNghieps { get; set; }
        public DbSet<HoiVien> HoiVien { get; set; }
        public DbSet<QuanTriVien> Admins { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostImage> PostsImage { get; set; }
        public DangKiHoiVienDBContext(): base("name=DangKiHoiVienDBContext")
        { 
        }
    }
}