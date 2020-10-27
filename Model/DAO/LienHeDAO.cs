using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class LienHeDAO
    {
        MyHandsDbContext db = null;
        public LienHeDAO()
        {
            db = new MyHandsDbContext();
        }
        public tbl_LienHe getThongTinLienHe()
        {
            return db.tbl_LienHe.Single(x=>x.sStatus==true);
        }
        public long InsertFeedBack(tbl_Feedback fb)
        {
            db.tbl_Feedback.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }
    }
}
