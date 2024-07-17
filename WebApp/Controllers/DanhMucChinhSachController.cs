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
    public class DanhMucChinhSachController : BaseController
    {
        // GET: CartManagerment
        crmcustomscontext db_ = new crmcustomscontext();
        public ActionResult Index()
        {
          
            return View();
        }
        public async Task<ActionResult> GetList(string tieuDeSearch, string ngayBatDauSearch, string ngayKetThucSearch)
        {
            DateTime DateNow = DateTime.UtcNow.AddHours(7);
            DateTime dateStart = DateTime.ParseExact(ngayBatDauSearch, "dd/MM/yyyy", null);
            DateTime dateEnd = DateTime.ParseExact(ngayKetThucSearch, "dd/MM/yyyy", null);
            dateStart = dateStart.AddHours(0).AddMinutes(0).AddSeconds(0);
            dateEnd = dateEnd.AddHours(23).AddMinutes(59).AddSeconds(59);
            var users = db_.Users.AsNoTracking();
            var ChinhSaches = db_.ChinhSaches.AsNoTracking();
            var data =await (from a in ChinhSaches
                       join b in users on a.nguoiTao equals b.Id
                       where (a.ngayTao >= dateStart) && (a.ngayTao <= dateEnd)
                       where string.IsNullOrEmpty(tieuDeSearch)== true || a.tieuDe.ToUpper().Contains(tieuDeSearch.ToUpper())
                       where a.xacNhanXoa ==false
                       select new ChinhSachModels
                       {
                           id = a.id,
                           tieuDe = a.tieuDe,
                           tenNguoiTao = b.hoVaTen,
                           ngayCapNhat = a.ngayCapNhat,
                           ngayTao = a.ngayTao,
                           ngayXoa= a.ngayXoa,
                           nguoiCapNhat = a.nguoiCapNhat,
                           nguoiTao= a.nguoiTao,
                           nguoiXoa= a.nguoiXoa,
                           noiDung=a.noiDung,
                           trangThai =a.trangThai,
                           xacNhanXoa=a.xacNhanXoa,
                       }).ToListAsync();
            ViewBag.data = data;
            return PartialView();
        }
        public ActionResult Insert()
        {

            return PartialView();
        }
        public ActionResult Edit(int id)
        {
            var data = db_.ChinhSaches.Where(a => a.id == id).FirstOrDefault();
            ViewBag.data = data;
            return PartialView();
        }
        public ActionResult View(int id)
        {
            var data = db_.ChinhSaches.Where(a => a.id == id).FirstOrDefault();
            ViewBag.data = data.noiDung;
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult _InsertFun(string tieuDe,string noiDung)
        {

            try
            {
                if (tieuDe == "" || tieuDe == null)
                {
                    return Json(new { status = -1, title = "", text = "Vui lòng nhập tiêu đề", obj = "" }, JsonRequestBehavior.AllowGet);

                }
                var model_copy = new ChinhSach();
                model_copy.tieuDe = tieuDe;
                model_copy.noiDung = noiDung;
                model_copy.trangThai = true;
                model_copy.ngayTao = DateTime.Now;
                model_copy.nguoiTao = User.UserId;
                model_copy.ngayCapNhat = DateTime.Now;
                model_copy.nguoiCapNhat = User.UserId;
                model_copy.ngayXoa = DateTime.Now;
                model_copy.nguoiXoa = User.UserId;
                model_copy.xacNhanXoa = false;
                db_.ChinhSaches.Add(model_copy);
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
        public JsonResult _EditFun(int id, string tieuDe, string noiDung)
        {

            try
            {

                if (tieuDe == "" || tieuDe == null)
                {
                    return Json(new { status = -1, title = "", text = "Vui lòng nhập tiêu đề", obj = "" }, JsonRequestBehavior.AllowGet);
                }
                var data = db_.ChinhSaches.Where(a => a.id == id && a.xacNhanXoa == false ).FirstOrDefault();
                if (data != null)
                {
                    data.tieuDe = tieuDe;
                    data.noiDung = noiDung;
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
                var data = db_.ChinhSaches.Where(a => a.id == id).FirstOrDefault();
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