using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using ToolsApp.Authentication;
using ToolsApp.EntityFramework;
using ToolsApp.Models;

namespace ToolsApp.Controllers
{
    [Authorize]
    public class CartManagermentController : BaseController
    {
        // GET: CartManagerment
        crmcustomscontext db_ = new crmcustomscontext();
        public ActionResult Index()
        {
            var nhomBaiViets = db_.NhomBaiViets.Where(a => a.trangThai == true).ToList();
            var ketCaus = db_.KetCaus.Where(a => a.trangThai == true).ToList();
            ViewBag.nhomBaiViets = nhomBaiViets;
            ViewBag.ketCaus = ketCaus;
            return View();
        }
        public ActionResult GetList(int? idNhomBaiViet)
        {
            var ketCaus = db_.KetCaus.Where(a => a.trangThai == true).ToList();
            ViewBag.ketCaus = ketCaus;
            var query = db_.BaiViets.Include(bv => bv.KetCau).AsQueryable();
            if (idNhomBaiViet != null)
            {
                query = query.Where(a => a.idNhomBaiViet == idNhomBaiViet);
                TempData["idNhomBaiViet"] = idNhomBaiViet;
            }
            var data = query.ToList();
            ViewBag.danhSachBaiViet = data;
            return PartialView();
        }
        public ActionResult Insert()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult hinhAnh_View()
        {
            return PartialView();
        }
        public ActionResult ThongTinBaiViet_View()
        {
            var baiViet = Session["CurrentBaiViet"] as BaiViet;
            var nhomBaiViets = db_.NhomBaiViets.Where(a => a.trangThai == true).ToList();
            var ketCaus = db_.KetCaus.Where(a => a.trangThai == true).ToList();
            ViewBag.ketCaus = ketCaus;
            ViewBag.nhomBaiViets = nhomBaiViets;

            if (baiViet == null)
            {
                baiViet = new BaiViet();
                var nhomBaiViet = TempData["idNhomBaiViet"] as int?;
                var firstIdNhomBaiViet = nhomBaiViets.FirstOrDefault();
                if (nhomBaiViet == null)
                {
                    baiViet.idNhomBaiViet = firstIdNhomBaiViet.id;
                }
                else
                {
                    baiViet.idNhomBaiViet = nhomBaiViet;
                }
                baiViet.ngayTao = DateTime.Now;
                baiViet.nguoiTao = User.UserId;
                baiViet.tenBaiViet = "Bài viết chưa đặt tên";
                baiViet.viTri = true;
                baiViet.dienTichDat = 0;
                baiViet.dienTichXayDung = 0;
                baiViet.dienTichSuDung = 0;
                baiViet.chieuDai = 0;
                baiViet.chieuNgang = 0;
                baiViet.idUser = User.UserId;
                var firstKetCau = ketCaus.FirstOrDefault();
                if (firstKetCau != null)
                {
                    baiViet.idKetCau = firstKetCau.id;
                }
                baiViet.huong = "Bắc";
                //baiViet.soPhong = "1-2";
                db_.BaiViets.Add(baiViet);
                db_.SaveChanges();
                Session["CurrentBaiViet"] = baiViet;
            }

            return PartialView(baiViet);
        }
        [HttpPost]
        public JsonResult QuickUpdateBaiViet(FormCollection form, int id)
        {
            try
            {
                var baiViet = db_.BaiViets.FirstOrDefault(a => a.id == id);
                if (baiViet != null)
                {
                    baiViet.ngayCapNhat = DateTime.Now;
                    baiViet.nguoiCapNhat = User.UserId;

                    var keys = form.AllKeys.Where(a => a != "id").ToList();
                    foreach (var key in keys)
                    {
                    var value = form[key];
                    var propertyInfo = typeof(BaiViet).GetProperty(key);
                    if (propertyInfo != null && propertyInfo.CanWrite)
                    {
                        try
                        {
                            if (Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null)
                            {
                                var underlyingType = Nullable.GetUnderlyingType(propertyInfo.PropertyType);
                                var convertedValue = (value == null) ? null : Convert.ChangeType(value, underlyingType);
                                propertyInfo.SetValue(baiViet, convertedValue);
                            }
                            else
                            {
                                var convertedValue = Convert.ChangeType(value, propertyInfo.PropertyType);
                                propertyInfo.SetValue(baiViet, convertedValue);
                            }
                        }
                        catch (Exception ex)
                        {
                            return Json(new { success = false, message = "Lỗi không thể chuyển đổi dữ liệu" });
                        }
                    }
                    }
                    db_.SaveChanges();
                    Session["CurrentBaiViet"] = baiViet;
                    return Json(new { success = true });
                } 
                else
                {
                    return Json(new { success = false, message = "Không tìm thấy id bài viết" });
                }
            }
            catch (Exception )
            {

                return Json(new { success = false, message = "Có lỗi trong quá trình lưu dữ liệu" });
            }
           
        }
        [HttpPost]
        public JsonResult SaveNote(int id, string note)
        {
            try
            {
                var baiViet = db_.BaiViets.FirstOrDefault(a => a.id == id);
                baiViet.note = note;
                baiViet.trangThaiGoiDien = true;
                baiViet.thoiGianGoiDien = DateTime.Now;
                db_.SaveChanges();
                return Json(new { success = true, message = "Lưu thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Không tìm thấy id bài viết" });
            }
        }
        public ActionResult ClearSession()
        {
            Session.Remove("CurrentBaiViet");
            return Json(new { success = true });
        }
    }
}