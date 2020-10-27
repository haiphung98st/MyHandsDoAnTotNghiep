using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyHandsDoAnTotNghiep.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Hãy nhập tên đăng nhập")]
        public String username { get; set; }

        [Required(ErrorMessage = "Hãy nhập mật khẩu")]
        public String password { get; set; }

        public bool rememberMe { get; set; }
    }
}