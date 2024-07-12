using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToolsApp.EntityFramework;

namespace ToolsApp.Authentication
{
    public class BaseController : Controller
    {
            protected new CustomPrincipal User
            { 
                get {return HttpContext.User as CustomPrincipal; }
            }
        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            bool UserHasPermission = false;
            string actionName = context.ActionDescriptor.ActionName;
            string controllerName = context.ActionDescriptor.ControllerDescriptor.ControllerName;
            string httpMethod = context.HttpContext.Request.HttpMethod;
            string action = $"User accessed {httpMethod}/ {controllerName}/{actionName}";
            string ipAddress = context.HttpContext.Request.UserHostAddress;
            var postData = context.HttpContext.Request.Form;
            if (User != null)
            {
                using (var db_ = new ToolsApp.EntityFramework.crmcustomscontext())
                {
                    string log = "Người dùng thực hiện action ";
                    log += actionName + " ";
                    if(httpMethod=="POST" )
                    {
                        foreach (var keyObj in postData.Keys)
                        {
                            string key = keyObj.ToString();
                            string value = "";
                            try
                            {
                                value = postData[key].ToString();
                                log += key + " = " + value +", ";
                            }
                            catch (Exception e)
                            {
                                continue;
                            }
                        }
                    }    
                    var history = new LogHistory
                    {
                        idUser = User.UserId,
                        moTa = "Người dùng thực hiện action" + (httpMethod == "POST" ? " thêm dữ liệu" : httpMethod == "PUT" ? " cập nhật dữ liệu" : httpMethod == "DELETE" ? " xóa dữ liệu" : " xem hoặc tìm kiếm dữ liệu"),
                        moTaChiTiet = log,
                        ipUserHostAddress = ipAddress,
                        ngayTao = DateTime.UtcNow.AddHours(7),
                        nguoiTao = User.UserId,
                        ngayCapNhat = DateTime.UtcNow.AddHours(7),
                        nguoiCapNhat = User.UserId,
                        ngayXoa = DateTime.UtcNow.AddHours(7),
                        nguoiXoa = User.UserId,
                        xacNhanXoa = false,
                        hieuLuc = true,
                    };
                    db_.LogHistorys.Add(history);
                    db_.SaveChanges();
                    if (controllerName != "Home")
                    {
                        var page = db_.UserAuthorizations.Where(p => p.Page.controllerName == controllerName && p.idUser == User.UserId).FirstOrDefault();
                        #region User Not Page
                        //if (page == null)
                        //{
                        //    context.HttpContext.Response.Redirect("~/Home/PageNotFound");
                        //    return;
                        //}
                        //else
                        //{
                        //    switch (httpMethod)
                        //    {
                        //        case "GET":
                        //            if (page.permissionGet == false)
                        //            {
                        //                context.HttpContext.Response.Redirect("~/Home/NotAuthentizace");
                        //                return;
                        //            }
                        //            break;
                        //        case "POST":
                        //            if (page.permissionPost == false)
                        //            {
                        //                context.HttpContext.Response.Redirect("~/Home/NotAuthentizace");
                        //                return;
                        //            }
                        //            break;
                        //        case "PUT":
                        //            if (page.permissionPut == false)
                        //            {
                        //                context.HttpContext.Response.Redirect("~/Home/NotAuthentizace");
                        //                return;
                        //            }
                        //            break;
                        //        case "DELETE":
                        //            if (page.permissionDelete == false)
                        //            {
                        //                context.HttpContext.Response.Redirect("~/Home/NotAuthentizace");
                        //                return;
                        //            }
                        //            break;
                        //        default:
                        //            context.HttpContext.Response.Redirect("~/Home/NotAuthentizace");
                        //            return;
                        //    }

                        //}
                        #endregion
                    }

                }
            }
            base.OnActionExecuting(context);
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            if (User != null)
            {
                using (var db_ = new ToolsApp.EntityFramework.crmcustomscontext())
                {
                    var controllerName = filterContext.RouteData.Values["controller"].ToString();
                    var actionName = filterContext.RouteData.Values["action"].ToString();
                    string action = $" {controllerName}/{actionName}";
                    string ipAddress = filterContext.HttpContext.Request.UserHostAddress;
                    string httpMethod = filterContext.HttpContext.Request.HttpMethod;
                    var history = new LogHistory
                    {
                        idUser = User.UserId,
                        moTa = "Lỗi hệ thống",
                        moTaChiTiet = "Người dùng thực hiện action" + (httpMethod == "POST" ? " thêm dử liệu" : httpMethod == "PUT" ? " cập nhật dữ liệu" : httpMethod == "DELETE" ? " xóa dữ liệu" : " xem hoặc tìm kiếm dũ liệu") + " gặp lỗi"+ filterContext.Exception.Message,
                        ipUserHostAddress = ipAddress,
                        ngayTao = DateTime.UtcNow.AddHours(7),
                        nguoiTao = User.UserId,
                        ngayCapNhat = DateTime.UtcNow.AddHours(7),
                        nguoiCapNhat = User.UserId,
                        ngayXoa = DateTime.UtcNow.AddHours(7),
                        nguoiXoa = User.UserId,
                        xacNhanXoa = false,
                        hieuLuc = false,
                    };
                    db_.LogHistorys.Add(history);
                    db_.SaveChanges();
                }
            }
        }

    }
}