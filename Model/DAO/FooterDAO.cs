using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class FooterDAO
    {
        MyHandsDbContext db = null;
        public FooterDAO()
        {
            db = new MyHandsDbContext();
        }
        public tbl_Footer GetFooterDB()
        {
            return db.tbl_Footer.SingleOrDefault(x=>x.bStatus == true);
        }
    }
}
