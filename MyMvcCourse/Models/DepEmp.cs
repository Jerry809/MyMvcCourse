using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMvcCourse.Models
{
    public class DepEmp
    {
        public List<Department> Departments { get; set; }
        public List<Employee> Employees { get; set; }
    }
}