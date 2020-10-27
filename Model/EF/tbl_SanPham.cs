namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SanPham
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Hãy nhập tên sản phẩm")]
        public string sTenSanPham { get; set; }

        [StringLength(10)]
        public string sMaSanPham { get; set; }

        [StringLength(250)]
        public string sTenSanPhamMeta { get; set; }

        [StringLength(250)]
        public string sMoTa { get; set; }

        [StringLength(250)]
        public string sImages { get; set; }

        [Column(TypeName = "xml")]
        public string sMoreImages { get; set; }

        public decimal? dGiaBan { get; set; }

        public decimal? dGiaKhuyenMai { get; set; }

        public int? iSoLuong { get; set; }

        public bool? bIncludeVAT { get; set; }

        public long? IDDanhMuc { get; set; }

        [Column(TypeName = "ntext")]
        public string sChiTietSanPham { get; set; }

        public int? iThangBaoHanh { get; set; }

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
    }
}
