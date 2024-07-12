using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using ToolsApp.Authentication;
using ToolsApp.EntityFramework;
using ToolsApp.Models;
using System.IO;

namespace ToolsApp.Controllers
{
    [Authorize]
    public class CheckCartController : BaseController
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
            var query = db_.BaiViets.Include(bv => bv.KetCau).Where(a => a.trangThai == true).AsQueryable();
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
        public ActionResult Edit(int id)
        {
            ViewData["id"] = id;
            return View();

        }

        public ActionResult ThongTinBaiViet_View(int? id)
        {
            BaiViet baiViet;
            var nhomBaiViets = db_.NhomBaiViets.Where(a => a.trangThai == true).ToList();
            var ketCaus = db_.KetCaus.Where(a => a.trangThai == true).ToList();
            ViewBag.ketCaus = ketCaus;
            ViewBag.nhomBaiViets = nhomBaiViets;

            if (id.HasValue)
            {
                baiViet = db_.BaiViets.FirstOrDefault(a => a.id == id);
                if (baiViet == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy bài viết" });
                }
            }
            else
            {
                baiViet = Session["CurrentBaiViet"] as BaiViet;


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
                    baiViet.trangThai = true;
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
                else
                {
                    if (baiViet.id == 0)
                    {
                        Session["CurrentBaiViet"] = null;
                        return RedirectToAction("ThongTinBaiViet_View");
                    }
                }
            }


            return PartialView(baiViet);
        }
        public ActionResult hinhAnh_View(int? id)
        {
            BaiViet baiViet;
            if (id.HasValue)
            {
                baiViet = db_.BaiViets.FirstOrDefault(a => a.id == id);
                if (baiViet == null)
                {

                    return RedirectToAction("GetList");
                }
            }
            else
            {
                baiViet = Session["CurrentBaiViet"] as BaiViet;
                if (baiViet == null)
                {
                    return RedirectToAction("ThongTinBaiViet_View");
                }

            }
            var urlPaths = db_.HinhAnhBaiViets
                 .Where(hbv => hbv.idBaiViet == baiViet.id && hbv.trangThai == true)
                 .Select(hbv => new HinhAnhBaiVietDto
                 {
                     Id = hbv.id,
                     UrlPath = hbv.HinhAnh.urlPath
                 })
                 .ToList();
            ViewBag.urlPaths = urlPaths;

            return PartialView(baiViet);
        }
        [HttpPost]
        public JsonResult ShowImages(int id)
        {
            var baiViet = db_.BaiViets.FirstOrDefault(a => a.id == id);
            var hinhAnhList = db_.HinhAnhBaiViets
               .Where(hbv => hbv.idBaiViet == baiViet.id && hbv.trangThai == true)
               .Select(hbv => new { hbv.id, hbv.HinhAnh.urlPath })
               .ToList();

            // Chuyển đổi đường dẫn tương đối thành đường dẫn tuyệt đối
            var urlPaths = hinhAnhList.Select(hbv => new HinhAnhBaiVietDto
            {
                Id = hbv.id,
                UrlPath = Url.Content(hbv.urlPath).Replace("~", Request.Url.Scheme + "://" + Request.Url.Authority)
            }).ToList();

            return Json(new { success = true, response = urlPaths });
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
            catch (Exception)
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
        [HttpPost]
        public ActionResult AddFilesToBaiViet(IEnumerable<HttpPostedFileBase> files, int id)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var uploadsPath = Server.MapPath("~/Uploads");
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(uploadsPath, fileName);
                        if (!Directory.Exists(uploadsPath))
                        {
                            Directory.CreateDirectory(uploadsPath);
                        }

                        file.SaveAs(filePath);
                        var relativePath = $"~/Uploads/{fileName}";

                        var hinhAnh = new HinhAnh
                        {
                            urlPath = relativePath,
                            ngayTao = DateTime.Now,
                            nguoiTao = User.UserId,
                            nguoiCapNhat = User.UserId,
                            ngayCapNhat = DateTime.Now,
                            trangThai = true
                        };

                        db_.HinhAnhs.Add(hinhAnh);
                        db_.SaveChanges();

                        var hinhAnhBaiViet = new HinhAnhBaiViet
                        {
                            idBaiViet = id,
                            idHinhAnh = hinhAnh.id,
                            trangThai = true,
                            ngayTao = DateTime.Now,
                            nguoiTao = User.UserId,
                            nguoiCapNhat = User.UserId,
                            ngayCapNhat = DateTime.Now,
                        };
                        db_.HinhAnhBaiViets.Add(hinhAnhBaiViet);
                        db_.SaveChanges();
                    }
                }
            }

            return Json(new { success = true });
        }
        [HttpPost]
        public JsonResult RemoveImage(int Id)
        {
            using (var transaction = db_.Database.BeginTransaction())
            {
                try
                {
                    var hinhAnhBaiViet = db_.HinhAnhBaiViets.FirstOrDefault(a => a.id == Id);
                    if (hinhAnhBaiViet != null)
                    {
                        var idHinhAnh = hinhAnhBaiViet.idHinhAnh;
                        var hinhAnh = db_.HinhAnhs.FirstOrDefault(c => c.id == idHinhAnh);
                        var path = hinhAnh.urlPath;
                        db_.HinhAnhBaiViets.Remove(hinhAnhBaiViet);
                        if (hinhAnh != null)
                        {
                            db_.HinhAnhs.Remove(hinhAnh);
                        }
                        db_.SaveChanges();
                        transaction.Commit();

                        try
                        {
                            string absolutePath = System.Web.Hosting.HostingEnvironment.MapPath(path);
                            if (System.IO.File.Exists(absolutePath))
                            {
                                System.IO.File.Delete(absolutePath);
                            }

                            return Json(new { success = true, message = "Hình ảnh đã được xóa thành công." }, JsonRequestBehavior.AllowGet);
                        }
                        catch (Exception ex)
                        {
                            return Json(new { success = false, message = "Xảy ra lỗi khi xóa file: " + ex.Message }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, message = "Không tìm thấy hình ảnh." }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }

        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var baiViet = db_.BaiViets.FirstOrDefault(a => a.id == id);
            if (baiViet != null)
            {
                baiViet.trangThai = false;
                baiViet.ngayXoa = DateTime.Now;
                baiViet.nguoiXoa = User.UserId;
                baiViet.xacNhanXoa = true;
            }
            db_.Entry(baiViet).State = EntityState.Modified;
            db_.SaveChanges();
            return Json(new { success = true, message = "Đã xóa bài viết thành công" }, JsonRequestBehavior.AllowGet);

        }
    }
}