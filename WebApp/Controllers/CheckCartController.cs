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
                .Where(a => a.trangThai == true)
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
            var tyGia = db_.Configs.Where(a => a.parentId == 21 && a.xacNhanXoa == false && a.hieuLuc == true).ToList();
            var nhomBaiViet = db_.NhomBaiViets.Where(a => a.trangThai == true).ToList();

            ViewBag.tyGia = tyGia;
            ViewBag.nhomBaiViet = nhomBaiViet;
            var recordsTotal = query.Count();
            var totalPages = (int)Math.Ceiling((double)recordsTotal / itemPerPage);

            var data = query.Skip(itemPerPage * (page - 1)).Take(itemPerPage).ToList();

            ViewBag.danhSachBaiViet = data;
            ViewBag.totalPages = totalPages;
            ViewBag.page = page;

            return PartialView();
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
        public JsonResult Share(int Id)
        {
            var baiViet = db_.BaiViets.FirstOrDefault(a => a.id == Id);
            if (baiViet == null)
            {
                return Json(new { success = false, message = "Bài viết không tồn tại." });
            }

            var loaiBaiDang = db_.NhomBaiViets
                .Where(b => b.id == baiViet.idNhomBaiViet)
                .Select(b => b.tenNhom)
                .FirstOrDefault();

            var userId = User.UserId;
            var chuKy = db_.Users
                .Where(a => a.Id == userId)
                .Select(a => a.chuKyUser)
                .FirstOrDefault();
            LogHistory log = new LogHistory
            {
                idUser = User.UserId,
                nguoiTao = User.UserId,
                moTa = "Đã chọn chia sẽ bài ",
                moTaChiTiet = $"Người dùng đã thực hiện hành động chia sẽ bài viết ${baiViet.diaChiTaiSan} vào lúc",
                ngayTao = DateTime.Now
            };
            db_.LogHistorys.Add(log);
            db_.SaveChanges();
            var viTri = (bool)baiViet.viTri ? "Mặt tiền" : "Hẻm";
            var donVi = "tỷ";
            if (loaiBaiDang == "Nhà cho thuê" || loaiBaiDang == "Nhà cho thuê")
            {
                donVi = "triệu";
            }
            return Json(new
            {
                success = true,
                data = new
                {
                    nhomBaiViet = loaiBaiDang,
                    donVi = donVi,
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
        public ActionResult ShowLog(int id)
        {
            DateTime currentDate = DateTime.Now.Date;
            DateTime threeDaysAgo = currentDate.AddDays(-2);
            var logData = db_.LogHistorys
        .Join(
            db_.Users,
            log => log.idUser,
            user => user.Id,
            (log, user) => new
            {
                Log = log,
                User = user
            })
        .Where(a => a.Log.IdBaiViet == id &&
                    DbFunctions.TruncateTime(a.Log.ngayTao) >= threeDaysAgo &&
                    DbFunctions.TruncateTime(a.Log.ngayTao) <= currentDate)
            .OrderByDescending(a => a.Log.ngayTao)
            .ThenByDescending(a => a.Log.Id)
            .Select(a => new LogHistoryViewModel
            {
                Id = a.Log.Id,
                idUser = a.Log.idUser,
                moTa = a.Log.moTa,
                hoVaTen = a.User.hoVaTen,
                tenTaiKhoan = a.User.tenTaiKhoan,
                moTaChiTiet = a.Log.moTaChiTiet,
                ngayTao = a.Log.ngayTao,
                nguoiTao = a.Log.nguoiTao,
                IdBaiViet = a.Log.IdBaiViet,
                ipUserHostAddress = a.Log.ipUserHostAddress
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
                moTaChiTiet = $"Người dùng ${User.tenTaiKhoan} click vào lấy số nhà vào lúc {DateTime.Now}",
                hieuLuc = true,
                IdBaiViet = id
            };
            db_.LogHistorys.Add(log);
            db_.SaveChanges();
            return Json(new { success = true });
        }
    }
}