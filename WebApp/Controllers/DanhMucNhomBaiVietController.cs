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
    public class DanhMucNhomBaiVietController : BaseController
    {
        // GET: CartManagerment
        crmcustomscontext db_ = new crmcustomscontext();
        private readonly AppGlobal appGlobal= new AppGlobal();
        public ActionResult Index()
        {

            return View();
        }
        public async Task<ActionResult> GetList(string tenNhomSearch)
        {
            var users = db_.Users.AsNoTracking();
            var NhomBaiViets = db_.NhomBaiViets.AsNoTracking();
            var TyGia = db_.Configs.Where(a=>a.parentId == appGlobal.IdDanhMucTyGia).AsNoTracking();
            var data = await (from a in NhomBaiViets
                              join b in users on a.nguoiTao equals b.Id
                              join c in TyGia on a.idTyGia equals c.Id
                              where string.IsNullOrEmpty(tenNhomSearch) == true || a.tenNhom.ToUpper().Contains(tenNhomSearch.ToUpper())
                              where a.xacNhanXoa == false
                              select new NhomBaiVietModels
                              {
                                  id = a.id,
                                  tenNhom = a.tenNhom,
                                  MoTa = a.MoTa,
                                  idTyGia = a.idTyGia,
                                  tenTyGia = c.MoTa,
                                  tenNguoiTao = b.hoVaTen,
                                  ngayCapNhat = a.ngayCapNhat,
                                  ngayTao = a.ngayTao,
                                  ngayXoa = a.ngayXoa,
                                  nguoiCapNhat = a.nguoiCapNhat,
                                  nguoiTao = a.nguoiTao,
                                  nguoiXoa = a.nguoiXoa,
                                  trangThai = a.trangThai,
                                  xacNhanXoa = a.xacNhanXoa,
                              }).ToListAsync();
            ViewBag.data = data;
            return PartialView();
        }
        public ActionResult Insert()
        {
            var dataTyGia = db_.Configs.Where(a => a.parentId == appGlobal.IdDanhMucTyGia && a.xacNhanXoa == false).ToList();
            ViewBag.dataTyGia = dataTyGia;
            return PartialView();
        }
        public ActionResult Edit(int id)
        {
            var dataNhomBaiViet = db_.NhomBaiViets.FirstOrDefault(a => a.id == id);
            var dataTyGia = db_.Configs.Where(a => a.parentId == appGlobal.IdDanhMucTyGia && a.xacNhanXoa == false).ToList();
            ViewBag.dataNhomBaiViet = dataNhomBaiViet;
            ViewBag.data = dataTyGia;
            return PartialView();
        }
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult _InsertFun(string tenNhom, string MoTa,int idTyGia)
        {

            try
            {
                if (tenNhom == "" || tenNhom == null)
                {
                    return Json(new { status = -1, title = "", text = "Vui lòng nhập tên nhóm", obj = "" }, JsonRequestBehavior.AllowGet);

                }
                if (idTyGia==0)
                {
                    return Json(new { status = -1, title = "", text = "Vui lòng chọn tỷ giá", obj = "" }, JsonRequestBehavior.AllowGet);

                }
                var model_copy = new NhomBaiViet();
                model_copy.tenNhom = tenNhom;
                model_copy.MoTa = MoTa;
                model_copy.idTyGia = idTyGia;
                model_copy.trangThai = true;
                model_copy.ngayTao = DateTime.Now;
                model_copy.nguoiTao = User.UserId;
                model_copy.ngayCapNhat = DateTime.Now;
                model_copy.nguoiCapNhat = User.UserId;
                model_copy.ngayXoa = DateTime.Now;
                model_copy.nguoiXoa = User.UserId;
                model_copy.xacNhanXoa = false;
                db_.NhomBaiViets.Add(model_copy);
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
        public JsonResult _EditFun(int id, string tenNhom, string MoTa, int idTyGia)
        {

            try
            {

                if (tenNhom == "" || tenNhom == null)
                {
                    return Json(new { status = -1, title = "", text = "Vui lòng nhập tên nhóm", obj = "" }, JsonRequestBehavior.AllowGet);

                }
                if (idTyGia == 0)
                {
                    return Json(new { status = -1, title = "", text = "Vui lòng chọn tỷ giá", obj = "" }, JsonRequestBehavior.AllowGet);
                }
                var data = db_.NhomBaiViets.Where(a => a.id == id && a.xacNhanXoa == false).FirstOrDefault();
                if (data != null)
                {
                    data.tenNhom = tenNhom;
                    data.MoTa = MoTa;
                    data.idTyGia = idTyGia;
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
                var data = db_.NhomBaiViets.Where(a => a.id == id).FirstOrDefault();
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