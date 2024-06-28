using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ToolsApp.EntityFramework;

namespace ToolsApp.Models
{
    public partial class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "ResetCode is required")]
        public string ResetCode { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8}$", ErrorMessage = "Password must meet requirements")]
        [StringLength(255, ErrorMessage = "Must be between 8 and 255 characters", MinimumLength = 8)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 8 and 255 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
    public partial class UserJson
    {
        public string username { get; set; }
        public string password { get; set; }
    }
 
    public class ResponseObject
    {
        public string message { get; set; }
        public object data { get; set; }
        public string token { get; set; }
        public DateTime expiration { get; set; }
        public string id { get; set; }
        public string phone { get; set; }
        public string role { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int idDepartment { get; set; }
    }
    public partial class QL_UserViewModel
    {
        public int Id { get; set; }
        public string tenTaiKhoan { get; set; }
        public string matKhau { get; set; }
        public string hoVaTen { get; set; }
        public string soDienThoai { get; set; }
        public string email { get; set; }
        public string dcThuongTru { get; set; }
        public string dcTamTru { get; set; }
        public Nullable<System.DateTime> thoiGianLamViec { get; set; }
        public string soTaiKhoan { get; set; }
        public string codeNganHang { get; set; }
        public string anhDaiDien { get; set; }
        public string Pages { get; set; }
        public Nullable<bool> dangThuViec { get; set; }
        public Nullable<System.DateTime> ngayTao { get; set; }
        public Nullable<int> nguoiTao { get; set; }
        public Nullable<System.DateTime> ngayCapNhat { get; set; }
        public Nullable<int> nguoiCapNhat { get; set; }
        public Nullable<System.DateTime> ngayXoa { get; set; }
        public Nullable<int> nguoiXoa { get; set; }
        public Nullable<bool> xacNhanXoa { get; set; }
        public Nullable<bool> hieuLuc { get; set; }
    }
    public partial class QLKTPPageViewModel
    {
        public int Id { get; set; }
        public string controllerName { get; set; }
        public string actionName { get; set; }
        public string Code { get; set; }
        public string Info { get; set; }
        public Nullable<System.DateTime> ngayTao { get; set; }
        public Nullable<int> nguoiTao { get; set; }
        public Nullable<System.DateTime> ngayCapNhat { get; set; }
        public Nullable<int> nguoiCapNhat { get; set; }
        public Nullable<System.DateTime> ngayXoa { get; set; }
        public Nullable<int> nguoiXoa { get; set; }
        public Nullable<bool> xacNhanXoa { get; set; }
        public Nullable<bool> hieuLuc { get; set; }
     
    }
    public partial class QLKTPRoleViewModel
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "The field Id is required.")]
        public int Id { get; set; }
        [Display(Name = "Code")]
        [Required(ErrorMessage = "The field Code is required.")]
        public string Code { get; set; }
        [Display(Name = "Role name")]
        [Required(ErrorMessage = "The field RoleName is required.")]
        public string RoleName { get; set; }
        public string UserCreate { get; set; }
        public string UserFullnameCreate { get; set; }
        public DateTime DatetimeCreate { get; set; }
        public string UserUpdate { get; set; }
        public string UserFullnameUpdate { get; set; }
        public DateTime DatetimeUpdate { get; set; }
        public Nullable<bool> isDelete { get; set; }
        public string UserDelete { get; set; }
        public string UserFullnameDelete { get; set; }
        public DateTime DatetimeDelete { get; set; }
    }
    public partial class QLKTPMenuViewModel
    {
        public int Id { get; set; }
        public Nullable<int> parentId { get; set; }
        public string tenMenu { get; set; }
        public string moTaMenu { get; set; }
        public string iconMenu { get; set; }
        public Nullable<int> idPage { get; set; }
        public Nullable<int> sortOrder { get; set; }
        public Nullable<System.DateTime> ngayTao { get; set; }
        public Nullable<int> nguoiTao { get; set; } 
        public Nullable<System.DateTime> ngayCapNhat { get; set; }
        public Nullable<int> nguoiCapNhat { get; set; }
        public Nullable<System.DateTime> ngayXoa { get; set; }
        public Nullable<int> nguoiXoa { get; set; }
        public Nullable<bool> xacNhanXoa { get; set; }
        public Nullable<bool> hieuLuc { get; set; }

    }
    public partial class QLKTPSoftwareGroupViewModel
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "The field Id is required.")]
        public int Id { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "The field Title is required.")]
        public string Title { get; set; }
        public string Domain { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public Nullable<int> OrderNo { get; set; }
        public string UserCreate { get; set; }
        public string UserFullnameCreate { get; set; }
        public DateTime DatetimeCreate { get; set; }
        public string UserUpdate { get; set; }
        public string UserFullnameUpdate { get; set; }
        public DateTime DatetimeUpdate { get; set; }
        public Nullable<bool> isDelete { get; set; }
        public string UserDelete { get; set; }
        public string UserFullnameDelete { get; set; }
        public DateTime DatetimeDelete { get; set; }
        public string ColorCode { get; set; }
    }
    public partial class QLKTPSoftwareViewModel
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "The field Id is required.")]
        public int Id { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "The field Title is required.")]
        public string Title { get; set; }
        public Nullable<int> GroupId { get; set; }
        public string Domain { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public Nullable<int> OrderNo { get; set; }
        public string UserCreate { get; set; }
        public string UserFullnameCreate { get; set; }
        public DateTime DatetimeCreate { get; set; }
        public string UserUpdate { get; set; }
        public string UserFullnameUpdate { get; set; }
        public DateTime DatetimeUpdate { get; set; }
        public Nullable<bool> isDelete { get; set; }
        public string UserDelete { get; set; }
        public string UserFullnameDelete { get; set; }
        public DateTime DatetimeDelete { get; set; }
        public string Sites { get; set; }
    }
    public partial class QLKTPUserBlackListViewModel
    {
        public long Id { get; set; }
        public string BlackListUser { get; set; }
        public string BlackListFullname { get; set; }
        public string Email { get; set; }
        public string PrincipalId { get; set; }
        public string PrincipalName { get; set; }
        public string Ticket { get; set; }
        public string Reason { get; set; }
        public string UserCreate { get; set; }
        public string UserFullnameCreate { get; set; }
        public DateTime DatetimeCreate { get; set; }
        public string UserUpdate { get; set; }
        public string UserFullnameUpdate { get; set; }
        public DateTime DatetimeUpdate { get; set; }
        public Nullable<bool> isDelete { get; set; }
        public string UserDelete { get; set; }
        public string UserFullnameDelete { get; set; }
        public DateTime DatetimeDelete { get; set; }
    }
    public partial class SitesViewModel
    {
        [Display(Name = "Site Code")]
        [Required(ErrorMessage = "The field SiteCode is required.")]
        [StringLength(3, ErrorMessage = "Must be between 1 and 3 characters", MinimumLength = 1)]
        public string SiteCode { get; set; }
        [Display(Name = "Site Name")]
        [Required(ErrorMessage = "The field SiteName is required.")]
        public string SiteName { get; set; }
    }
    public partial class TaiKhoanViewModels
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public Nullable<bool> IsHieuLuc { get; set; }
        public string NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        public string NguoiCapNhat { get; set; }
        public DateTime NgayCapNhat { get; set; }
    }
    public partial class Response
    {
        public string Status { get; set; }

        public string Message { get; set; }
    }
  
    public partial class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Pages { get; set; }
    }
    public partial class RegisterViewModel
    {

        public string tenTaiKhoan { get; set; }
        public string matKhau { get; set; }
        public string hoVaTen { get; set; }
        public string soDienThoai { get; set; }
        public string email { get; set; }
        public string dcThuongTru { get; set; }
        public string dcTamTru { get; set; }
        public Nullable<System.DateTime> thoiGianLamViec { get; set; }
        public string soTaiKhoan { get; set; }
        public string codeNganHang { get; set; }
        public string anhDaiDien { get; set; }
        public Nullable<bool> dangThuViec { get; set; }
        public Nullable<System.DateTime> ngayTao { get; set; }
        public Nullable<int> nguoiTao { get; set; }
        public Nullable<System.DateTime> ngayCapNhat { get; set; }
        public Nullable<int> nguoiCapNhat { get; set; }
        public Nullable<System.DateTime> ngayXoa { get; set; }
        public Nullable<int> nguoiXoa { get; set; }
        public Nullable<bool> xacNhanXoa { get; set; }
        public Nullable<bool> hieuLuc { get; set; }
    }
    
    public class KetCauModels
    {
        public int id { get; set; }
        public string tenKetCau { get; set; }
        public string tenNguoiTao { get; set; }
        public string tenNguoiCapNhat { get; set; }

        public Nullable<bool> trangThai { get; set; }
        public Nullable<System.DateTime> ngayTao { get; set; }
        public Nullable<int> nguoiTao { get; set; }
        public Nullable<System.DateTime> ngayCapNhat { get; set; }
        public Nullable<int> nguoiCapNhat { get; set; }
        public Nullable<System.DateTime> ngayXoa { get; set; }
        public Nullable<int> nguoiXoa { get; set; }
        public Nullable<bool> xacNhanXoa { get; set; }
    }
}   