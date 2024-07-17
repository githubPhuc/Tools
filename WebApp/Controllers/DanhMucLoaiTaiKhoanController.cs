using System;
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
    public class DanhMucLoaiTaiKhoanController : BaseController
    {
        private readonly AppGlobal appGlobal = new AppGlobal();
        crmcustomscontext db_ = new crmcustomscontext();
        public ActionResult Index()
        {
          
            return View();
        }
        public async Task<ActionResult> GetList(string MoTaSearch)
        {
            var data = db_.Configs.Where(a=>a.parentId == appGlobal.IdDanhMucLoaiTaiKhoan && a.xacNhanXoa ==false &&(string.IsNullOrEmpty(MoTaSearch) ==true || a.MoTa.ToUpper().Contains(MoTaSearch.ToUpper()))).ToList();
            ViewBag.data = data;
            return PartialView();
        }
        public ActionResult Insert()
        {
           
            return PartialView();
        }
        public ActionResult Edit(int id)
        {
            var data = db_.Configs.Where(a=>a.Id==id && a.parentId == appGlobal.IdDanhMucLoaiTaiKhoan).FirstOrDefault();
            ViewBag.data = data;
            return PartialView();
        }
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult _InsertFun(string MoTa, string moTaChiTiet)
        {
         
            try
            {

                if (MoTa == "" || MoTa == null)
                {
                    return Json(new { status = -1, title = "", text = "Vui lòng nhập tên loại tài khoản", obj = "" }, JsonRequestBehavior.AllowGet);

                }
                var model_copy = new Config();
                model_copy.parentId = appGlobal.IdDanhMucLoaiTaiKhoan;
                model_copy.code = "AccountType";
                model_copy.MoTa = MoTa;
                model_copy.moTaChiTiet = moTaChiTiet;
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
        public JsonResult _EditFun(int id,string MoTa,string moTaChiTiet)
        {
         
            try
            {

                if (MoTa == "" || MoTa == null)
                {
                    return Json(new { status = -1, title = "", text = "Vui lòng nhập tên loại tài khoản", obj = "" }, JsonRequestBehavior.AllowGet);
                }
                var data = db_.Configs.Where(a=>a.Id==id && a.xacNhanXoa == false && a.parentId ==appGlobal.IdDanhMucLoaiTaiKhoan).FirstOrDefault();
                if(data != null)
                {
                    data.MoTa = MoTa;
                    data.moTaChiTiet = moTaChiTiet;
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
                var data = db_.Configs.Where(a => a.Id == id  && a.parentId == appGlobal.IdDanhMucLoaiTaiKhoan).FirstOrDefault();
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