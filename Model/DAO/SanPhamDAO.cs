using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.ViewModel;
using System.Threading.Tasks;
using PagedList;
using Common;

namespace Model.DAO
{
    public class SanPhamDAO
    {
        MyHandsDbContext db = null;
        public SanPhamDAO()
        {
            db = new MyHandsDbContext();
        }
        public int CountProduct()
        {
            return db.tbl_SanPham.Count(x=>x.bStatus == true);
        }
        public List<string> ListName(string keyword)
        {
            return db.tbl_SanPham.Where(x => x.sTenSanPham.Contains(keyword) && x.bStatus==true).Select(x => x.sTenSanPham).ToList();
        }
        public List<tbl_SanPham> ListAll()
        {
            return db.tbl_SanPham.Where(x => x.bStatus == true).OrderBy(x => x.dNgayTao).ToList();
        }
        public tbl_SanPham GetSanPhamByID(long id)
        {
            return db.tbl_SanPham.Find(id);
        }
        public List<tbl_SanPham> ListProductNewArrivals(int top )
        {
            return db.tbl_SanPham.Where(x=>x.bStatus == true).OrderByDescending(x => x.dNgayTao).Take(top).ToList();
        }
        public List<tbl_SanPham> SortProduct(string strSort, ref int totalRecord, int page = 1, int pageSize = 1)
        {
            totalRecord = db.tbl_SanPham.Where(x => x.bStatus == true).Count();

            if (!string.IsNullOrEmpty(strSort))
            {
                if (strSort == "opt_sortnew")
                {
                    return db.tbl_SanPham.Where(x => x.bStatus == true).OrderByDescending(x => x.dNgayTao).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                }
                else if (strSort == "opt_bestsell")
                {
                    return db.tbl_SanPham.Where(x => x.bStatus == true).OrderByDescending(x => x.dNgayTao).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                }
                else if (strSort == "opt_priceASC")
                {
                    return db.tbl_SanPham.Where(x => x.bStatus == true).OrderBy(x => x.dGiaBan).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                }
                else if (strSort == "opt_priceDESC")
                {
                    return db.tbl_SanPham.Where(x => x.bStatus == true).OrderByDescending(x => x.dGiaBan).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    return db.tbl_SanPham.Where(x => x.bStatus == true).OrderBy(x => x.dNgayTao).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            else
            {
                return db.tbl_SanPham.Where(x => x.bStatus == true).OrderByDescending(x => x.dGiaBan).Skip((page - 1) * pageSize).Take(pageSize).ToList();


                //return db.tbl_SanPham.Where(x => x.bStatus == true).OrderBy(x => x.dNgayTao).ToList();
            }
        }
        public List<tbl_SanPham> ListProductBestSeller(int top)
        {
            return db.tbl_SanPham.Where(x=>x.dTopHot!=null && x.dTopHot > DateTime.Now && x.bStatus == true).OrderByDescending(x => x.dNgayTao).Take(top).ToList();
        }
        public List<tbl_SanPham> ListByCustomActive(int id)
        {
            return db.tbl_SanPham.Where(x => x.IDDanhMuc == id && x.bStatus == true && x.bCustomActive == true).OrderBy(x => x.ID).ToList();
        }
        public List<ProductViewModel> ListProductByCategoryID(int categoryid, ref int totalRecord, int page=1, int pageSize=1)
        {
            totalRecord = db.tbl_SanPham.Where(x => x.IDDanhMuc == categoryid && x.bStatus == true).Count();
            //var model = db.tbl_SanPham.Where(x => x.IDDanhMuc == categoryid).OrderByDescending(x => x.dNgayTao).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            //lay tong cac san pham trong danh muc. Skip-lay tu ban ghi 
            //return model;

            var model = (from a in db.tbl_SanPham
                         join b in db.tbl_DanhMucSanPham
                         on a.IDDanhMuc equals b.IDDanhMuc
                         where a.IDDanhMuc == categoryid
                         select new 
                         {
                             iSoluong = a.iSoLuong,
                             sTenDanhMucMeta = b.sTenDanhMucMeta,
                             sTenDanhMuc = b.sTenDanhMuc,
                             dNgayTao = a.dNgayTao,
                             ID = a.ID,
                             sImages = a.sImages,
                             sTenSanPham = a.sTenSanPham,
                             sTenSanPhamMeta = a.sTenSanPhamMeta,
                             dGiaBan = a.dGiaBan,
                             dGiaKhuyenMai = a.dGiaKhuyenMai
                         })
                         .AsEnumerable().Select(x => new ProductViewModel()
                          {
                              iSoluong = x.iSoluong,
                              sTenDanhMucMeta = x.sTenDanhMucMeta,
                              sTenDanhMuc = x.sTenDanhMuc,
                              dNgayTao = x.dNgayTao,
                              ID = x.ID,
                              sImages = x.sImages,
                              sTenSanPham = x.sTenSanPham,
                              sTenSanPhamMeta = x.sTenSanPhamMeta,
                              dGiaBan = x.dGiaBan,
                              dGiaKhuyenMai = x.dGiaKhuyenMai
                          });
            model.OrderByDescending(x => x.dNgayTao).Skip((page - 1) * pageSize).Take(pageSize);
            return model.ToList();

        }
        public tbl_SanPham chiTietSanPham(int id)
        {
            return db.tbl_SanPham.Find(id);
        }
        public bool divSanPham(long id,int soluong)
        {
            try
            {
                var sanpham = db.tbl_SanPham.Find(id);
                if(sanpham.iSoLuong >= soluong)
                {
                    sanpham.iSoLuong = sanpham.iSoLuong - soluong;
                    db.SaveChanges();
                    return true;
                }
                else { return false; }
                
            }
           
            catch(Exception ex)
            {
                return false;
            }

        }
        public bool addquantitySanPham(long id, int soluong)
        {
            try
            {
                var sanpham = db.tbl_SanPham.Find(id);
                sanpham.iSoLuong = sanpham.iSoLuong + soluong;
                db.SaveChanges();
                return true;

            }

            catch (Exception ex)
            {
                return false;
            }

        }
        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int page = 1, int pageSize = 2)
        {
            totalRecord = db.tbl_SanPham.Where(x => x.sTenSanPham.Contains(keyword) && x.bStatus == true).Count();
            //var model = db.tbl_SanPham.Where(x => x.IDDanhMuc == categoryid).OrderByDescending(x => x.dNgayTao).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            //lay tong cac san pham trong danh muc. Skip-lay tu ban ghi 
            //return model;

            var model = (from a in db.tbl_SanPham
                         join b in db.tbl_DanhMucSanPham
                         on a.IDDanhMuc equals b.IDDanhMuc
                         where a.sTenSanPham.Contains(keyword)
                         select new
                         {
                             iSoluong = a.iSoLuong,

                             sTenDanhMucMeta = b.sTenDanhMucMeta,
                             sTenDanhMuc = b.sTenDanhMuc,
                             dNgayTao = a.dNgayTao,
                             ID = a.ID,
                             sImages = a.sImages,
                             sTenSanPham = a.sTenSanPham,
                             sTenSanPhamMeta = a.sTenSanPhamMeta,
                             dGiaBan = a.dGiaBan,
                             dGiaKhuyenMai = a.dGiaKhuyenMai
                         })
                         .AsEnumerable().Select(x => new ProductViewModel()
                         {
                             iSoluong = x.iSoluong,

                             sTenDanhMucMeta = x.sTenDanhMucMeta,
                             sTenDanhMuc = x.sTenDanhMuc,
                             dNgayTao = x.dNgayTao,
                             ID = x.ID,
                             sImages = x.sImages,
                             sTenSanPham = x.sTenSanPham,
                             sTenSanPhamMeta = x.sTenSanPhamMeta,
                             dGiaBan = x.dGiaBan,
                             dGiaKhuyenMai = x.dGiaKhuyenMai
                         });
            model.OrderByDescending(x => x.dNgayTao).Skip((page - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }

        public IEnumerable<tbl_SanPham> ListAllPaging(string SearchSanPham, int page, int pageSize)
        {
            IQueryable<tbl_SanPham> model = db.tbl_SanPham;
            if (!string.IsNullOrEmpty(SearchSanPham))
            {
                model = model.Where(x => x.sTenSanPham.Contains(SearchSanPham));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }

        public IEnumerable<tbl_SanPham> ListAllPaging(int page, int pageSize)
        {
            IQueryable<tbl_SanPham> model = db.tbl_SanPham;
            return model.Where(x => x.bStatus == true).OrderByDescending(x => x.dNgayTao).ToPagedList(page, pageSize);
        }
        public long Create(tbl_SanPham sanpham)
        {
            if (string.IsNullOrEmpty(sanpham.sTenSanPhamMeta))
            {
                sanpham.sTenSanPhamMeta = StringHelper.ToUnsignString(sanpham.sTenSanPham);
            }
            //sanpham.sImages = "http://192.168.0.107:7500" + sanpham.sImages;
            sanpham.sMaSanPham = sanpham.IDDanhMuc+ " " + sanpham.ID;
            sanpham.dNgayTao = DateTime.Now;
            sanpham.iViewCount = 0;
            db.tbl_SanPham.Add(sanpham);
            db.SaveChanges();
           

            return sanpham.ID;
        }
        public bool Delete(int id)
        {
            try
            {
                var sanpham = db.tbl_SanPham.Find(id);
                db.tbl_SanPham.Remove(sanpham);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool ChangeStatusSanPham(long id)
        {
            var sanpham = db.tbl_SanPham.Find(id);
            sanpham.bStatus = !sanpham.bStatus;
            db.SaveChanges();
            return !sanpham.bStatus;
        }

        public bool Update(tbl_SanPham entity)
        {
            try
            {
                var sanpham = db.tbl_SanPham.Find(entity.ID);
                sanpham.sTenSanPham = entity.sTenSanPham;
                if (string.IsNullOrEmpty(sanpham.sTenSanPhamMeta))
                {
                    sanpham.sTenSanPhamMeta = StringHelper.ToUnsignString(entity.sTenSanPham);
                }
                sanpham.sImages = entity.sImages;

                sanpham.dGiaBan = entity.dGiaBan;
                sanpham.IDDanhMuc = entity.IDDanhMuc;
                sanpham.sMaSanPham = sanpham.IDDanhMuc + " " + sanpham.ID;

                sanpham.dGiaKhuyenMai = entity.dGiaKhuyenMai;
                sanpham.bStatus = entity.bStatus;
                sanpham.dNgaySua = DateTime.Now;
                
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<OptionCustomizeViewModel> ViewOptionByIDdanhmuc(int id)
        {
            var model = (from a in db.tbl_DanhMucSanPham
                         join b in db.tbl_SPmauOption on a.IDDanhMuc equals b.IDDanhMucMauSP
                         join c in db.tbl_OptionSPMau on b.IDOption equals c.ID
                         join d in db.tbl_OptionValue on c.ID equals d.IDoption
                         where a.IDDanhMuc == id
                         select new
                         {
                             IDDanhMuc = a.IDDanhMuc,
                             sTenOption = c.sTenOption,
                             sValueOption = d.ValueOption,
                             IDoption = c.ID,
                         })
                         .AsEnumerable().Select(x => new OptionCustomizeViewModel()
                         {
                             IDoption =x.IDoption,
                             IDDanhMuc = x.IDDanhMuc,
                             sTenOption = x.sTenOption,
                             sValueOption = x.sValueOption,
                         });
            model.OrderByDescending(x => x.sTenOption);
            return model.ToList();
        }
        public List<OptionCustomizeViewModel> getNameoptionByID(int id)
        {
            var model = (from a in db.tbl_DanhMucSanPham
                         join b in db.tbl_SPmauOption on a.IDDanhMuc equals b.IDDanhMucMauSP
                         join c in db.tbl_OptionSPMau on b.IDOption equals c.ID
                         where a.IDDanhMuc == id
                         select new
                         {
                             IDDanhMuc = a.IDDanhMuc,
                             sTenOption = c.sTenOption,
                             IDoption = c.ID,
                         })
                         .AsEnumerable().Select(x => new OptionCustomizeViewModel()
                         {
                             IDoption = x.IDoption,
                             IDDanhMuc = x.IDDanhMuc,
                             sTenOption = x.sTenOption,
                         });
            model.OrderByDescending(x => x.sTenOption);
            return model.ToList();
        }
        public int getQuantityByID(long id) {
            var sanpham = db.tbl_SanPham.Find(id);
            return sanpham.iSoLuong;
        }

    }
}
