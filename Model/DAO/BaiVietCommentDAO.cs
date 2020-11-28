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
    public class BaiVietCommentDAO
    {
        MyHandsDbContext db = null;
        public BaiVietCommentDAO()
        {
            db = new MyHandsDbContext();
        }
        public List<tbl_BaiVietComment> ListAll(long idSanPham)
        {
            return db.tbl_BaiVietComment.Where(x => x.bStatus == true && x.IDBaiViet == idSanPham).OrderBy(x => x.dNgayTao).ToList();
        }
        public tbl_BaiVietComment getPersonByID(long id)
        {
            return db.tbl_BaiVietComment.Find(id);
        }
        public long InsertCMT(tbl_BaiVietComment cmt)
        {
            db.tbl_BaiVietComment.Add(cmt);
            db.SaveChanges();
            return cmt.IDComment;
        }
    }
}
