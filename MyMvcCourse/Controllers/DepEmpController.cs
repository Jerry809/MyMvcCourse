using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using MyMvcCourse.Models;

namespace MyMvcCourse.Controllers
{
    public class DepEmpController : Controller
    {
        private string _conn = System.Configuration.ConfigurationManager.ConnectionStrings["Employee"].ConnectionString;
        // GET: DepEmp
        public ActionResult Index(int depId = 1)
        {
            var deps = new DepEmp();
            using (var cn = new SqlConnection(_conn))
            {
                var dept = cn.Query<Department>("Select * from tDepartment where fdepid = @depid",
                    new { depid = depId }).FirstOrDefault();
                ViewBag.DepName = dept?.fDepName + "部門";

                string query = @"SELECT * FROM tDepartment
                                 SELECT * FROM tEmployee where fdepId = @depId";
                using (var depEmp = cn.QueryMultiple(query, new { depId = depId }))
                {
                    deps.Departments = new List<Department>();
                    deps.Departments.AddRange(depEmp.Read<Department>());
                    deps.Employees = new List<Employee>();
                    deps.Employees.AddRange(depEmp.Read<Employee>());
                };
            }

            return View(deps);
        }

        public ActionResult Create()
        {

            using (var cn = new SqlConnection(_conn))
            {
                var query = "select * from tDepartment";
                var dep = cn.Query<Department>(query);
                return View(dep);
            }
        }

        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            using (var cn = new SqlConnection(_conn))
            {
                var query = "Insert Into tEmployee (fEmpId, fName, fPhone, fDepId)" +
                    "                   values(@fempId, @fname, @fphone, @fdepid)";
                var result = cn.Execute(query, emp);
            }
            return RedirectToAction("Index", new { depId = emp.fDepId });
        }

        public ActionResult Delete(string fEmpId)
        {
            var emp = new Employee();
            using (var cn = new SqlConnection(_conn))
            {
                var query = "select * from tEmployee where fempId = @empId";
                emp = cn.QueryFirstOrDefault<Employee>(query, new { empId = fEmpId });
                query = "delete from tEmployee where fempId = @empId";
                var result = cn.Execute(query, new { empId = fEmpId });

                if (result==1)
                {
                    TempData["IsDelete"] = true;
                }
                else
                {
                    TempData["IsDelete"] = false;
                }
            }
            return RedirectToAction("Index", new { depId = emp.fDepId });
        }
    }
}