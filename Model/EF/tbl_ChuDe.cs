namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_ChuDe
    {
        [Key]
        public long IDChuDe { get; set; }

        [StringLength(250)]
        [Display(Name ="Tên chủ đề")]
        [Required(ErrorMessage ="Hãy nhập tên chủ đề")]
        public string sTenChuDe { get; set; }

        [StringLength(250)]
        public string sTenChuDeMeta { get; set; }
        [StringLength(250)]
        [Display(Name = "Hình ảnh")]
        public string sImage { get; set; }

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
        [Display(Name = "Trạng thái")]
        public bool bStatus { get; set; }

        public bool? bShowOnHome { get; set; }
    }
}
