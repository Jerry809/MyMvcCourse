using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMvcCourse.Models;

namespace MyMvcCourse.Controllers
{
    public class NightMarketController : Controller
    {
        // GET: NightMarket
        public ActionResult Index()
        {
            var list = GenMarketsData();
            return View(list);
        }     

        public List<Market> GenMarketsData()
        {
            var markets = new List<Market>();
            for (int i = 1; i < 8; i++)
            {
                markets.Add(new Market()
                {
                    Id = $"A{i}",
                    Name = $"夜市{i}",
                    Address = $"地址{i}"
                });
            }
            return markets;
        }

    }
}