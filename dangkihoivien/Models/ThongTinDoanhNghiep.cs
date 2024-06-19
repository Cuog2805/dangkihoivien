using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dangkihoivien.Models
{
    public class ThongTinDoanhNghiep
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ThongTinDoanhNghiepId { get; set; }
        [Required(ErrorMessage = "Thông tin không dược để trống!")]
        public string Tendoanhnghiep { get; set; }
        [Required(ErrorMessage = "Thông tin không dược để trống!")]
        public string Masothue { get; set; }
        [Required(ErrorMessage = "Thông tin không dược để trống!")]
        public string Diachi { get; set; }
        [Required(ErrorMessage = "Thông tin không dược để trống!")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ!")]
        public string Dienthoai { get; set; }
        [Required(ErrorMessage = "Thông tin không dược để trống!")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ!")]
        public string Zalo { get; set; }
        [Required(ErrorMessage = "Thông tin không dược để trống!")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        public string Email { get; set; }
        public string Website { get; set; }

        public ThongTinDoanhNghiep() { }
        public ThongTinDoanhNghiep(string tendoanhnghiep, string masothue, string diachi, string dienthoai, string zalo, string email, string website)
        {
            Tendoanhnghiep = tendoanhnghiep;
            Masothue = masothue;
            Diachi = diachi;
            Dienthoai = dienthoai;
            Zalo = zalo;
            Email = email;
            Website = website;
        }
    }
}