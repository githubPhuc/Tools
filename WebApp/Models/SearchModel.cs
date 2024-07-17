using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToolsApp.Models
{
    public class SearchViewModel
    {
        public string DiaChiTaiSanSearch { get; set; }
        public int? NhomBaiVietSearch { get; set; }
        public string IdThanhPho { get; set; }
        public string IdQuanHuyen { get; set; }
        public string IdPhuongXa { get; set; }
        public bool? ViTriSearch { get; set; }
        public decimal? DienTichTu { get; set; }
        public decimal? DienTichDen { get; set; }
        public decimal? GiaBanTu { get; set; }
        public decimal? GiaBanDen { get; set; }
        public decimal? ChieuDaiTu { get; set; }
        public decimal? ChieuDaiDen { get; set; }
        public decimal? ChieuNgangTu { get; set; }
        public decimal? ChieuNgangDen { get; set; }
        public int? KetCauSearch { get; set; }
        public string SoPhongSearch { get; set; }
        public string HuongSearch { get; set; }
    }


}