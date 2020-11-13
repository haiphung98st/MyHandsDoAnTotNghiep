using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHandsDoAnTotNghiep.Models
{
    [Serializable]
    public class GioHangItems
    {
        public tbl_TaiKhoan TaiKhoan { get; set; }
        public tbl_SanPham SanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal? TongTien { get; set; }
    }
}