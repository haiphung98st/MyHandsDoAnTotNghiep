using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHandsDoAnTotNghiep
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string GroupID { set; get; }

    }
}