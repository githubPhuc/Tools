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
    public partial class ChinhSachModels
    {
        public int id { get; set; }
        public string tieuDe { get; set; }
        public string noiDung { get; set; }
        public string tenNguoiTao { get; set; }
        public Nullable<bool> trangThai { get; set; }
        public Nullable<System.DateTime> ngayTao { get; set; }
        public Nullable<int> nguoiTao { get; set; }
        public Nullable<System.DateTime> ngayCapNhat { get; set; }
        public Nullable<int> nguoiCapNhat { get; set; }
        public Nullable<System.DateTime> ngayXoa { get; set; }
        public Nullable<int> nguoiXoa { get; set; }
        public Nullable<bool> xacNhanXoa { get; set; }
    }
    public partial class NhomBaiVietModels
    {
        public int id { get; set; }
        public string tenNhom { get; set; }
        public string MoTa { get; set; }
        public string tenNguoiTao { get; set; }
        public Nullable<int> idTyGia { get; set; }
        public string tenTyGia { get; set; }
        public Nullable<bool> trangThai { get; set; }
        public Nullable<System.DateTime> ngayTao { get; set; }
        public Nullable<int> nguoiTao { get; set; }
        public Nullable<System.DateTime> ngayCapNhat { get; set; }
        public Nullable<int> nguoiCapNhat { get; set; }
        public Nullable<System.DateTime> ngayXoa { get; set; }
        public Nullable<int> nguoiXoa { get; set; }
        public Nullable<bool> xacNhanXoa { get; set; }
    }
    public class NhanVienUserViewModel
    {
        public int Id { get; set; }
        public string TenTaiKhoan { get; set; }
        public string HoVaTen { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DcThuongTru { get; set; }
        public string DcTamTru { get; set; }
        public DateTime? ThoiGianLamViec { get; set; }
        public string SoTaiKhoan { get; set; }
        public string CodeNganHang { get; set; }
        public string AnhDaiDien { get; set; }
        public bool? DangThuViec { get; set; }
        public DateTime? NgayTao { get; set; }
        public int? NguoiTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public int? NguoiCapNhat { get; set; }
        public DateTime? NgayXoa { get; set; }
        public int? NguoiXoa { get; set; }
        public bool? XacNhanXoa { get; set; }
        public bool? HieuLuc { get; set; }
        public int? CapDoTaiKhoan { get; set; } 
        public string NguyenQuan { get; set; }
        public DateTime? NgaySinh { get; set; }
        public bool? GioiTinh { get; set; }
        public string MstCaNhan { get; set; }
        public string DanToc { get; set; }
        public string TringDoVanHoa { get; set; }
        public string SoDienThoaiKhac { get; set; }
        public string Zalo { get; set; }
        public string SoCCCD { get; set; }
        public string AnhCCCDMatTruoc { get; set; }
        public string AnhCCCDMatSau { get; set; }
        public string TenTaiKhoanNganHang { get; set; }
    }
    public class BaiVietViewModel
    {
        public int id { get; set; }
        public Nullable<int> idNhomBaiViet { get; set; }
        public Nullable<int> idUser { get; set; }
        public Nullable<int> idKetCau { get; set; }
        public Nullable<int> maBaiViet { get; set; }
        public string motaKetCau { get; set; }
        public string huong { get; set; }
        public string tenBaiViet { get; set; }
        public string diaChiTaiSan { get; set; }
        public string diaChiDayDu { get; set; }
        public Nullable<int> soPhong { get; set; }
        public Nullable<int> idThanhPho { get; set; }
        public Nullable<int> idQuanHuyen { get; set; }
        public Nullable<int> idPhuongXa { get; set; }
        public Nullable<decimal> dienTichDat { get; set; }
        public Nullable<decimal> dienTichXayDung { get; set; }
        public Nullable<decimal> dienTichSuDung { get; set; }
        public Nullable<decimal> chieuNgang { get; set; }
        public Nullable<decimal> chieuDai { get; set; }
        public Nullable<bool> viTri { get; set; }
        public string moTaViTri { get; set; }
        public Nullable<decimal> giaBan { get; set; }
        public string moTaGia { get; set; }
        public Nullable<decimal> giaM2Dat { get; set; }
        public Nullable<decimal> giaM2XayDung { get; set; }
        public Nullable<decimal> giaM2SuDung { get; set; }
        public string note { get; set; }
        public Nullable<bool> trangThaiGoiDien { get; set; }
        public Nullable<System.DateTime> thoiGianGoiDien { get; set; }
        public string tenKh { get; set; }
        public Nullable<int> sdtKh { get; set; }
        public string emailKh { get; set; }
        public Nullable<System.DateTime> ngaySinhKh { get; set; }
        public string diachiKh { get; set; }
        public Nullable<int> CCCDKh { get; set; }
        public Nullable<bool> trangThai { get; set; }
        public Nullable<System.DateTime> ngayTao { get; set; }
        public Nullable<int> nguoiTao { get; set; }
        public Nullable<System.DateTime> ngayCapNhat { get; set; }
        public Nullable<int> nguoiCapNhat { get; set; }
        public Nullable<System.DateTime> ngayXoa { get; set; }
        public Nullable<int> nguoiXoa { get; set; }
        public Nullable<bool> xacNhanXoa { get; set; }
        public Nullable<int> tinhTrang { get; set; }
        public  Config Config { get; set; }
        public KetCau KetCau { get; set; }
        public NhomBaiViet NhomBaiViet { get; set; }
        public ICollection<HinhAnhBaiViet> HinhAnhBaiViets { get; set; }

        public User User { get; set; }
        public string MoTaTyGia { get; set; }
    }
    public class HinhAnhViewModel
    {
        public int id { get; set; }
        public string urlPath { get; set; }
        public bool? trangThai { get; set; }
        public DateTime? ngayTao { get; set; }
        public int? nguoiTao { get; set; }
        public DateTime? ngayCapNhat { get; set; }
        public int? nguoiCapNhat { get; set; }
        public DateTime? ngayXoa { get; set; }
        public int? nguoiXoa { get; set; }
        public bool? xacNhanXoa { get; set; }
    }
    public class HinhAnhBaiVietViewModel
    {
        public int id { get; set; }
        public int? idBaiViet { get; set; }
        public int? idHinhAnh { get; set; }
        public bool? trangThai { get; set; }
        public DateTime? ngayTao { get; set; }
        public int? nguoiTao { get; set; }
        public DateTime? ngayCapNhat { get; set; }
        public int? nguoiCapNhat { get; set; }
        public DateTime? ngayXoa { get; set; }
        public int? nguoiXoa { get; set; }
        public bool? xacNhanXoa { get; set; }

        public virtual BaiViet BaiViet { get; set; }
        public virtual HinhAnh HinhAnh { get; set; }
    }
    public class HinhAnhBaiVietDto
    {
        public int Id { get; set; }
        public string UrlPath { get; set; }
    }
    public class TyGiaViewModel
    {
        public int Id { get; set; }
        public string TyGiaMoTa { get; set; }
    }
    public class LogHistoryViewModel
    {
        public int Id { get; set; }
        public int? idUser { get; set; }
        public string hoVaTen { get; set; }
        public string tenTaiKhoan { get; set; }
        public string moTa { get; set; }
        public string moTaChiTiet { get; set; }
        public DateTime? ngayTao { get; set; }
        public int? nguoiTao { get; set; }
        public DateTime? ngayCapNhat { get; set; }
        public int? nguoiCapNhat { get; set; }
        public DateTime? ngayXoa { get; set; }
        public int? nguoiXoa { get; set; }
        public bool? xacNhanXoa { get; set; }
        public bool? hieuLuc { get; set; }
        public string ipUserHostAddress { get; set; }
        public int? IdBaiViet { get; set; }
    }
}