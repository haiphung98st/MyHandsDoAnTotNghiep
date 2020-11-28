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
        [Display(Name ="Khách hàng")]
        public long?  IDkhachhang { get; set; }
        [Display(Name = "Hình ảnh")]
        [Required(ErrorMessage = "Hãy chọn hình ảnh")]

        [StringLength(250)]
        public string sHinhAnh { get; set; }
        [Display(Name = "Chi tiết")]

        [StringLength(250)]
        [Required(ErrorMessage = "Hãy nhập chi tiết")]

        public string sChiTiet { get; set; }
        [StringLength(250)]
        [Display(Name = "Tiêu đề")]

        public string sTittle { get; set; }
        [Required(ErrorMessage = "Hãy nhập ngày thông báo")]

        [Display(Name = "Ngày thông báo")]

        public DateTime? dNgayTB { get; set; }
        [Display(Name = "Gửi cho tất cả")]

        public bool bApplyForAll { get; set; }
    }
}
