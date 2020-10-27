using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ChiTietHoaDonDAO
    {
        MyHandsDbContext db = null;
        public ChiTietHoaDonDAO()
        {
            db = new MyHandsDbContext();
        }
        public bool Insert(tbl_ChiTietHoaDon detail)
        {
            try
            {
                db.tbl_ChiTietHoaDon.Add(detail);
                db.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
            
        }
    }
}
