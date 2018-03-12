using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMvcCourse.Models
{
    public class Member
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public string Email { get; set; }
        public string Birthday { get; set; }
    }
}