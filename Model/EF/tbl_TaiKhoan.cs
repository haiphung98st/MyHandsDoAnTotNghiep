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
        public string sTenTaiKhoan { get; set; }

        [StringLength(250)]
        public string sMatKhau { get; set; }

        [StringLength(50)]
        public string sHoTen { get; set; }

        [StringLength(50)]
        public string sDiaChi { get; set; }

        [StringLength(50)]
        public string sEmail { get; set; }

        [StringLength(50)]
        public string sSDT { get; set; }

        public DateTime? dNgayTao { get; set; }

        [StringLength(50)]
        public string sNguoiTao { get; set; }

        public DateTime? dNgaySua { get; set; }

        [StringLength(50)]
        public string sNguoiSua { get; set; }

        public bool bStatus { get; set; }

        public int? iTinhThanhID { get; set; }
        public int? iQuanHuyenID { get; set; }
        [StringLength(50)]
        public string sQuyen { get; set; }

    }
}
