namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_HoaDon
    {
        public long ID { get; set; }

        public DateTime? dNgayTao { get; set; }
        public DateTime? dNgaySua { get; set; }


        public long? IDKhachHang { get; set; }
        public long iMaTrangThai { get; set; }

        [StringLength(50)]
        public string sTenNguoiNhan { get; set; }
        [StringLength(50)]
        public string sNguoiTiepNhan { get; set; }

        [StringLength(50)]
        public string sSDTnguoiNhan { get; set; }

        [StringLength(50)]
        public string sEmailNguoiNhan { get; set; }
        [StringLength(250)]
        public string sDiaChi { get; set; }
        [StringLength(50)]
        public string sFormOfPayment { get; set; }
        [StringLength(50)]
        public string sBankCode { get; set; }
        public int? iStatus { get; set; }
    }
}
