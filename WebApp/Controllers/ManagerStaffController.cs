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
                            (string.IsNullOrEmpty(FullnameSearch) || (a.hoVaTen).ToUpper().Contains(FullnameSearch.ToUpper()))
                        ).ToList();
            ViewBag.list = list;
            var dataUser = db_.Users.Where(a => a.tenTaiKhoan == User.tenTaiKhoan).FirstOrDefault();
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
        public JsonResult EditUser(RegisterViewModel model, int Id)
        {
            var item = db_.Users.FirstOrDefault(p => p.Id == Id);

            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(model.hoVaTen))
                    {
                        return Json(new { status = -1, title = "", text = "Chưa nhập Họ và tên.", obj = "" }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(model.email.Trim()))
                        {
                            return Json(new { status = -1, title = "", text = "Email không được để trống.", obj = "" }, JsonRequestBehavior.AllowGet);

                        }

                    }
                    item.hoVaTen = model.hoVaTen;
                    item.tenTaiKhoan = model.tenTaiKhoan;
                    item.email = model.email;
                    item.soDienThoai = model.soDienThoai;
                    item.ngayCapNhat = DateTime.Now;
                    item.nguoiCapNhat = User.UserId;
                    db_.Entry(item).State = EntityState.Modified;
                    db_.SaveChanges();
                    return Json(new { status = 1, title = "", text = "Cập nhật thành công.", obj = "" }, JsonRequestBehavior.AllowGet);
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
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> SaveNewPassword(string Username, string newPass)
        {
            try
            {
                //// Phân tích chuỗi JSON thành danh sách đối tượng


                //using (HttpClient client = new HttpClient())
                //{
                //    var data = new { Username = Username, newPass = newPass };
                //    string token = User.token;
                //    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                //    // Gửi GET request tới API
                //    HttpResponseMessage response = await client.PostAsJsonAsync($"https://api.hoanglongsecurity.info/api/Authenticate/ChangePassword?Username={data.Username}&newPass={data.newPass}", data);
                //    HttpStatusCode statusCode = response.StatusCode;
                //    string responseData = await response.Content.ReadAsStringAsync();
                //    dynamic json = JsonConvert.DeserializeObject<dynamic>(responseData);
                //    ResponseObject responseObject = JsonConvert.DeserializeObject<ResponseObject>(responseData);
                //    if (response.StatusCode.ToString() == "200" || response.ReasonPhrase == "OK")
                //    {
                        return Json(new { status = 1, title = "", text = "", obj = "" }, JsonRequestBehavior.AllowGet);


                //    }
                //    else
                //    {
                //        return Json(new { status = -1, title = "", text = responseObject.message, obj = "" }, JsonRequestBehavior.AllowGet);
                //    }
                //}
            }
            catch (Exception ex)
            {
                return Json(new { status = -1, title = "", text = "Lỗi: Không cấu trúc api", obj = "" }, JsonRequestBehavior.AllowGet);
            }


        }

    }

}

 