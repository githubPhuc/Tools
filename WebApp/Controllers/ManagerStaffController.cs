using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ToolsApp.Authentication;
using ToolsApp.EntityFramework;
using ToolsApp.Models;
using ToolsApp.Utilities;

namespace ToolsApp.Controllers
{
    [Authorize]
    [CustomAuthorize(Function = "ManagerStaff/Index")]
    public class ManagerStaffController : BaseController
    {
        crmcustomscontext db_ = new crmcustomscontext();
        public ActionResult Index()
        {
            var dataAccountType =  db_.Configs.Where(a => a.parentId == 1).ToList();
            ViewBag.dataAccountType = dataAccountType;
            return View();
        }
        public ActionResult _Image_View(string Id)
        {
            ViewData["Id"] = Id;
            return PartialView();
        }
        public ActionResult _GetList(string UsernameSearch, string AccountType, string FullnameSearch)
        {
            UsernameSearch = UsernameSearch?.Trim();
            FullnameSearch = FullnameSearch?.Trim();
            var list = db_.Users.Where(a =>
                            (string.IsNullOrEmpty(UsernameSearch) || a.tenTaiKhoan.ToUpper().Contains(UsernameSearch.ToUpper())) &&
                            (string.IsNullOrEmpty(FullnameSearch) || (a.hoVaTen).ToUpper().Contains(FullnameSearch.ToUpper())) && a.xacNhanXoa == false
                        ).ToList();
            ViewBag.list = list;
            var dataUser = db_.Users.Where(a => a.tenTaiKhoan == User.tenTaiKhoan).FirstOrDefault();
            var dataAccountType =  db_.Configs.Where(a => a.parentId == 1).ToList();
            ViewBag.dataAccountType = dataAccountType;
            ViewBag.dataUser = dataUser;
            return PartialView();
        }
        public ActionResult ViewInfomation(int Id)
        {
            return RedirectToAction("UserProfile", "InfomationUser", new { id = Id });
        }
        public ActionResult _Insert_View()
        {
          
            return PartialView();
        }
        public ActionResult _Update_View(int Id)
        {

            var data = db_.Users.FirstOrDefault(p => p.Id == Id);
            ViewBag.model = data;
            return PartialView();
        }
        public ActionResult ChangePassword(int Id)
        {

            var user = db_.Users.FirstOrDefault(p => p.Id == Id);
            ViewBag.user = user;
            return PartialView();
        }
        public async Task<ActionResult> ChangeRole(int id)
        {
            var user = db_.Users.FirstOrDefault(a => a.Id == id);
            var dataAccountType = await db_.Configs.AsNoTracking().Where(a => a.parentId == 1).ToListAsync();
            ViewBag.dataAccountType = dataAccountType;
            ViewBag.user = user;

            return PartialView();
        }
        public ActionResult ShowLog(int id)
        {
            DateTime currentDate = DateTime.Now.Date;
            DateTime threeDaysAgo = currentDate.AddDays(-2);
            var logData = db_.LogHistorys
                .Where(a => a.idUser == id && DbFunctions.TruncateTime(a.ngayTao) >= threeDaysAgo && DbFunctions.TruncateTime(a.ngayTao) <= currentDate)
                .OrderByDescending(a => a.ngayTao)
                .ThenByDescending(a => a.Id)
                 .Select(a => new LogHistoryViewModel
                 {
                     Id = a.Id,
                     idUser = a.idUser,
                     moTa = a.moTa,
                     hoVaTen = User.hoVaTen,
                     tenTaiKhoan = User.tenTaiKhoan,
                     moTaChiTiet = a.moTaChiTiet,
                     ngayTao = a.ngayTao,
                     nguoiTao = a.nguoiTao,
                     ipUserHostAddress = a.ipUserHostAddress
                 })
                .ToList();

            if (logData.Count == 0)
            {
                return Json(new { success = false, message = "Người dùng này chưa ghi lại bất kỳ log nào:" }, JsonRequestBehavior.AllowGet);
            }

            ViewData["id"] = id;
            ViewBag.logData = logData;
            db_.SaveChanges();

            return PartialView();
        }
        public JsonResult _ChangeRoleFun(int Id, int capDoTaiKhoan)
        {
            var ParenUser = db_.Users.FirstOrDefault(a => a.Id == User.UserId);
            if (ParenUser.capDoTaiKhoan == 5)
            {
                return Json(new { status = -1, title = "", text = "Bạn không có quyền truy cập chức năng này.", obj = "" }, JsonRequestBehavior.AllowGet);

            }
            var user = db_.Users.FirstOrDefault(a => a.Id == Id);
            if (user == null)
            {
                return Json(new { status = -1, title = "", text = "Lỗi không tìm thấy người dùng.", obj = "" }, JsonRequestBehavior.AllowGet);

            }
            user.capDoTaiKhoan = capDoTaiKhoan;
            db_.SaveChanges();
            return Json(new { status = 1, title = "", text = "Cập nhật thành công.", obj = "" }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult _Lock_User(int Id)
        {
            var ParenUser = db_.Users.FirstOrDefault(a => a.Id == User.UserId);
            if (ParenUser.capDoTaiKhoan == 5 )
            {
                return Json(new { status = -1, title = "", text = "Bạn không có quyền truy cập chức năng này.", obj = "" }, JsonRequestBehavior.AllowGet);

            }
            var user = db_.Users.FirstOrDefault(p => p.Id == Id);
            if (user == null)
            {
                return Json(new { status = -1, title = "", text = "Lỗi không tìm thấy người dùng.", obj = "" }, JsonRequestBehavior.AllowGet);

            }
            user.hieuLuc = false;
            user.ngayCapNhat = DateTime.Now;
            user.nguoiCapNhat = User.UserId;
            db_.SaveChanges();
            return Json(new { status = 1, title = "", text = "Khóa người dùng thành công. Tài khoản này sẽ không thể đăng nhập.", obj = "" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult _UnLock_User(int Id)
        {
            var ParenUser = db_.Users.FirstOrDefault(a => a.Id == User.UserId);
            if (ParenUser.capDoTaiKhoan == 5)
            {
                return Json(new { status = -1, title = "", text = "Bạn không có quyền truy cập chức năng này.", obj = "" }, JsonRequestBehavior.AllowGet);

            }
            var user = db_.Users.FirstOrDefault(p => p.Id == Id);
            if (user == null)
            {
                return Json(new { status = -1, title = "", text = "Lỗi không tìm thấy người dùng.", obj = "" }, JsonRequestBehavior.AllowGet);

            }
            user.hieuLuc = true;
            user.nguoiCapNhat = User.UserId;
            user.ngayCapNhat = DateTime.Now;
            db_.SaveChanges();
            return Json(new { status = 1, title = "", text = "Mở người dùng thành công.", obj = "" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult _Delete(int Id)
        {
            var ParenUser = db_.Users.FirstOrDefault(a => a.Id == User.UserId);
            if (ParenUser.capDoTaiKhoan == 5)
            {
                return Json(new { status = -1, title = "", text = "Bạn không có quyền truy cập chức năng này.", obj = "" }, JsonRequestBehavior.AllowGet);

            }
            var user = db_.Users.FirstOrDefault(p => p.Id == Id);
            if (user == null)
            {
                return Json(new { status = -1, title = "", text = "Lỗi không tìm thấy người dùng.", obj = "" }, JsonRequestBehavior.AllowGet);

            }
            user.xacNhanXoa = true;
            user.ngayXoa = DateTime.Now;
            user.nguoiXoa = User.UserId;
            db_.SaveChanges();
            return Json(new { status = 1, title = "", text = "Xóa người dùng thành công.", obj = "" }, JsonRequestBehavior.AllowGet);
        }
       
    }

}

 