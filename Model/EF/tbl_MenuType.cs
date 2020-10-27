namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_MenuType
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string sName { get; set; }
    }
}
