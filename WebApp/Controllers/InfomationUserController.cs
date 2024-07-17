using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        //[HttpPost]
        //[AllowAnonymous]
        //public JsonResult _SaveImageAvatar(string image, int Id)
        //{
        //    try
        //    {
        //        var user = db_.Users.FirstOrDefault(a => a.Id == Id);
        //        user.imageName = image;
        //        db_.Entry(user).State = EntityState.Modified;
        //        db_.SaveChanges();
        //        return Json(new { status = 1, title = "", text = "Cập nhật ảnh thành công.", obj = "" }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { status = -1, title = "", text = "Lỗi: Không cấu trúc api", obj = "" }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        [HttpPost]
        [AllowAnonymous]
        public JsonResult _SaveImageCCCD(string frontCCCD, string backCCCD, int Id)
        {
            return Json(new { status = -1 }, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //[AllowAnonymous]
        //public JsonResult _SaveInfo(ProfileUserviewModel model, int Id)
        //{
        //    var item = db_.Users.FirstOrDefault(a => a.Id == Id);
        //    try
        //    {
        //        item.birtDate = model.birtDate;
        //        item.placeOfBirt = model.placeOfBirt;
        //        item.placeOfOrigin = model.placeOfOrigin;
        //        item.curResidence = model.curResidence;
        //        item.resident = model.resident;
        //        item.temResidence = model.temResidence;
        //        item.gender = model.gender;
        //        item.taxCode = model.taxCode;
        //        item.marital = model.marital;
        //        item.ethnicity = model.ethnicity;
        //        item.religion = model.religion;
        //        item.nationality = model.nationality;
        //        item.educationalLevel = model.educationalLevel;
        //        db_.Entry(item).State = EntityState.Modified;
        //        db_.SaveChanges();
        //        return Json(new { status = 1, title = "", text = "Cập nhật thông tin cơ bản thành công.", obj = "" }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { status = -1, title = "", text = "Lỗi: Không cấu trúc api", obj = "" }, JsonRequestBehavior.AllowGet);
        //    }


        //}
        //    [HttpPost]
        //    [AllowAnonymous]
        //    public JsonResult _SaveInfoContact(ProfileUserviewModel model, int Id, string Email)
        //    {
        //        var user = db_.AspNetUsers.FirstOrDefault(a => a.Id == Id);
        //        var item = db_.Users.FirstOrDefault(a => a.Id == Id);
        //        if (model.otherNum != null && model.otherNum.Length > 0)
        //        {
        //            if (model.otherNum.Length != 10)
        //            {
        //                return Json(new { status = -1, title = "", text = "Số điện thoại chưa hợp lệ", obj = "" }, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                try
        //                {
        //                    user.Email = Email;
        //                    item.otherNum = model.otherNum;
        //                    item.address = model.address;
        //                    item.zalo = model.zalo;
        //                    db_.Entry(item).State = EntityState.Modified;
        //                    db_.SaveChanges();
        //                    return Json(new { status = 1, title = "", text = "Cập nhật thông tin liên hệ thành công.", obj = "" }, JsonRequestBehavior.AllowGet);
        //                }
        //                catch (Exception ex)
        //                {
        //                    return Json(new { status = -1, title = "", text = "Lỗi: Không cấu trúc api", obj = "" }, JsonRequestBehavior.AllowGet);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            try
        //            {
        //                user.Email = Email;
        //                item.otherNum = model.otherNum;
        //                item.address = model.address;
        //                item.zalo = model.zalo;
        //                db_.Entry(item).State = EntityState.Modified;
        //                db_.SaveChanges();
        //                return Json(new { status = 1, title = "", text = "Cập nhật thông tin liên hệ thành công.", obj = "" }, JsonRequestBehavior.AllowGet);
        //            }
        //            catch (Exception ex)
        //            {
        //                return Json(new { status = -1, title = "", text = "Lỗi: Không cấu trúc api", obj = "" }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //    }
        //    [HttpPost]
        //    [AllowAnonymous]
        //    public JsonResult _SaveInfoPersonal(ProfileUserviewModel model, int Id)
        //    {
        //        var item = db_.Users.FirstOrDefault(a => a.Id == Id);
        //        try
        //        {
        //            item.CCCD = model.CCCD;
        //            item.passPortNum = model.passPortNum;
        //            item.codeBank = model.codeBank;
        //            item.numBank = model.numBank;
        //            item.nameAccountBank = model.nameAccountBank;
        //            db_.Entry(item).State = EntityState.Modified;
        //            db_.SaveChanges();
        //            return Json(new { status = 1, title = "", text = "Cập nhật thông tin cá nhân thành công.", obj = "" }, JsonRequestBehavior.AllowGet);
        //        }
        //        catch (Exception ex)
        //        {
        //            return Json(new { status = -1, title = "", text = "Lỗi: Không cấu trúc api", obj = "" }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //}
    }
}