using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToolsApp.Authentication;
using ToolsApp.EntityFramework;
using ToolsApp.Models;

namespace ToolsApp.Controllers
{
    public class InfomationUserController : BaseController
    {
        crmcustomscontext db_ = new crmcustomscontext();
        // GET: Infomation
        public ActionResult Index(int Id)
        {
            return View();
        }
        public ActionResult _Image_Avatar_View(int Id)
        {

            ViewData["Id"] = Id;
            return PartialView();
        }
        public ActionResult _Image_View(int Id)
        {
            var user = db_.Users.FirstOrDefault(a => a.Id == Id);
            ViewBag.user = user;
            ViewData["Id"] = Id;
            return PartialView();
        }

        public ActionResult ThongTinCoBan_View(int Id)
        {
            ViewData["Id"] = Id;
            var data = db_.Users.FirstOrDefault(a => a.Id == Id);
            ViewBag.data = data;
            return PartialView();
        }
        public ActionResult ThongTinLienHe_View(int Id)
        {
            ViewData["Id"] = Id;
            var data = db_.Users.FirstOrDefault(a => a.Id == Id);
            ViewBag.data = data;
            return PartialView();
        }
        public ActionResult ThôngTinCaNhan_View(int Id)
        {
            ViewData["Id"] = Id;
            var data = db_.Users.FirstOrDefault(a => a.Id == Id);
            ViewBag.data = data;
            return PartialView();
        }





        public ActionResult _ViewCCCD(int Id)
        {
            var user = db_.Users.FirstOrDefault(a => a.Id == Id);
            ViewBag.user = user;
            return PartialView();
        }

        public ActionResult UserProfile(int Id)
        {
            var user = db_.Users.FirstOrDefault(a => a.Id == Id);
            ViewBag.user = user;
            return View("User");
        }
        [HttpPost]
        public JsonResult saveChuKy(string content, int Id)
        {
            var user = db_.Users.FirstOrDefault(a => a.Id == Id);
            if (user == null) return Json(new { success = false });
            user.chuKyUser = content;
            user.ngayCapNhat = DateTime.Now;
            user.nguoiCapNhat = User.UserId;
            db_.Entry(user).State = EntityState.Modified;
            db_.SaveChanges();
            LogHistory log = new LogHistory
            {
                idUser = User.UserId,
                nguoiTao = User.UserId,
                moTa = "Đã thực hiện thay đổi chữ ký",
                moTaChiTiet = "Người dùng đã thực hiện thay đổi chữ ký vào lúc",
                ngayTao = DateTime.Now
            };
            db_.LogHistorys.Add(log);
            db_.SaveChanges();
            return Json(new { success = true });

        }
        public JsonResult _SaveImageAvatar(HttpPostedFileBase file, int id)
        {
            var user = db_.Users.FirstOrDefault(a => a.Id == id);
            if (file != null && file.ContentLength > 0)
            {
                string path = Server.MapPath("~/Uploads/Avatars");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = Path.GetFileName(file.FileName);
                string fullPath = Path.Combine(path, fileName);
                file.SaveAs(fullPath);
                var relativePath = $"~/Uploads/Avatars/{fileName}";
                user.anhDaiDien = relativePath;
                user.ngayCapNhat = DateTime.Now;
                db_.SaveChanges();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Vui lòng chọn một tệp hợp lệ." });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult _SaveImageCCCD(int Id)
        {
            try
            {
                var user = db_.Users.FirstOrDefault(a => a.Id == Id);
                if (user == null)
                {
                    return Json(new { success = false, message = "Người dùng không tồn tại." });
                }

                var uploadedFiles = new List<string>();

                foreach (string f in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[f];
                    if (file != null && file.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/CCCD");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        string fileName = Path.GetFileName(file.FileName);
                        string fullPath = Path.Combine(path, fileName);
                        file.SaveAs(fullPath);
                        var relativePath = $"~/Uploads/CCCD/{fileName}";
                        uploadedFiles.Add(relativePath);
                    }
                }

                if (uploadedFiles.Count == 2)
                {
                    // Lưu thông tin đường dẫn ảnh vào cơ sở dữ liệu
                    user.anhCCCDMatTruoc = uploadedFiles[0];
                    user.anhCCCDMatSau = uploadedFiles[1];
                    user.ngayCapNhat = DateTime.Now;
                    db_.SaveChanges();

                    return Json(new { success = true, files = uploadedFiles });
                }
                else
                {
                    return Json(new { success = false, message = "Vui lòng chọn đúng 2 ảnh." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult _SaveInfo(NhanVienUserViewModel model, int Id)
        {
            var item = db_.Users.FirstOrDefault(a => a.Id == Id);
            try
            {
                item.ngaySinh = model.NgaySinh;
                item.hoVaTen = model.HoVaTen;
                item.nguyenQuan = model.NguyenQuan;
                item.dcTamTru = model.DcTamTru;
                item.dcThuongTru = model.DcThuongTru;
                item.gioiTinh = model.GioiTinh;
                item.mstCaNhan = model.MstCaNhan;
                item.danToc = model.DanToc;
                item.tringDoVanHoa = model.TringDoVanHoa;
                item.dangThuViec = model.DangThuViec;
                db_.Entry(item).State = EntityState.Modified;
                db_.SaveChanges();
                return Json(new { status = 1, title = "", text = "Cập nhật thông tin cơ bản thành công.", obj = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = -1, title = "", text = "Lỗi: Không cấu trúc api", obj = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult _SaveInfoContact(NhanVienUserViewModel model, int Id)
        {
             var item = db_.Users.FirstOrDefault(a => a.Id == Id);
             item.email = model.Email;
             item.soDienThoai = model.SoDienThoai;
             item.soDienThoaiKhac = model.SoDienThoaiKhac;
             item.zalo = model.Zalo;
             db_.Entry(item).State = EntityState.Modified;
             db_.SaveChanges();
             return Json(new { status = 1, title = "", text = "Cập nhật thông tin liên hệ thành công.", obj = "" }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult _SaveInfoPersonal(NhanVienUserViewModel model, int Id)
        {
            var item = db_.Users.FirstOrDefault(a => a.Id == Id);
            try
            {
                item.soCCCD = model.SoCCCD;
                item.codeNganHang = model.CodeNganHang;
                item.soTaiKhoan = model.SoTaiKhoan;
                item.tenTaiKhoanNganHang = model.TenTaiKhoanNganHang;
                db_.Entry(item).State = EntityState.Modified;
                db_.SaveChanges();
                return Json(new { status = 1, title = "", text = "Cập nhật thông tin cá nhân thành công.", obj = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = -1, title = "", text = "Lỗi: Không cấu trúc api", obj = "" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
