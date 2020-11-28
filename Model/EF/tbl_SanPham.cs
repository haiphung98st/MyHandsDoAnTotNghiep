namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SanPham
    {
        [Key]
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Hãy nhập tên sản phẩm")]
        public string sTenSanPham { get; set; }

        [StringLength(10)]
        [Display(Name = "Mã sản phẩm")]
        //[Required(ErrorMessage = "Hãy nhập mã sản phẩm")]
        public string sMaSanPham { get; set; }

        [StringLength(250)]
        public string sTenSanPhamMeta { get; set; }

        [StringLength(250)]
        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Hãy nhập mô tả")]
        public string sMoTa { get; set; }

        [StringLength(250)]
        [Display(Name = "Hình ảnh")]
        [Required(ErrorMessage = "Bạn chưa chọn hình ảnh")]
        public string sImages { get; set; }

        [Column(TypeName = "xml")]
        public string sMoreImages { get; set; }
        [Display(Name = "Giá bán")]
        //[Required(ErrorMessage = "Hãy nhập giá bán")]
        public decimal? dGiaBan { get; set; }
        [Display(Name = "Giá khuyến mãi")]
        
        public decimal? dGiaKhuyenMai { get; set; } 
        [Display(Name = "Giá nhập")]
        //[Required(ErrorMessage = "Hãy nhập giá nhập")]

        public decimal? dGiaNhap { get; set; }
        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "Hãy nhập số lượng")]

        public int iSoLuong { get; set; }

        public bool? bIncludeVAT { get; set; }
        public bool? bCustomActive { get; set; }

        public long? IDDanhMuc { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Chi tiết sản phẩm")]
        [Required(ErrorMessage = "Hãy nhập chi tiết sản phẩm")]
        public string sChiTietSanPham { get; set; }
        [Display(Name = "Tháng bảo hành")]
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
        [Display(Name ="Hiển thị")]
        public bool bStatus { get; set; }
        [Display(Name = "Top hot")]

        public DateTime? dTopHot { get; set; }

        public int? iViewCount { get; set; }
    }
}
