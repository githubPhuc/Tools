using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToolsApp.Authentication;
using ToolsApp.EntityFramework;

namespace ToolsApp.Controllers
{
    [Authorize]
    public class CartManagermentController : BaseController
    {
        // GET: CartManagerment
        crmcustomscontext db_ = new crmcustomscontext();
        public ActionResult Index()
        {
          
            return View();
        }
        public ActionResult GetList()
        {
            var data = db_.BaiViets.ToList();
            ViewBag.danhSachBaiViet = data;
            return PartialView();
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