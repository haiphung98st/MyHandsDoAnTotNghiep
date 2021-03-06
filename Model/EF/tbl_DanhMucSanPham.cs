namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DanhMucSanPham
    {
        [Key]
        public long IDDanhMuc { get; set; }

        [StringLength(250)]
        public string sTenDanhMuc { get; set; }

        [StringLength(250)]
        public string sTenDanhMucMeta { get; set; }

        public long? iParentID { get; set; }

        public int? iDisplayOrder { get; set; }

        [StringLength(250)]
        public string sSeoTitle { get; set; }

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

        public bool? bShowOnHome { get; set; }

        [StringLength(250)]
        public string sHinhAnhDanhMuc { get; set; }
    }
}
