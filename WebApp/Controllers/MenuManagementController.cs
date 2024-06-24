using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToolsApp.App_Start;
using ToolsApp.Authentication;
using ToolsApp.EntityFramework;
using ToolsApp.Models;
using ToolsApp.Utilities;

namespace ToolsApp.Areas.Admin.Controllers
{
    [Authorize]
    [CustomAuthorize(Function = "MenuManagement/Index")]
    public class MenuManagementController : BaseController
    {
        crmcustomscontext db_ = new crmcustomscontext();
        // GET: UserManagement
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _GetList(string MenuNameSearch)
        {
            MenuNameSearch = MenuNameSearch?.Trim();
            var List = db_.Menus.Where(p =>
            (MenuNameSearch == "" || MenuNameSearch == null || p.tenMenu.ToUpper().Contains(MenuNameSearch.ToUpper())) &&
            p.Id == p.Id && p.xacNhanXoa == false).ToList();
            ViewBag.List = List;
            return PartialView(new QLKTPMenuViewModel() );
        }
        public ActionResult _Insert(int Id)
        {
            ViewBag.Roots = db_.Menus.Where(p => p.parentId == null && (p.xacNhanXoa == null || p.xacNhanXoa == false)).ToList();

            ViewBag.Pages = db_.Pages.Where(p => p.xacNhanXoa == false).ToList();

            return PartialView(new QLKTPMenuViewModel { Id = 0 });
        }
        public ActionResult _Edit(int Id)
        {
            var model = db_.Menus.FirstOrDefault(p => p.Id == Id);

            ViewBag.Roots = db_.Menus.Where(p => p.parentId == null).ToList();

            ViewBag.Pages = db_.Pages.ToList();

            return PartialView(Mapper.MapFrom(model));
        }
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult _InsertFun(QLKTPMenuViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    if (model.iconMenu == "" || model.iconMenu == null)
                    {
                        return Json(new { status = -1, title = "", text = "Vui lòng nhập icon", obj = "" }, JsonRequestBehavior.AllowGet);

                    }
                    var model_copy = Mapper.MapFrom(model);
                    model_copy.ngayTao = DateTime.Now;
                    model_copy.nguoiTao = User.UserId;
                    model_copy.ngayCapNhat = DateTime.Now;
                    model_copy.nguoiCapNhat = User.UserId;
                    model_copy.ngayXoa = DateTime.Now;
                    model_copy.nguoiXoa = User.UserId;
                    model_copy.xacNhanXoa = false;
                    model_copy.hieuLuc = true;

                    #region Xử lý parent
                    model_copy.parentId = (model.parentId == 0 ? null : model.parentId);
                    model_copy.idPage = (model.idPage == 0 ? null : model.idPage);
                    #endregion

                    db_.Menus.Add(model_copy);
                    db_.SaveChanges();

                    return Json(new { status = 1, title = "", text = "Created.", obj = "" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { status = -1, title = "", text = ex.Message, obj = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { status = -1, title = "", text = "Error: " + UtilsLocal.ModelStateError(ModelState), obj = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult _EditFun(QLKTPMenuViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var item = db_.Menus.FirstOrDefault(p => p.Id == model.Id);
                    item.tenMenu = model.tenMenu;
                    item.iconMenu = model.iconMenu;
                    item.sortOrder = model.sortOrder;
                    item.ngayCapNhat = DateTime.Now;
                    item.nguoiCapNhat = User.UserId;

                    #region Xử lý parent
                    item.parentId = (model.parentId == 0 ? null : model.parentId);
                    item.idPage = (model.idPage == 0 ? null : model.idPage);
                    #endregion
                    db_.Entry(item).State = EntityState.Modified;
                    db_.SaveChanges();
                    return Json(new { status = 1, title = "", text = "Updated.", obj = "" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { status = -1, title = "", text = ex.Message, obj = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { status = -1, title = "", text = "Error: " + UtilsLocal.ModelStateError(ModelState), obj = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult _DeleteFun(int Id)
        {
            try
            {
                var item = db_.Menus.FirstOrDefault(p => p.Id == Id);
                item.ngayXoa = DateTime.Now;
                item.nguoiXoa = User.UserId;
                item.xacNhanXoa = true;
                db_.Entry(item).State = EntityState.Modified;
                db_.SaveChanges();
                return Json(new { status = 1, title = "", text = "Xóa thành công.", obj = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = -1, title = "", text = ex.Message, obj = "" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}