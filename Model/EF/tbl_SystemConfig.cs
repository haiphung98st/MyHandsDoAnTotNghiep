namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SystemConfig
    {
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string sName { get; set; }

        [StringLength(50)]
        public string sType { get; set; }

        [StringLength(50)]
        public string sValue { get; set; }

        public bool? bStatus { get; set; }
    }
}
