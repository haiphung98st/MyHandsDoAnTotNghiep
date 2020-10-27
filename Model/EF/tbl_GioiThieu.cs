namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_GioiThieu
    {
        [Key]
        public long IDGioiThieu { get; set; }

        [StringLength(250)]
        public string sTenGioiThieu { get; set; }

        [StringLength(250)]
        public string sTenGioiThieuMeta { get; set; }

        [Column(TypeName = "ntext")]
        public string sNoiDung { get; set; }

        [StringLength(250)]
        public string sImage { get; set; }

        public DateTime? dNgayTao { get; set; }

        [StringLength(50)]
        public string sNguoiTao { get; set; }

        public DateTime? dNgaySua { get; set; }

        [StringLength(50)]
        public string sNguoiSua { get; set; }

        [StringLength(250)]
        public string sMetaKeywords { get; set; }

        [StringLength(250)]
        public string sMetaDescription { get; set; }

        public bool? bStatus { get; set; }
    }
}
