using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToolsApp.Authentication;

namespace ToolsApp.Controllers
{
    public class CartManagermentController : BaseController
    {
        // GET: CartManagerment
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Insert()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult hinhAnh_View()
        {
            return PartialView();
        }
        public ActionResult ThongTinBaiViet_View()
        {
            return PartialView();
        }
        public ActionResult ThongTinKhachHang_View()
        {
            return PartialView();
        }

    }
}