namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Feedback
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string sTenKH { get; set; }

        [StringLength(250)]
        public string sNoiDung { get; set; }

        [StringLength(50)]
        public string sSoDienThoai { get; set; }

        [StringLength(50)]
        public string sDiaChi { get; set; }

        [StringLength(50)]
        public string sEmail { get; set; }

        public DateTime? dNgayTao { get; set; }

        public bool? bStatus { get; set; }
    }
}
