using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MyMvcCourse.Models
{
    public class Market
    {
        [DisplayName("編號")]
        public string Id { get; set; }
        [DisplayName("名稱")]
        public string Name { get; set; }
        [DisplayName("地址")]
        public string Address { get; set; }
    }
}