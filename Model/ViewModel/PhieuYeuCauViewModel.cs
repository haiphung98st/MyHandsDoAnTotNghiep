using Model.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    [Serializable]

    public class PhieuYeuCauViewModel
    {
        public tbl_SanPham sanPham { get; set; }
        public tbl_NCC ncc { get; set; }
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IDPhieuYeuCau { get; set; }
        [Key]
        [Column(Order = 1)]

        public long IDncc { get; set; }
        [Key]
        [Column(Order = 2)]

        public long IDSanPham { get; set; }
        public string sTenNCC { get; set; }
        public string sTenSanPham { get; set; }
        public int iSoLuongYC { get; set; }
    }
}
