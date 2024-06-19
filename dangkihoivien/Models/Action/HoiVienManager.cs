using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dangkihoivien.Models
{
    public class HoiVienManager
    {
        private DangKiHoiVienDBContext dbContext;
        public HoiVienManager(DangKiHoiVienDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int HoiVienCount() { return dbContext.HoiVien.Count(); }
        public int HoiVienDaDuyetCount() { return dbContext.HoiVien.Count(item => item.IsActive); }
        public int HoiVienChuaDuyetCount() { return dbContext.HoiVien.Count(item => item.IsDenided==false && item.IsActive==false); }
        public int HoiVienBiTuChoi() { return dbContext.HoiVien.Count(item => item.IsDenided); }
    }
}