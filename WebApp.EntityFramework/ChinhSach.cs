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
    
    public partial class ChinhSach
    {
        public int id { get; set; }
        public string tieuDe { get; set; }
        public string noiDung { get; set; }
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
