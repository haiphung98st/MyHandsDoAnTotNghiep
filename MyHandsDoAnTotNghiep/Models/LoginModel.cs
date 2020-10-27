using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyHandsDoAnTotNghiep.Models
{
    public class LoginModel
    {
        [Key]
        public long ID { get; set; }
        [Display(Name ="Tên đăng nhập")]
        [Required(ErrorMessage ="Hãy nhập tên đăng nhập")]
        public string sTenTaiKhoanLogin { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Hãy nhập mật khẩu")]
        public string sMatKhauLogin { get; set; }
    }
}