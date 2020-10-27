namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_LienHe
    {
        [Key]
        public long IDLienHe { get; set; }

        [Column(TypeName = "ntext")]
        public string sNoiDung { get; set; }

        public bool? sStatus { get; set; }
    }
}
