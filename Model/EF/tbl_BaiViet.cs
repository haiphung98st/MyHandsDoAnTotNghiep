namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_BaiViet
    {
        [Key]
        public long IDBaiViet { get; set; }

        [StringLength(250)]
        public string sTieuDe { get; set; }

        [StringLength(250)]
        public string sTenTieuDecMeta { get; set; }

        [StringLength(250)]
        public string sMoTa { get; set; }

        [StringLength(250)]
        public string sImages { get; set; }

        public long? IDChuDe { get; set; }

        [Column(TypeName = "ntext")]
        public string sXemTruoc { get; set; }

        [Column(TypeName = "ntext")]
        public string sNoiDung { get; set; }

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

        public bool bStatus { get; set; }

        public DateTime? dTopHot { get; set; }

        public int? iViewCount { get; set; }

        [StringLength(500)]
        public string sTags { get; set; }
    }
}
