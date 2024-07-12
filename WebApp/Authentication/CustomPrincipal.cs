using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ToolsApp.Authentication
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity {     get; private set; }


        public bool IsInRole(string role)
        {
            return true;
        }

        public CustomPrincipal(string tenTaiKhoan)
        {
            this.Identity = new GenericIdentity(tenTaiKhoan);
        }
        public int UserId { get; set; }
        public string tenTaiKhoan { get; set; }
        public string anhDaiDien { get; set; }
        public string hoVaTen { get; set; }
        public string soDienThoai { get; set; }
        public string email { get; set; }
        public int capDoTaiKhoan { get; set; }
        public string ipUserHostAddress { get; set; }
    }

    public class CustomPrincipalSerializeModel
    {
        public int UserId { get; set; }
        public string tenTaiKhoan { get; set; }
        public string anhDaiDien { get; set; }
        public string hoVaTen { get; set; }
        public string soDienThoai { get; set; }
        public string email { get; set; }
        public int capDoTaiKhoan { get; set; }
        public string ipUserHostAddress { get; set; }
    }
}