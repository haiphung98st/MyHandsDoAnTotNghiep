namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_HinhAnhSP
    {
        [Key]
        public long IDHinhAnh { get; set; }
        public long IDSanPham { get; set; }
        [StringLength(250)]
        public string sDuongDan { get; set; }
    }
}
