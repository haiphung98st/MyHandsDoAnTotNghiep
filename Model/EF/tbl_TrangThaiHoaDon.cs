namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public class tbl_TrangThaiHoaDon
    {
        [Key]
        public long ID { get; set; }
        [StringLength(50)]
        public string sTenTrangThai { get; set; }

        [StringLength(50)]
        public string sNguoiTao { get; set; }

        [StringLength(50)]
        public string sNguoiSua { get; set; }
        public DateTime dNgayTao { get; set; }
        public DateTime? dNgaySua { get; set; }

    }
}
