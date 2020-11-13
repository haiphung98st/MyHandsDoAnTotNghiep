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
        public tbl_ChuDe viewDetail(long id)
        {
            return db.tbl_ChuDe.Find(id);
        }
        public IEnumerable<tbl_ChuDe> ListAllPaging(string SearchDanhMuc, int page, int pageSize)
        {
            IQueryable<tbl_ChuDe> model = db.tbl_ChuDe;
            if (!string.IsNullOrEmpty(SearchDanhMuc))
            {
                model = model.Where(x => x.sTenChuDe.Contains(SearchDanhMuc));
            }
            return model.OrderByDescending(x => x.IDChuDe).ToPagedList(page, pageSize);
        }
        public bool Update(tbl_ChuDe entity)
        {
            try
            {
                var chude = db.tbl_ChuDe.Find(entity.IDChuDe);
                chude.sTenChuDe = entity.sTenChuDe;
                if (string.IsNullOrEmpty(chude.sTenChuDeMeta))
                {
                    chude.sTenChuDeMeta = StringHelper.ToUnsignString(entity.sTenChuDe);
                }
                chude.sImage = entity.sImage;

                chude.bStatus = entity.bStatus;
                chude.dNgaySua = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public long Create(tbl_ChuDe chude)
        {
            if (string.IsNullOrEmpty(chude.sTenChuDe))
            {
                chude.sTenChuDeMeta = StringHelper.ToUnsignString(chude.sTenChuDe);
            }
            chude.dNgayTao = DateTime.Now;
            db.tbl_ChuDe.Add(chude);
            db.SaveChanges();


            return chude.IDChuDe;
        }

        public bool Delete(int id)
        {
            try
            {
                var chude = db.tbl_ChuDe.Find(id);
                db.tbl_ChuDe.Remove(chude);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool ChangeStatusDanhMuc(long id)
        {
            var dmsanpham = db.tbl_ChuDe.Find(id);
            dmsanpham.bStatus = !dmsanpham.bStatus;
            db.SaveChanges();
            return !dmsanpham.bStatus;
        }
    }
}
