using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class DanhMucBaiVietDAO
    {

        MyHandsDbContext db = null;
        public DanhMucBaiVietDAO()
        {
            db = new MyHandsDbContext();
        }
        public List<tbl_ChuDe> ListChude()
        {
            return db.tbl_ChuDe.Where(x=>x.bStatus == true).ToList();
        }
    }
}
