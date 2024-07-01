using Newtonsoft.Json;
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
    public class LichSuNguoiDungController : BaseController
    {
        // GET: CartManagerment
        crmcustomscontext db_ = new crmcustomscontext();
        public async Task<ActionResult> Index()
        {
            var data = db_.Users.AsNoTracking();
            var dataUser = await data.Where(a => a.Id == User.UserId).FirstOrDefaultAsync();
            var dataUserDanhMuc = await data.Where(a => a.Id != User.UserId && a.capDoTaiKhoan < dataUser.capDoTaiKhoan).ToListAsync();
            ViewBag.dataUser = dataUser;
            ViewBag.dataUserDanhMuc = dataUserDanhMuc;
            return View();
        }
        public async Task<ActionResult> GetList(int nguoiDungSearch, string ngayBatDauSearch, string ngayKetThucSearch)
        {
       
            var data = await db_.LogHistorys.Where(a=>
                (a.Id == nguoiDungSearch || nguoiDungSearch == 0 || nguoiDungSearch == null)//&&
               // (a.ngayTao >= ngayBatDauSearch)
                ).AsNoTracking().ToListAsync();
            if (User.capDoTaiKhoan == 5)
            {
                data = await db_.LogHistorys.AsNoTracking().Where(a => a.idUser == User.UserId).ToListAsync();
            }

            ViewBag.data= data;
            


            return PartialView();
        }
    
        

    }
}