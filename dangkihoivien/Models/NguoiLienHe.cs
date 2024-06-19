using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dangkihoivien.Models
{
    public class NguoiLienHe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NguoiLienHeId { get; set; }
        [Required(ErrorMessage = "Thông tin không dược để trống!")]
        public string Hoten { get; set; }
        public string Chucvu { get; set; }
        [Required(ErrorMessage = "Thông tin không dược để trống!")]
        public DateTime Ngaysinh { get; set; }
        [Required(ErrorMessage = "Thông tin không dược để trống!")]
        [Phone(ErrorMessage ="Số điện thoại không hợp lệ!")]
        public string Sdt {  get; set; }
        [Required(ErrorMessage = "Thông tin không dược để trống!")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ!")]
        public string Zalo { get; set; }
        [Required(ErrorMessage = "Thông tin không dược để trống!")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ!")]
        public string Email { get; set; }

        public NguoiLienHe() { }
        public NguoiLienHe(string hoten, string chucvu, DateTime ngaysinh, string sdt, string zalo, string email)
        {
            Hoten = hoten;
            Chucvu = chucvu;
            Ngaysinh = ngaysinh;
            Sdt = sdt;
            Zalo = zalo;
            Email = email;
        }
    }
}