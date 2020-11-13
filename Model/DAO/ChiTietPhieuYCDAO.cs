using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ChiTietPhieuYCDAO
    {
        MyHandsDbContext db = null;
        public ChiTietPhieuYCDAO()
        {
            db = new MyHandsDbContext();
        }
        public bool Insert(tbl_ChiTietPhieuYC detail)
        {
            try
            {
                db.tbl_ChiTietPhieuYC.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
