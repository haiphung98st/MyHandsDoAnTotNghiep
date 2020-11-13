using Model.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    [Serializable]
    public class HoaDonViewModel
    {
        public tbl_TaiKhoan taikhoan { get; set; }
        public tbl_HoaDon hoadon { get; set; }
        public tbl_TrangThaiHoaDon trangThaiHoaDon { get; set; }

        public long ID { get; set; }
        [Display(Name ="Tên người nhận")]
        public string sTenNguoiNhan { get; set; }
        [Display(Name = "Số điện thoại")]
        public string sSoDienThoai { get; set; }
        [Display(Name = "Email")]
        public string sEmail { get; set; }
        [Display(Name = "Địa chỉ")]
        public string sDiaChi { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime? dNgayTao { get; set; }
        [Display(Name = "Đơn giá")]
        public decimal? dDongia { get; set; }
        [Display(Name = "Giá nhập")]
        public decimal? dGiaNhap { get; set; }
        [Display(Name = "Số lượng")]
        public int? iSoLuong { get; set; }
        [Display(Name = "Trạng thái")]
        public string sTenTrangThai { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string sTenSanPham { get; set; }
    }
}
