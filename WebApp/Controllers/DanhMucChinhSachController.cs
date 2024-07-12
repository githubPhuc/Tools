using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ToolsApp.Authentication;
using ToolsApp.EntityFramework;
using ToolsApp.Models;
using ToolsApp.Utilities;

namespace ToolsApp.Controllers
{
    [Authorize]
    public class DanhMucChinhSachController : BaseController
    {
        // GET: CartManagerment
        crmcustomscontext db_ = new crmcustomscontext();
        public ActionResult Index()
        {
          
            return View();
        }
        public ActionResult GetList(string ngayBatDauSearch, string ngayKetThucSearch)
        {

            return View();
        }


    }
}