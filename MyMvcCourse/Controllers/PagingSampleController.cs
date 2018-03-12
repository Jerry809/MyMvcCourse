using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMvcCourse.Models;
using PagedList;

namespace MyMvcCourse.Controllers
{
    public class PagingSampleController : Controller
    {
        // GET: PagingSample
        public ActionResult Index(int page = 1)
        {
            int pagesize = 4;
            int pagecurrent = page < 1 ? 1 : page;
            var list = GenMarkets();
            var pagedList = list.ToPagedList(pagecurrent, pagesize);
            return View(pagedList);
        }

        public List<Market> GenMarkets()
        {
            var markets = new List<Market>();
            for (int i = 0; i < 20; i++)
            {
                markets.Add(new Market()
                {
                    Id = $"A{i}",
                    Name = $"夜市第{ i }間",
                    Address = $"地址在{i}"
                });
            }

            return markets;
        }
    }
}