namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Slice
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string sImage { get; set; }

        public int? iDisplayOrder { get; set; }

        [StringLength(250)]
        public string sLink { get; set; }

        [StringLength(50)]
        public string sMoTa { get; set; }

        public bool? bStatus { get; set; }

        public DateTime? dNgayTao { get; set; }

        [StringLength(50)]
        public string sNguoiTao { get; set; }

        public DateTime? dNgaySua { get; set; }

        [StringLength(50)]
        public string sNguoiSua { get; set; }
    }
}
