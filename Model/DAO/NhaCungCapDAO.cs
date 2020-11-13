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
    public class NhaCungCapDAO
    {
        MyHandsDbContext db = null;
        public NhaCungCapDAO()
        {
            db = new MyHandsDbContext();
        }
        public List<tbl_NCC> ListAll()
        {
            return db.tbl_NCC.Where(x => x.bStatus == true).OrderBy(x => x.dNgayTao).ToList();
        }
        public IEnumerable<tbl_NCC> ListAllPaging(string SearchNCC, int page, int pageSize)
        {
            IQueryable<tbl_NCC> model = db.tbl_NCC;
            if (!string.IsNullOrEmpty(SearchNCC))
            {
                model = model.Where(x => x.sTenNCC.Contains(SearchNCC));
            }
            return model.OrderBy(x => x.IDncc).ToPagedList(page, pageSize);
        }
        public long Create(tbl_NCC ncc)
        {


            ncc.dNgayTao = DateTime.Now;
            db.tbl_NCC.Add(ncc);
            db.SaveChanges();
            //tags
            //lay danh sach tag
            

            return ncc.IDncc;
        }
        public tbl_NCC GetByID(long id)
        {
            return db.tbl_NCC.Find(id);
        }
        public bool Edit(tbl_NCC ncc)
        {
            try
            {
                var addcontent = db.tbl_NCC.Find(ncc.IDncc);
                addcontent.sTenNCC = ncc.sTenNCC;
                addcontent.sEmail = ncc.sEmail;
                addcontent.sSDT = ncc.sSDT;
                addcontent.sDiaChi = ncc.sDiaChi;
                addcontent.bStatus = ncc.bStatus;
                db.SaveChanges();
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool ChangeStatus(long id)
        {
            var ncc = db.tbl_NCC.Find(id);
            ncc.bStatus = !ncc.bStatus;
            db.SaveChanges();
            return !ncc.bStatus;
        }
    }
}
