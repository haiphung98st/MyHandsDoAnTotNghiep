namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Serializable]
    public partial class tbl_Permission
    {
        [Key]
        [StringLength(50)]
        public string PhanQuyenID { get; set; }
        
        [StringLength(50)]
        public string RoleID { get; set; }
    }
}
