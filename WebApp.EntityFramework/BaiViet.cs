//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToolsApp.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class BaiViet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaiViet()
        {
            this.HinhAnhBaiViets = new HashSet<HinhAnhBaiViet>();
        }
    
        public int id { get; set; }
        public Nullable<int> idNhomBaiViet { get; set; }
        public Nullable<int> idUser { get; set; }
        public Nullable<int> idKetCau { get; set; }
        public Nullable<int> maBaiViet { get; set; }
        public string motaKetCau { get; set; }
        public string huong { get; set; }
        public string tenBaiViet { get; set; }
        public string diaChiTaiSan { get; set; }
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
    
        public virtual KetCau KetCau { get; set; }
        public virtual NhomBaiViet NhomBaiViet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HinhAnhBaiViet> HinhAnhBaiViets { get; set; }
        public virtual User User { get; set; }
    }
}
