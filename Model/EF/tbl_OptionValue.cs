namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_OptionValue
    {
        [Key]
        public long ID { get; set; }
        public long IDoption { get; set; }
        [StringLength(250)]
        public string ValueOption { get; set; }
    }
}
