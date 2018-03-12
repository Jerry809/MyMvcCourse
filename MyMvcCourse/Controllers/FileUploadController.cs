using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMvcCourse.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase photo, string name, bool chk=false)
        {
            string fileName = "";
            if (photo != null)
            {
                if(photo.ContentLength > 0)
                {
                    fileName = Path.GetExtension(photo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Photos"), name + fileName);
                    photo.SaveAs(path);
                }
            }
            return RedirectToAction("ShowPhotos");
        }

        public string ShowPhotos()
        {
            string show = "";
            var dir = new DirectoryInfo(Server.MapPath("~/Photos"));
            FileInfo[] info = dir.GetFiles();

            int n = 0;
            foreach (var result in info)
            {
                n++;
                show += $"<a href='../Photos/{result.Name}' target='_blank'>";
                show += $"<img src='../Photos/{result.Name}' width='90' height='60' border='0'></a>";
                if (n % 4 == 0)
                {
                    show += "<p>";
                }
            }
            show += "<p><a href='Create'>返回</a></p>";
            return show;
        }
    }
}