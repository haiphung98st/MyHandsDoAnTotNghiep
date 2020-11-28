namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SanPhamComment
    {
        [Key]
        public long IDComment { get; set; }
        public long IDSanPham { get; set; }
        [StringLength(250)]
        public string sTenNguoiDung { get; set; }
        public DateTime dNgayTao { get; set; }
        public DateTime? dNgaySua { get; set; }
        public bool bStatus { get; set; }

        [StringLength(250)]
        [Display(Name = "Bình luận")]
        [Required(ErrorMessage = " *Hãy nhập nội dung")]
        public string sNoiDung { get; set; }
    }
}
