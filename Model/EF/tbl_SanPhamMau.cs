namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SanPhamMau
    {
        [Key]
        public long ID { get; set; }
        public long IDDanhMucMau { get; set; }
        public bool? bStatus { get; set; }
        [StringLength(250)]
        public string sTenSanPhamMau { get; set; }
        [StringLength(250)]

        public string sHinhAnhMau { get; set; }
        public DateTime? dNgayTao { get; set; }
        public DateTime? dNgaySua { get; set; }
        [StringLength(50)]

        public string sNguoiTao { get; set; }
        [StringLength(50)]

        public string sNguoiSua { get; set; }
    }
}
