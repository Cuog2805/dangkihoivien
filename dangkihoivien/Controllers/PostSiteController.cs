using dangkihoivien.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace dangkihoivien.Controllers
{
    public class PostSiteController : Controller
    {
        private DangKiHoiVienDBContext dbContext = new DangKiHoiVienDBContext();
        public ActionResult PostSiteIndex()
        {
            var postList = dbContext.Posts.Include(p => p.HoiVien)
                                            .Include(p => p.HoiVien.ThongTinDoanhNghiep)
                                            .ToList();
            return View(postList);
        }
        public ActionResult PostDetail(int? postId)
        {
            Post post = dbContext.Posts.Include(p => p.HoiVien)
                                       .Include(p => p.HoiVien.ThongTinDoanhNghiep)
                                       .FirstOrDefault(p => p.Id == postId);
            return View(post);
        }
        public ActionResult Posting() 
        {
            Post newPost = new Post(); 
            return View(newPost);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Posting(Post post)
        {
            post.HoiVien = dbContext.HoiVien.FirstOrDefault(hv => hv.Id == 1);

            if (string.IsNullOrEmpty(post.Title))
            {
                return View(post);
            }
            else if(string.IsNullOrEmpty(post.Description))
            {
                return View(post);
            }
            else if (string.IsNullOrEmpty(post.Content))
            {
                return View(post);
            }

            var file = Request.Files["Thumbnail"];

            if (file != null && file.ContentLength > 0)
            {
                string thumbnail = Path.GetFileName(file.FileName);
                string uploadFile = Server.MapPath("~/Asset/Image/" + thumbnail);

                file.SaveAs(uploadFile);
                post.Thumbnail = thumbnail;
            }

            dbContext.Posts.Add(post);
            dbContext.SaveChanges();

            return View(post);
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                string fileName = Path.GetFileName(upload.FileName);
                string path = Path.Combine(Server.MapPath("~/Asset/Image/"), fileName);
                upload.SaveAs(path);

                string url = Url.Content("~/Asset/Image/" + fileName);

                return Json(new { uploaded = 1, fileName = fileName, url = url });
            }
            return Json(new { uploaded = 0, error = new { message = "Upload failed" } });
        }
    }
}