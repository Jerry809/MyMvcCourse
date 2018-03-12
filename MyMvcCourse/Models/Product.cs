using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMvcCourse.Models
{
    public class Product
    {
        public int 產品編號 { get; set; }
        public string 產品 { get; set; }
        public Nullable<int> 供應商編號 { get; set; }
        public Nullable<int> 類別編號 { get; set; }
        public string 單位數量 { get; set; }
        public Nullable<decimal> 單價 { get; set; }
        public Nullable<short> 庫存量 { get; set; }
        public Nullable<short> 已訂購量 { get; set; }
        public Nullable<short> 安全存量 { get; set; }
        public bool 不再銷售 { get; set; }
    }
}