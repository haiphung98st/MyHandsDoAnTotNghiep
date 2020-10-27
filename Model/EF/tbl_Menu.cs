namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Menu
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string sText { get; set; }

        [StringLength(250)]
        public string sLink { get; set; }

        public int? iDisplayOrder { get; set; }

        [StringLength(250)]
        public string sTarget { get; set; }

        public bool? bStatus { get; set; }

        public int? iTypeID { get; set; }
    }
}
