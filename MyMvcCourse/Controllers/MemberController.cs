using MyMvcCourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMvcCourse.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Member member)
        {
            string msg = "";
            msg = $"註冊資料如下 : <br> " +
                  $"帳號: {member.UserId} <br>" +
                  $"密碼: {member.Pwd} <br>" +
                  $"姓名: {member.Name} <br>" +
                  $"信箱: {member.Email} <br>" +
                  $"生日: {member.Birthday} <br> ";

            ViewBag.Msg = msg;
            return View(member);
        }
    }
}