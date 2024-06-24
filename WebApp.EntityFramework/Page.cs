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
    
    public partial class Page
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Page()
        {
            this.Menus = new HashSet<Menu>();
            this.UserAuthorizations = new HashSet<UserAuthorization>();
        }
    
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Menu> Menus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserAuthorization> UserAuthorizations { get; set; }
    }
}
