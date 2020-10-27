﻿using Model.EF;
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
        public List<string> ListName(string keyword)
        {
            return db.tbl_SanPham.Where(x => x.sTenSanPham.Contains(keyword)).Select(x => x.sTenSanPham).ToList();
        }
        public tbl_SanPham GetSanPhamByID(long id)
        {
            return db.tbl_SanPham.Find(id);
        }
        public List<tbl_SanPham> ListProductNewArrivals(int top )
        {
            return db.tbl_SanPham.OrderByDescending(x => x.dNgayTao).Take(top).ToList();
        }
        public List<tbl_SanPham> ListProductBestSeller(int top)
        {
            return db.tbl_SanPham.Where(x=>x.dTopHot!=null && x.dTopHot > DateTime.Now).OrderByDescending(x => x.dNgayTao).Take(top).ToList();
        }
        public List<ProductViewModel> ListProductByCategoryID(int categoryid, ref int totalRecord, int page=1, int pageSize=1)
        {
            totalRecord = db.tbl_SanPham.Where(x => x.IDDanhMuc == categoryid).Count();
            //var model = db.tbl_SanPham.Where(x => x.IDDanhMuc == categoryid).OrderByDescending(x => x.dNgayTao).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            //lay tong cac san pham trong danh muc. Skip-lay tu ban ghi 
            //return model;

            var model = (from a in db.tbl_SanPham
                         join b in db.tbl_DanhMucSanPham
                         on a.IDDanhMuc equals b.IDDanhMuc
                         where a.IDDanhMuc == categoryid
                         select new 
                         {
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
        
        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int page = 1, int pageSize = 2)
        {
            totalRecord = db.tbl_SanPham.Where(x => x.sTenSanPham.Contains(keyword)).Count();
            //var model = db.tbl_SanPham.Where(x => x.IDDanhMuc == categoryid).OrderByDescending(x => x.dNgayTao).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            //lay tong cac san pham trong danh muc. Skip-lay tu ban ghi 
            //return model;

            var model = (from a in db.tbl_SanPham
                         join b in db.tbl_DanhMucSanPham
                         on a.IDDanhMuc equals b.IDDanhMuc
                         where a.sTenSanPham.Contains(keyword)
                         select new
                         {
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
            return model.OrderByDescending(x => x.dNgayTao).ToPagedList(page, pageSize);
        }
        public long Create(tbl_SanPham sanpham)
        {
            if (string.IsNullOrEmpty(sanpham.sTenSanPhamMeta))
            {
                sanpham.sTenSanPhamMeta = StringHelper.ToUnsignString(sanpham.sTenSanPham);
            }
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
                sanpham.sMaSanPham = entity.sMaSanPham;
                sanpham.dGiaBan = entity.dGiaBan;
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
    }
}
