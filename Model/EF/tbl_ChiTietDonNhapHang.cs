namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class tbl_ChiTietDonNhapHang
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IDDonNhapHang { get; set; }
        [Key]
        [Column(Order =1)]
        public long IDSanPham { get; set; }
        public int iSoLuongNhap { get; set; }
        public int iSoLuongLoai { get; set; }
    }
}
