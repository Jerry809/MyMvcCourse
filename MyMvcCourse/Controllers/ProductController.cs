using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Dapper;
using PagedList;
using MyMvcCourse.Models;

namespace MyMvcCourse.Controllers
{
    public class ProductController : Controller
    {
        private string _conn = System.Configuration.ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;
        private int _pageSize = 10;

        // GET: Product
        public ActionResult Index(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;

            using (var cn = new SqlConnection(_conn))
            {
                var query = "select * from 產品資料 order by 產品編號";
                var product = cn.Query<Product>(query);
                var result = product.ToPagedList(currentPage, _pageSize);
                return View(result);
            }
        }
    }
}