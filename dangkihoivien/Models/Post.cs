using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dangkihoivien.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Thông tin không được để trống!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Thông tin không được để trống!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Thông tin không được để trống!")]
        public string Content { get; set; }
        public string Thumbnail { get; set; }
        [Required]
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public int HoiVienId { get; set; }
        [ForeignKey("HoiVienId")]
        public HoiVien HoiVien { get; set; }
    }
}