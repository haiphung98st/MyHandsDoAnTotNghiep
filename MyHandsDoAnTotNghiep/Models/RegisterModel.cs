using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyHandsDoAnTotNghiep.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { get; set; }
        [Display(Name ="Tên đăng nhập")]
        [Required(ErrorMessage ="Hãy nhập tên đăng nhập")]
        public string sTenDangNhap { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Hãy nhập mật khẩu")]
        [StringLength(50,MinimumLength =6,ErrorMessage ="Mật khẩu phải nhiều hơn 6 ký tự")]
        public string sMatKhau { get; set; }
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("sMatKhau",ErrorMessage ="Mật khẩu nhập lại không trùng khớp")]
        public string sXacNhanMatKhau { get; set; }
        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Hãy nhập họ tên")]
        public string sTenNguoiDung { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Hãy nhập Email")]
        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage ="Email không đúng định dạng")]
        public string sEmail { get; set; }
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Hãy nhập địa chỉ")]
        public string sDiaChi { get; set; }
        [Display(Name = "Số điện thoại")]
        [RegularExpression(@"0[0-9\s.-]{9,13}", ErrorMessage = "Số điện thoại không đúng định dạng")]

        [Required(ErrorMessage = "Hãy nhập số điện thoại")]
        public string sSDT { get; set; }
        [Display(Name = "Tỉnh/Thành phố")]
        public string iTinhThanhID { get; set; }

        [Display(Name = "Quận/Huyện")]
        public string iQuanHuyenID { get; set; }
        //[RegularExpression(@"",ErrorMessage ="")]
    }
}