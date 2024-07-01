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
    public class DanhMucKetCauController : BaseController
    {
        // GET: CartManagerment
        crmcustomscontext db_ = new crmcustomscontext();
        public ActionResult Index()
        {
          
            return View();
        }
        public async Task<ActionResult> GetList(string tenKetCauSearch)
        {
            var _KetCaus = db_.KetCaus.AsNoTracking();
            var _Users = db_.Users.AsNoTracking();
            var data = await(from a in _KetCaus
                        join b in _Users on a.nguoiTao equals b.Id
                        join c in _Users on a.nguoiCapNhat equals c.Id
                        where a.xacNhanXoa == false
                        where a.tenKetCau.ToLower().Contains(tenKetCauSearch.ToLower()) || string.IsNullOrEmpty(tenKetCauSearch)
                        select new KetCauModels
                        {
                            id = a.id,
                            tenKetCau = a.tenKetCau,
                            tenNguoiTao = b.hoVaTen,
                            tenNguoiCapNhat = c.hoVaTen,
                            trangThai = a.trangThai,
                            ngayTao = a.ngayTao,
                            nguoiTao = a.nguoiTao,
                            ngayCapNhat = a.ngayCapNhat,
                            nguoiCapNhat = a.nguoiCapNhat,
                            ngayXoa = a.ngayXoa,
                            nguoiXoa = a.nguoiXoa,
                            xacNhanXoa = a.xacNhanXoa,
                        }).ToListAsync();
            ViewBag.KetCaus = data;
            return PartialView();
        }
        public ActionResult Insert()
        {
           
            return PartialView();
        }
        public ActionResult Edit(int id)
        {
            var data = db_.KetCaus.Where(a=>a.id==id).FirstOrDefault();
            ViewBag.data = data;
            return PartialView();
        }
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult _InsertFun(string tenKetCau)
        {
         
            try
            {

                if (tenKetCau == "" || tenKetCau == null)
                {
                    return Json(new { status = -1, title = "", text = "Vui lòng nhập tên kết cấu", obj = "" }, JsonRequestBehavior.AllowGet);

                }
                var model_copy = new KetCau();
                model_copy.tenKetCau = tenKetCau;
                model_copy.trangThai = true;
                model_copy.ngayTao = DateTime.Now;
                model_copy.nguoiTao = User.UserId;
                model_copy.ngayCapNhat = DateTime.Now;
                model_copy.nguoiCapNhat = User.UserId;
                model_copy.ngayXoa = DateTime.Now;
                model_copy.nguoiXoa = User.UserId;
                model_copy.xacNhanXoa = false;

                db_.KetCaus.Add(model_copy);
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
        public JsonResult _EditFun(int id,string tenKetCau)
        {
         
            try
            {

                if (tenKetCau == "" || tenKetCau == null)
                {
                    return Json(new { status = -1, title = "", text = "Vui lòng nhập tên kết cấu", obj = "" }, JsonRequestBehavior.AllowGet);

                }
                var data = db_.KetCaus.Find(id);
                if(data != null)
                {
                    data.tenKetCau = tenKetCau;
                    data.trangThai = true;
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
                var data = db_.KetCaus.Find(id);
                if(data != null)
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