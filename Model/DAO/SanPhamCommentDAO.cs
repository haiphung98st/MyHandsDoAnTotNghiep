using Common;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class SanPhamCommentDAO
    {
        MyHandsDbContext db = null;
        public SanPhamCommentDAO()
        {
            db = new MyHandsDbContext();
        }
        public List<tbl_SanPhamComment> ListAll(long idSanPham)
        {
            return db.tbl_SanPhamComment.Where(x => x.bStatus == true && x.IDSanPham == idSanPham).OrderBy(x => x.dNgayTao).ToList();
        }
        public tbl_SanPhamComment getPersonByID(long id)
        {
            return db.tbl_SanPhamComment.Find(id);
        }
        public long InsertCMT(tbl_SanPhamComment cmt)
        {
            db.tbl_SanPhamComment.Add(cmt);
            db.SaveChanges();
            return cmt.IDComment;
        }
    }
}
