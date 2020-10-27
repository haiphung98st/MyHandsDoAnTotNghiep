using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class HoaDonDAO
    {
        MyHandsDbContext db = null;
        public HoaDonDAO()
        {
            db = new MyHandsDbContext();
        }
        public long Insert (tbl_HoaDon order)
        {
            db.tbl_HoaDon.Add(order);
            db.SaveChanges();
            return order.ID;
        }
    }
}
