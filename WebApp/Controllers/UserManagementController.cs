using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
    [CustomAuthorize(Function = "UserManagement/Index")]
    public class UserManagementController : BaseController
    {
        crmcustomscontext db_ = new crmcustomscontext();


        // GET: UserManagement
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _GetList(string UsernameSearch, string EmailSearch)
        {
            UsernameSearch = UsernameSearch?.Trim();
            EmailSearch = EmailSearch?.Trim();
            var List = db_.Users.Where(p =>
            (UsernameSearch == "" || UsernameSearch == null || p.tenTaiKhoan.ToUpper().Contains(UsernameSearch.ToUpper())) &&
            (EmailSearch == "" || EmailSearch == null || p.email.ToUpper().Contains(EmailSearch.ToUpper()))).ToList();
            var lst = List.Select(p => p.tenTaiKhoan).ToList();
            ViewBag.List = List;
            return PartialView(new UserViewModel());
        }
        public async Task<ActionResult> _Insert(int? id)
        {
            var dataAccountType =await db_.Configs.AsNoTracking().Where(a => a.parentId == 1).ToListAsync();
            ViewBag.dataAccountType = dataAccountType;
            return PartialView();
        }
        public ActionResult _Edit(int Id)
        {
            var model = db_.Users.FirstOrDefault(p => p.Id == Id);
            #region Load page
            var pages = db_.Pages.ToList();
            ViewBag.pages = pages;
            #endregion                                    

            return PartialView(model);
        }
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult _InsertFun(string tenTaiKhoan, string matKhau, string hoVaTen, string soDienThoai, string email, int capDoTaiKhoan)
        {
           
            try
            {
                if(string.IsNullOrEmpty(tenTaiKhoan))
                {
                    return Json(new { status = -1, title = "", text = "Tên tài khoản không được để trống@@", obj = "" }, JsonRequestBehavior.AllowGet);

                }
                if(string.IsNullOrEmpty(matKhau))
                {
                    return Json(new { status = -1, title = "", text = "Mật khẩu không được để trống@@", obj = "" }, JsonRequestBehavior.AllowGet);

                }
                if(string.IsNullOrEmpty(hoVaTen))
                {
                    return Json(new { status = -1, title = "", text = "Họ và tên không được để trống@@", obj = "" }, JsonRequestBehavior.AllowGet);

                }
                if(string.IsNullOrEmpty(soDienThoai))
                {
                    return Json(new { status = -1, title = "", text = "Số điện thoại không được để trống@@", obj = "" }, JsonRequestBehavior.AllowGet);

                }
                if(string.IsNullOrEmpty(email))
                {
                    return Json(new { status = -1, title = "", text = "Email không được để trống@@", obj = "" }, JsonRequestBehavior.AllowGet);

                }
                if(capDoTaiKhoan ==0)
                {
                    return Json(new { status = -1, title = "", text = "Vui lòng chọn loại tài khoản@@", obj = "" }, JsonRequestBehavior.AllowGet);
                }
                var checkUser = db_.Users.Where(a=>a.tenTaiKhoan == tenTaiKhoan).FirstOrDefault();
                if(checkUser != null)
                {
                    return Json(new { status = -1, title = "", text = "Tên tài khoản đã tồn tại!!", obj = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = new User()
                    {
                        tenTaiKhoan = tenTaiKhoan,
                        matKhau = matKhau,
                        hoVaTen = hoVaTen,
                        email = email,
                        soDienThoai = soDienThoai,
                        capDoTaiKhoan = capDoTaiKhoan,
                        hieuLuc = true,
                        ngayTao = DateTime.Now,
                        nguoiTao = User.UserId,
                        ngayCapNhat = DateTime.Now,
                        nguoiCapNhat = User.UserId,
                        ngayXoa = DateTime.Now,
                        nguoiXoa = User.UserId,
                        xacNhanXoa = false,
                    };
                    db_.Users.Add(data);
                    db_.SaveChanges();
                    return Json(new { status = 1, title = "", text = "Tạo tài khoản thành công!!", obj = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = -1, title = "", text = ex.Message, obj = "" }, JsonRequestBehavior.AllowGet);
            }
         
        }

        //Edit role
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult _EditFun(QL_UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var item = db_.Users.FirstOrDefault(p => p.Id == model.Id);
                    if (item == null)
                    {
                        return Json(new { status = -1, title = "", text = "User không tồn tại!!.", obj = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        #region Page
                        var Lst = db_.UserAuthorizations.Where(a => a.idUser == model.Id).ToList();

                        if (model.Pages == null)
                        {
                            db_.UserAuthorizations.RemoveRange(Lst);
                            db_.Entry(item).State = EntityState.Modified;
                            db_.SaveChanges();
                            return Json(new { status = 1, title = "", text = "Cập nhật thành công.", obj = "" }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.Pages != null && model.Pages.Length > 0)
                        {
                            if (Lst != null && Lst.Count() > 0)
                            {
                                foreach (var data in Lst)
                                {
                                    db_.UserAuthorizations.Remove(data);
                                }
                            }
                            db_.SaveChanges();
                            #endregion
                            var Pages = model.Pages.Split(',');
                            foreach (var item_ in Pages)
                            {
                                var data = new UserAuthorization
                                {
                                    idUser = model.Id,
                                    idPage = int.Parse(item_),
                                    ngayTao = DateTime.Now,
                                    nguoiTao = User.UserId,
                                    ngayCapNhat = DateTime.Now,
                                    nguoiCapNhat = User.UserId,
                                    ngayXoa = DateTime.Now,
                                    nguoiXoa = User.UserId,
                                    xacNhanXoa = false,
                                    hieuLuc = true,
                                };
                                item.UserAuthorizations.Add(data);
                            }
                            db_.Entry(item).State = EntityState.Modified;
                            db_.SaveChanges();
                            return Json(new { status = 1, title = "", text = "Cập nhật thành công.", obj = "" }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { status = -1, title = "", text = "Upload false page is null.", obj = "" }, JsonRequestBehavior.AllowGet);

                    }

                }
                catch (Exception ex)
                {
                    return Json(new { status = -1, title = "", text = ex.Message, obj = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

                return Json(new { status = -1, title = "", text = "Error: " + message, obj = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public JsonResult _DeleteFun(QL_UserViewModel model)
        {
            try
            {
                var item = db_.Users.FirstOrDefault(p => p.Id == model.Id);
                if (item == null)
                {
                    return Json(new { status = -1, title = "", text = "User không tồn tại!!.", obj = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var Lst = db_.UserAuthorizations.Where(a => a.idUser == model.Id).ToList();

                    if (Lst.Count() == 0)
                    {
                        return Json(new { status = -1, title = "", text = "Người dùng này chưa được phân quyền.", obj = "" }, JsonRequestBehavior.AllowGet);
                    }
                    if (Lst.Count() > 0)
                    {
                        foreach (var data in Lst)
                        {
                            db_.UserAuthorizations.Remove(data);
                        }
                    }
                    db_.SaveChanges();
                    return Json(new { status = 1, title = "", text = "Xóa quyền thành công.", obj = "" }, JsonRequestBehavior.AllowGet);
                }
            }   
            catch (Exception ex)
            {
                return Json(new { status = -1, title = "", text = ex.Message, obj = "" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
