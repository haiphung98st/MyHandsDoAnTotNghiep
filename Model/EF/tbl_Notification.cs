namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Notification
    {
        [Key]
        public long ID { get; set; }
        public long  IDkhachhang { get; set; }
        [StringLength(250)]
        public string sHinhAnh { get; set; }
        [StringLength(250)]
        public string sChiTiet { get; set; }
        [StringLength(250)]
        public string sTittle { get; set; }
        public DateTime? dNgayTB { get; set; }
        public bool bApplyForAll { get; set; }
    }
}
