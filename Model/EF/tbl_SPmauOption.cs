namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SPmauOption

    {
        [Key]
        public long ID { get; set; }
        public long IDDanhMucMauSP { get; set; }
        public long IDOption { get; set; }


    }
}
