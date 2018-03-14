using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMvcCourse.Controllers
{
    public class ShoppingCarController : Controller
    {
        //建立可存取ShoppingCar 的物件
        ShoppingCarEntities db = new ShoppingCarEntities();
        
        // GET: ShoppingCar
        public ActionResult Index()
        {
            var products = db.tProduct.ToList();
            //若session為空, 表示會員未登入
            if (Session["Member"] == null)
            {
                return View("Index", "_LayoutShoppingCar", products);
            }

            //會員己登入
            //指定Index.cshtml套用_LayoutMember.cshtml

            return View("Index", "_LayoutMember", products);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(tMember pMember)
        {
            if (ModelState.IsValid== false)
            {
                return View();
            }

            var member = db.tMember.FirstOrDefault(x => x.fUserId == pMember.fUserId);
            if (member == null)
            {
                db.tMember.Add(pMember);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            ViewBag.Message = "此帳號己存在, 註刪失敗";
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string fUserId, string fPwd)
        {
            var member = db.tMember.FirstOrDefault(x => x.fUserId == fUserId && x.fPwd == fPwd);
            if (member == null)
            {
                ViewBag.Message = "帳密錯誤, 登入失敗";
                return View();
            }
            Session["Welcome"] = member.fName + "歡迎光臨";
            Session["Member"] = member;
            return RedirectToAction("Index");

        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult ShoppingCar()
        {
            string fUserId = (Session["member"] as tMember).fUserId;
            var orderDetails = db.tOrderDetail.Where(x => x.fUserId == fUserId && x.fIsApproved == "否").ToList();
            return View("ShoppingCar", "_LayoutMember", orderDetails);
        }

        [HttpPost]
        public ActionResult ShoppingCar(string fReceiver, string fEmail, string fAddress)
        {
            string fUserId = (Session["Member"] as tMember).fUserId;
            string guid = Guid.NewGuid().ToString();
            var order = new tOrder();
            order.fOrderGuid = guid;
            order.fUserId = fUserId;
            order.fReceiver = fReceiver;
            order.fEmail = fEmail;
            order.fAddress = fAddress;
            order.fDate = DateTime.Now;
            db.tOrder.Add(order);

            var carList = db.tOrderDetail.Where(x => x.fIsApproved == "否" && x.fUserId == fUserId).ToList();
            carList.ForEach((item)=> {
                item.fIsApproved = "是";
                item.fOrderGuid = guid;
            });

            db.SaveChanges();
            return RedirectToAction("OrderList");
        }

        public ActionResult OrderList()
        {
            var fUserId = (Session["Member"] as tMember).fUserId;
            var orders = db.tOrder.Where(x => x.fUserId == fUserId).OrderByDescending(x => x.fDate).ToList();
            return View("OrderList", "_LayoutMember", orders);
        }

        public ActionResult OrderDetail(string fOrderGuid)
        {
            var detail = db.tOrderDetail.Where(x => x.fOrderGuid == fOrderGuid).ToList();
            return View("OrderDetail", "_LayoutMember", detail);
        }

        public ActionResult AddCar(string fPId)
        {
            string fUserId = (Session["Member"] as tMember).fUserId;
            var currentCar = db.tOrderDetail.FirstOrDefault(x => x.fPId == fPId && x.fIsApproved == "否"
                                && x.fUserId == fUserId);
            if (currentCar == null)
            {
                var product = db.tProduct.FirstOrDefault(x => x.fPId == fPId);
                var detail = new tOrderDetail();
                detail.fUserId = fUserId;
                detail.fPId = product.fPId;
                detail.fName = product.fName;
                detail.fPrice = product.fPrice;
                detail.fQty = 1;
                detail.fIsApproved = "否";
                db.tOrderDetail.Add(detail);
            }
            else
            {
                currentCar.fQty += 1;
            }
            db.SaveChanges();
            return RedirectToAction("ShoppingCar");
        }



        public ActionResult DeleteCar(int fId)
        {
            var detail = db.tOrderDetail.FirstOrDefault(x => x.fId == fId);
            db.tOrderDetail.Remove(detail);
            db.SaveChanges();
            return RedirectToAction("ShoppingCar");
        }
    }
}