namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Tag
    {
        [Key]
        [StringLength(50)]
        public string TagID { get; set; }

        [StringLength(50)]
        public string sTenTag { get; set; }
    }
}
