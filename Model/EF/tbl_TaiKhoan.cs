namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_TaiKhoan
    {
        public long ID { get; set; }

        [StringLength(50)]
        [Display(Name ="Tên tài khoản")]
        public string sTenTaiKhoan { get; set; }
        [Display(Name = "Mật khẩu")]

        [StringLength(250)]
        public string sMatKhau { get; set; }

        [StringLength(50)]
        [Display(Name = "Họ và tên")]

        public string sHoTen { get; set; }

        [StringLength(50)]
        [Display(Name = "Địa chỉ")]

        public string sDiaChi { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]

        public string sEmail { get; set; }

        [StringLength(50)]
        [Display(Name = "Số điện thoại")]

        public string sSDT { get; set; }

        public DateTime? dNgayTao { get; set; }

        [StringLength(50)]
        public string sNguoiTao { get; set; }

        public DateTime? dNgaySua { get; set; }

        [StringLength(50)]
        public string sNguoiSua { get; set; }
        [Display(Name = "Trạng thái")]

        public bool bStatus { get; set; }
        [Display(Name = "Tỉnh thành")]

        public int? iTinhThanhID { get; set; }
        [Display(Name = "Quận huyện")]

        public int? iQuanHuyenID { get; set; }
        [StringLength(50)]
        [Display(Name = "Quyền")]

        public string sQuyen { get; set; }

    }
}
