using Model.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    [Serializable]
    public class OptionCustomizeViewModel
    {
        public long IDoption { get; set; }
        public long IDDanhMuc { get; set; }
        [Display(Name ="Tên option")]
        public string sTenOption { get; set; }
        [Display(Name = "Giá trị")]
        public string sValueOption { get; set; }
    }
}
