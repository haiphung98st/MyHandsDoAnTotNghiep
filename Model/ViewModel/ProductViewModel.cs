using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ProductViewModel
    {
        public long ID { get; set; }
        public string sImages { get; set; }
        public int? iSoluong { get; set; }
        public string sTenSanPham { get; set; }

        public string sMaSanPham { get; set; }

        public string sTenSanPhamMeta { get; set; }

        public decimal? dGiaBan { get; set; }
        public decimal? dGiaKhuyenMai { get; set; }

        public DateTime? dNgayTao { get; set; }
        public string sTenDanhMuc { get; set; }

        public string sTenDanhMucMeta { get; set; }
    }
}
