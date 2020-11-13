namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_NCC
    {
        [Key]

        public long IDncc { get; set; }
        [StringLength(50)]
        [Display(Name ="Tên nhà cung cấp")]
        public string sTenNCC { get; set; }
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
        [Display(Name = "Trạng thái")]

        public bool bStatus { get; set; }
    }
}
