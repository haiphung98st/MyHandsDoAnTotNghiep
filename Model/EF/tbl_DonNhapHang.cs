namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class tbl_DonNhapHang
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IDPhieuYeuCau { get; set; }
        [Key]
        [Column(Order = 1)]

        public long IDDonNhapHang { get; set; }
        public DateTime? dNgayTao { get; set; }
        public object ID { get; internal set; }
    }
}
