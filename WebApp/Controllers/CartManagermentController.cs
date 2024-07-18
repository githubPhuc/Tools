﻿using System;
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

        public ActionResult GetList(SearchViewModel searchModel, int? idNhomBaiViet, int page = 1, int itemPerPage = 10)
        {
            var ketCaus = db_.KetCaus.Where(a => a.trangThai == true).ToList();
            var getTinhTrang = db_.Configs.Where(a => a.parentId == 16 && a.xacNhanXoa == false).ToList();
            ViewBag.tinhTrang = getTinhTrang;

            ViewBag.ketCaus = ketCaus;
            
            var query = db_.BaiViets
                .Include(bv => bv.NhomBaiViet)
                .Include(bv => bv.HinhAnhBaiViets)
                .Include(bv => bv.User)
                .Include(bv => bv.KetCau).AsQueryable();

            if (idNhomBaiViet != null)
            {
                query = query.Where(a => a.idNhomBaiViet == idNhomBaiViet);
                TempData["idNhomBaiViet"] = idNhomBaiViet;
                ViewData["id"] = idNhomBaiViet;
            }
            if (!string.IsNullOrEmpty(searchModel.DiaChiTaiSanSearch))
            {  
                var searchValue = searchModel.DiaChiTaiSanSearch.ToLower();
                query = query.Where(a => a.diaChiTaiSan.ToLower().Contains(searchValue));
 
            }
            if (searchModel.NhomBaiVietSearch.HasValue)
            {
                query = query.Where(a => a.idNhomBaiViet == searchModel.NhomBaiVietSearch.Value);
            }
            if (!string.IsNullOrWhiteSpace(searchModel.IdThanhPho) ||
                  !string.IsNullOrWhiteSpace(searchModel.IdQuanHuyen) ||
                  !string.IsNullOrWhiteSpace(searchModel.IdPhuongXa))
            {
                query = query.Where(a =>
                    (string.IsNullOrEmpty(searchModel.IdThanhPho) || a.diaChiDayDu.ToLower().Contains(searchModel.IdThanhPho.ToLower())) &&
                    (string.IsNullOrEmpty(searchModel.IdQuanHuyen) || a.diaChiDayDu.ToLower().Contains(searchModel.IdQuanHuyen.ToLower())) &&
                    (string.IsNullOrEmpty(searchModel.IdPhuongXa) || a.diaChiDayDu.ToLower().Contains(searchModel.IdPhuongXa.ToLower())));
            }
            if (searchModel.ViTriSearch.HasValue)
            {
                query = query.Where(a => a.viTri == searchModel.ViTriSearch.Value);
            }
            if (searchModel.DienTichTu.HasValue)
            {
                query = query.Where(a => a.dienTichDat >= searchModel.DienTichTu.Value);
            }
            if (searchModel.DienTichDen.HasValue)
            {
                query = query.Where(a => a.dienTichDat <= searchModel.DienTichDen.Value);
            }
            if (searchModel.GiaBanTu.HasValue)
            {
                query = query.Where(a => a.giaBan >= searchModel.GiaBanTu.Value);
            }
            if (searchModel.GiaBanDen.HasValue)
            {
                query = query.Where(a => a.giaBan <= searchModel.GiaBanDen.Value);
            }
            if (searchModel.ChieuDaiTu.HasValue)
            {
                query = query.Where(a => a.chieuDai >= searchModel.ChieuDaiTu.Value);
            }
            if (searchModel.ChieuDaiDen.HasValue)
            {
                query = query.Where(a => a.chieuDai <= searchModel.ChieuDaiDen.Value);
            }
            if (searchModel.ChieuNgangTu.HasValue)
            {
                query = query.Where(a => a.chieuNgang >= searchModel.ChieuNgangTu.Value);
            }
            if (searchModel.ChieuNgangDen.HasValue)
            {
                query = query.Where(a => a.chieuNgang <= searchModel.ChieuNgangDen.Value);
            }
            if (searchModel.KetCauSearch.HasValue)
            {
                query = query.Where(a => a.idKetCau == searchModel.KetCauSearch);
            }
            if (!string.IsNullOrEmpty(searchModel.SoPhongSearch))
            {
                var soPhongRange = searchModel.SoPhongSearch.Split('-');
                if (soPhongRange.Length == 2 && int.TryParse(soPhongRange[0], out int soPhongTu) && int.TryParse(soPhongRange[1], out int soPhongDen))
                {
                    query = query.Where(a => a.soPhong >= soPhongTu && a.soPhong <= soPhongDen);
                }
                else if (searchModel.SoPhongSearch == ">10")  
                {
                    query = query.Where(a => a.soPhong >= 10);  
                }
            }
            if (!string.IsNullOrEmpty(searchModel.HuongSearch))
            {
                query = query.Where(a => a.huong.ToLower().Contains(searchModel.HuongSearch.ToLower()));
            }

            query = query.AsNoTracking().OrderByDescending(a => a.id);

            var recordsTotal = query.Count();
            var totalPages = (int)Math.Ceiling((double)recordsTotal / itemPerPage);

            var data = query.Skip(itemPerPage * (page - 1)).Take(itemPerPage).ToList();

            ViewBag.danhSachBaiViet = data;
            ViewBag.totalPages = totalPages;
            ViewBag.page = page;

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
            var IdTinhTrang =db_.Configs.FirstOrDefault(a => a.parentId == 16 && a.xacNhanXoa == false).Id;
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
                    baiViet.tenBaiViet = "";
                    baiViet.viTri = true;
                    baiViet.trangThai = true;
                    baiViet.dienTichDat = 0;
                    baiViet.dienTichXayDung = 0;
                    baiViet.dienTichSuDung = 0;
                    baiViet.chieuDai = 0;
                    baiViet.chieuNgang = 0;
                    baiViet.giaBan = 0;
                    baiViet.tinhTrang = IdTinhTrang;
                    baiViet.idUser = User.UserId;
                    var firstKetCau = ketCaus.FirstOrDefault();
                    if (firstKetCau != null)
                    {
                        baiViet.idKetCau = firstKetCau.id;
                    }
                    baiViet.huong = "Bắc";
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
            LogHistory log = new LogHistory
            {
                IdBaiViet = id,
                idUser = User.UserId,
                nguoiTao = User.UserId,
                moTa = "Click xem hình ảnh ",
                ngayTao = DateTime.Now
            };
            db_.LogHistorys.Add(log);
            db_.SaveChanges();
            return Json(new { success = true, response = urlPaths });
        }
        [HttpPost]
        public JsonResult Share(int Id)
        {
            var baiViet = db_.BaiViets.FirstOrDefault(a => a.id == Id);
            if (baiViet == null)
            {
                return Json(new { success = false, message = "Bài viết không tồn tại." });
            }

              var loaiBaiDang = db_.NhomBaiViets
             .Where(b => b.id == baiViet.idNhomBaiViet)
             .Select(b => new { b.idTyGia, b.tenNhom })
             .FirstOrDefault();
            var tyGia = db_.Configs.FirstOrDefault(a => a.parentId == 21 && a.xacNhanXoa == false && a.hieuLuc == true && a.Id == loaiBaiDang.idTyGia).MoTa;
            var userId = User.UserId;
            var chuKy = db_.Users
                .Where(a => a.Id == userId)
                .Select(a => a.chuKyUser)
                .FirstOrDefault();
            LogHistory log = new LogHistory
            {
                IdBaiViet = Id,
                idUser = User.UserId,
                nguoiTao = User.UserId,
                moTa = "Đã chọn chia sẽ bài ",
                moTaChiTiet = $"Người dùng {User.tenTaiKhoan} đã thực hiện hành động chia sẽ bài viết {baiViet.diaChiTaiSan} vào lúc",
                ngayTao = DateTime.Now
            };
            db_.LogHistorys.Add(log);
            db_.SaveChanges();
            var viTri = (bool)baiViet.viTri ? "Mặt tiền" : "Hẻm";
            return Json(new
            {
                success = true,
                data = new
                {
                    nhomBaiViet = loaiBaiDang.tenNhom,
                    donVi = tyGia,
                    gia = baiViet.giaBan,
                    ketCau = baiViet.motaKetCau,
                    dienTich = new { chieuNgang = baiViet.chieuNgang, chieuDai = baiViet.chieuDai },
                    viTri = viTri,
                    diaChi = baiViet.diaChiTaiSan,
                    chuKy = chuKy
                }
            });
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
                        var uploadsPath = Server.MapPath("~/Uploads/Realestate");
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(uploadsPath, fileName);
                        if (!Directory.Exists(uploadsPath))
                        {
                            Directory.CreateDirectory(uploadsPath);
                        }

                        file.SaveAs(filePath);
                        var relativePath = $"~/Uploads/Realestate/{fileName}";

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
        [HttpPost]
        public JsonResult GetUpdatedValues(int id)
        {
            var baiViet = db_.BaiViets.FirstOrDefault(a => a.id == id);
            return Json(new
            {
                dienTichDat = baiViet.dienTichDat,
                dienTichXayDung = baiViet.dienTichXayDung,
                dienTichSuDung = baiViet.dienTichSuDung
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getSoNha(int id)
        {
            string userIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(userIpAddress))
            {
                userIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            var log = new LogHistory
            {
                idUser = User.UserId,
                ipUserHostAddress = userIpAddress,
                ngayTao = DateTime.Now,
                moTa = "Click lấy số nhà",
                moTaChiTiet = $"Người dùng <strong style='background-color: blue; color: white;'>{User.tenTaiKhoan}</strong> vào lấy số nhà vào lúc {DateTime.Now}",
                hieuLuc = true,
                IdBaiViet = id
            };
            db_.LogHistorys.Add(log);
            db_.SaveChanges();
            return Json(new { success = true });
        }
        public ActionResult ShowLog(int id, int? idUser)
        {
            DateTime currentDate = DateTime.Now.Date;
            DateTime threeDaysAgo = currentDate.AddDays(-2);
            var logData = db_.LogHistorys
                .Where(a => a.IdBaiViet == id && DbFunctions.TruncateTime(a.ngayTao) >= threeDaysAgo && DbFunctions.TruncateTime(a.ngayTao) <= currentDate 
                && a.idUser == idUser)
                .OrderByDescending(a => a.ngayTao)
                .ThenByDescending(a => a.Id)
                 .Select(a => new LogHistoryViewModel
                 {
                     Id = a.Id,
                     idUser = a.idUser,
                     moTa = a.moTa,
                     hoVaTen = User.hoVaTen,
                     tenTaiKhoan = User.tenTaiKhoan,
                     moTaChiTiet = a.moTaChiTiet,
                     ngayTao = a.ngayTao,
                     nguoiTao = a.nguoiTao,
                     IdBaiViet = a.IdBaiViet,
                     ipUserHostAddress = a.ipUserHostAddress
                 })
                .ToList();

            if (logData.Count == 0)
            {
                return Json(new { success = false, message = "Bài viết chưa ghi lại bất kỳ log nào:" }, JsonRequestBehavior.AllowGet);
            }

            ViewData["id"] = id;
            ViewBag.logData = logData;
            db_.SaveChanges();

            return PartialView();
        }
    }
}