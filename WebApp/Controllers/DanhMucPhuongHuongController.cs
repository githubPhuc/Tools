﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
    public class DanhMucPhuongHuongController : BaseController
    {
        static int Direction = 6;
        crmcustomscontext db_ = new crmcustomscontext();
        public ActionResult Index()
        {
          
            return View();
        }
        public async Task<ActionResult> GetList(string tenPhuongHuongSearch)
        {
            var data = db_.Configs.Where(a=>a.parentId == Direction && a.xacNhanXoa ==false &&(string.IsNullOrEmpty(tenPhuongHuongSearch)==true || a.MoTa.ToUpper().Contains(tenPhuongHuongSearch.ToUpper()))).ToList();
            ViewBag.data = data;
            return PartialView();
        }
        public ActionResult Insert()
        {
           
            return PartialView();
        }
        public ActionResult Edit(int id)
        {
            var data = db_.Configs.Where(a=>a.Id==id && a.parentId == Direction).FirstOrDefault();
            ViewBag.data = data;
            return PartialView();
        }
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult _InsertFun(string tenPhuongHuong)
        {
         
            try
            {

                if (tenPhuongHuong == "" || tenPhuongHuong == null)
                {
                    return Json(new { status = -1, title = "", text = "Vui lòng nhập tên phương hướng", obj = "" }, JsonRequestBehavior.AllowGet);

                }
                var model_copy = new Config();
                model_copy.parentId = Direction;
                model_copy.code = "Direction";
                model_copy.MoTa = tenPhuongHuong;
                model_copy.moTaChiTiet = tenPhuongHuong;
                model_copy.ngayTao = DateTime.Now;
                model_copy.nguoiTao = User.UserId;
                model_copy.ngayCapNhat = DateTime.Now;
                model_copy.nguoiCapNhat = User.UserId;
                model_copy.ngayXoa = DateTime.Now;
                model_copy.nguoiXoa = User.UserId;
                model_copy.xacNhanXoa = false;
                model_copy.hieuLuc = true;

                db_.Configs.Add(model_copy);
                db_.SaveChanges();

                return Json(new { status = 1, title = "", text = "Tạo thành công", obj = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = -1, title = "", text = ex.Message, obj = "" }, JsonRequestBehavior.AllowGet);
            }
           
        }
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult _EditFun(int id,string tenPhuongHuong)
        {
         
            try
            {

                if (tenPhuongHuong == "" || tenPhuongHuong == null)
                {
                    return Json(new { status = -1, title = "", text = "Vui lòng nhập tên phương hướng", obj = "" }, JsonRequestBehavior.AllowGet);

                }
                var data = db_.Configs.Where(a=>a.Id==id && a.xacNhanXoa == false && a.parentId ==Direction).FirstOrDefault();
                if(data != null)
                {
                    data.MoTa = tenPhuongHuong;
                    data.hieuLuc = true;
                    data.ngayCapNhat = DateTime.Now;
                    data.nguoiCapNhat = User.UserId;

                    db_.Entry(data).State = EntityState.Modified;
                    db_.SaveChanges();
                    return Json(new { status = 1, title = "", text = "Cập nhật thành công", obj = "" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { status = 1, title = "", text = "Không tìm thấy data hoặc data đã được xóa! vui lòng liên hệ bộ phận kỹ thuật@@", obj = "" }, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ex)
            {
                return Json(new { status = -1, title = "", text = ex.Message, obj = "" }, JsonRequestBehavior.AllowGet);
            }
           
        }
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult _DeleteFun(int id)
        {
         
            try
            {
                var data = db_.Configs.Where(a => a.Id == id  && a.parentId == Direction).FirstOrDefault();
                if (data != null)
                {
                    data.ngayXoa = DateTime.Now;
                    data.nguoiXoa = User.UserId;
                    data.xacNhanXoa = true;
                    db_.Entry(data).State = EntityState.Modified;
                    db_.SaveChanges();
                    return Json(new { status = 1, title = "", text = "Xóa thành công", obj = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = 1, title = "", text = "Không tìm thấy dữ liệu hoặc dữ liệu đã được xóa", obj = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = -1, title = "", text = ex.Message, obj = "" }, JsonRequestBehavior.AllowGet);
            }
           
        }

    }
}