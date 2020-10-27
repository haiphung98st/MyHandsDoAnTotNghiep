using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class MenuDAO
    {

        MyHandsDbContext db = null;
        public MenuDAO()
        {
            db = new MyHandsDbContext();
        }
        public List<tbl_Menu> ListByGroupMenuId(int groupID)
        {
            return db.tbl_Menu.Where(x => x.iTypeID == groupID && x.bStatus==true).OrderBy(x=>x.iDisplayOrder).ToList();
        }
    }
}
