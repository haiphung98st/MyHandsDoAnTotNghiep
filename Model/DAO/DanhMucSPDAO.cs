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
    public class DanhMucSPDAO
    {
        MyHandsDbContext db = null;
        public DanhMucSPDAO()
        {
            db = new MyHandsDbContext();
        }
        public List<tbl_DanhMucSanPham> ListAll()
        {
            return db.tbl_DanhMucSanPham.Where(x => x.bStatus == true).OrderBy(x => x.iDisplayOrder).ToList();
        }
        public tbl_DanhMucSanPham viewByDanhMuc(int id)
        {
            return db.tbl_DanhMucSanPham.Find(id);
        }
        public tbl_DanhMucSanPham viewDetail(long id)
        {
            return db.tbl_DanhMucSanPham.Find(id);
        }
        public IEnumerable<tbl_DanhMucSanPham> ListAllPaging(string SearchDanhMuc, int page, int pageSize)
        {
            IQueryable<tbl_DanhMucSanPham> model = db.tbl_DanhMucSanPham;
            if (!string.IsNullOrEmpty(SearchDanhMuc))
            {
                model = model.Where(x => x.sTenDanhMuc.Contains(SearchDanhMuc));
            }
            return model.OrderByDescending(x => x.IDDanhMuc).ToPagedList(page, pageSize);
        }

        public bool Update(tbl_DanhMucSanPham entity)
        {
            try
            {
                var danhMucSanPham = db.tbl_DanhMucSanPham.Find(entity.IDDanhMuc);
                danhMucSanPham.sTenDanhMuc = entity.sTenDanhMuc;
                if (string.IsNullOrEmpty(danhMucSanPham.sTenDanhMucMeta))
                {
                    danhMucSanPham.sTenDanhMucMeta = StringHelper.ToUnsignString(entity.sTenDanhMuc);
                }
                danhMucSanPham.sHinhAnhDanhMuc = entity.sHinhAnhDanhMuc;

                danhMucSanPham.bStatus = entity.bStatus;
                danhMucSanPham.dNgaySua = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public long Create(tbl_DanhMucSanPham danhMucSanPham)
        {
            if (string.IsNullOrEmpty(danhMucSanPham.sTenDanhMuc))
            {
                danhMucSanPham.sTenDanhMucMeta = StringHelper.ToUnsignString(danhMucSanPham.sTenDanhMuc);
            }
            danhMucSanPham.dNgayTao = DateTime.Now;
            db.tbl_DanhMucSanPham.Add(danhMucSanPham);
            db.SaveChanges();


            return danhMucSanPham.IDDanhMuc;
        }

        public bool Delete(int id)
        {
            try
            {
                var dmsanpham = db.tbl_DanhMucSanPham.Find(id);
                db.tbl_DanhMucSanPham.Remove(dmsanpham);
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
            var dmsanpham = db.tbl_DanhMucSanPham.Find(id);
            dmsanpham.bStatus = !dmsanpham.bStatus;
            db.SaveChanges();
            return !dmsanpham.bStatus;
        }
    }
}
