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
    public class BaiVietDAO
    {

        MyHandsDbContext db = null;
        public BaiVietDAO()
        {
            db = new MyHandsDbContext();
        }
        public tbl_BaiViet GetBaiVietByID(long id)
        {
            return db.tbl_BaiViet.Find(id);
        }

        public tbl_BaiViet getByID(long id)
        {
            return db.tbl_BaiViet.SingleOrDefault(x => x.IDBaiViet == id);
        }
        public long Create(tbl_BaiViet content)
        {
            if (string.IsNullOrEmpty(content.sTenTieuDecMeta))
            {
                content.sTenTieuDecMeta = StringHelper.ToUnsignString(content.sTieuDe);
            }
            //content.sImages = "http://192.168.0.107:7500" + content.sImages;

            content.dNgayTao = DateTime.Now;
            content.iViewCount = 0;
            db.tbl_BaiViet.Add(content);
            db.SaveChanges();
            //tags
            //lay danh sach tag
            if (!string.IsNullOrEmpty(content.sTags))
            {
                string[] tags = content.sTags.Split(',');
                foreach(var tag in tags)
                {
                    var tagID = StringHelper.ToUnsignString(tag);
                    var existTag = this.CheckTag(tagID);
                    //them vao bang tag
                    if (!existTag)
                    {
                        this.InsertTag(tagID, tag);
                    }
                    //them vao bang tagBaiviet
                    this.InsertContentTag(content.IDBaiViet, tagID);
                }
            }

            return content.IDBaiViet;
        }

        public bool Edit(tbl_BaiViet content)
        {
            try
            {
                var addcontent = db.tbl_BaiViet.Find(content.IDBaiViet);
                addcontent.sTieuDe = content.sTieuDe;
                if (string.IsNullOrEmpty(content.sTenTieuDecMeta))
                {
                    content.sTenTieuDecMeta = StringHelper.ToUnsignString(content.sTieuDe);
                }
                //addcontent.sImages = "http://192.168.0.107:7500" + content.sImages;
                addcontent.sImages =  content.sImages;
                addcontent.IDChuDe = content.IDChuDe;
                addcontent.sXemTruoc = content.sXemTruoc;
                addcontent.sNoiDung = content.sNoiDung;
                addcontent.bStatus = content.bStatus;
                addcontent.sTags = content.sTags;
                addcontent.dNgaySua = DateTime.Now;
                db.SaveChanges();
                //tags
                //lay danh sach tag
                if (!string.IsNullOrEmpty(content.sTags))
                {
                    this.RemoveAllContentTag(content.IDBaiViet);
                    string[] tags = content.sTags.Split(',');
                    foreach (var tag in tags)
                    {
                        var tagID = StringHelper.ToUnsignString(tag);
                        var existTag = this.CheckTag(tagID);
                        //them vao bang tag
                        if (!existTag)
                        {
                            this.InsertTag(tagID, tag);
                        }
                        //them vao bang tagBaiviet
                        this.InsertContentTag(content.IDBaiViet, tagID);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void RemoveAllContentTag(long contentId)
        {
            db.tbl_TagBaiViet.RemoveRange(db.tbl_TagBaiViet.Where(x => x.IDBaiViet == contentId));
            db.SaveChanges();
        }
        public void InsertTag (string id, string name)
        {
            var tag = new tbl_Tag();
            tag.TagID = id;
            tag.sTenTag = name;
            db.tbl_Tag.Add(tag);
            db.SaveChanges();
        }
        public void InsertContentTag(long contentId, string tagId)
        {
            var contentTag = new tbl_TagBaiViet();
            contentTag.IDBaiViet = contentId;
            contentTag.TagID = tagId;
            db.tbl_TagBaiViet.Add(contentTag);
            db.SaveChanges();
        }
        public tbl_Tag getTag(string id)
        {
            return db.tbl_Tag.Find(id);
        }
        public bool CheckTag(string id)
        {
            return db.tbl_Tag.Count(x => x.TagID == id) > 0;
        }
        public IEnumerable<tbl_BaiViet> ListAllPaging(string SearchBaiViet, int page, int pageSize)
        {
            IQueryable<tbl_BaiViet> model = db.tbl_BaiViet;
            if (!string.IsNullOrEmpty(SearchBaiViet))
            {
                model = model.Where(x => x.sTieuDe.Contains(SearchBaiViet));
            }
            return model.OrderByDescending(x => x.IDBaiViet).ToPagedList(page, pageSize);
        }

        public IEnumerable<tbl_BaiViet> ListAllPaging( int page, int pageSize)
        {
            IQueryable<tbl_BaiViet> model = db.tbl_BaiViet;
            return model.Where(x=>x.bStatus == true).OrderByDescending(x => x.dNgayTao).ToPagedList(page, pageSize);
        }
        public List<tbl_BaiViet> listHomeContent(int top)
        {

            return db.tbl_BaiViet.Where(x => x.bStatus == true).OrderByDescending(x => x.dNgayTao).Take(top).ToList();
        }
        public IEnumerable<tbl_BaiViet> ListAllByTag(string tagid,int page, int pageSize)
        {
            var model = (from a in db.tbl_BaiViet
                                             join b in db.tbl_TagBaiViet
                                             on a.IDBaiViet equals b.IDBaiViet
                                             where b.TagID == tagid 
                                             select new
                                             {
                                                 TieuDe = a.sTieuDe,
                                                 TieuDeMeta = a.sTenTieuDecMeta,
                                                 Image = a.sImages,
                                                 XemTruoc = a.sXemTruoc,
                                                 NoiDung = a.sNoiDung,
                                                 NguoiDang = a.sNguoiTao,
                                                 NgayTao = a.dNgayTao,
                                                 ID = a.IDBaiViet

                                             }).AsEnumerable().Select(x=> new tbl_BaiViet()
                                             {
                                                 sTieuDe = x.TieuDe,
                                                 sTenTieuDecMeta = x.TieuDeMeta,
                                                 sImages = x.Image,
                                                 sXemTruoc = x.XemTruoc,
                                                 sNoiDung = x.NoiDung,
                                                 sNguoiTao = x.NguoiDang,
                                                 dNgayTao = x.NgayTao,
                                                 IDBaiViet = x.ID
                                             });
            return model.OrderByDescending(x => x.dNgayTao).ToPagedList(page, pageSize);
        }

        public IEnumerable<tbl_BaiViet> ListAllByUser(string username, int page, int pageSize)
        {
            var model = (from a in db.tbl_BaiViet
                         join b in db.tbl_TaiKhoan
                         on a.sNguoiTao equals b.sTenTaiKhoan
                         where b.sTenTaiKhoan == username
                         select new
                         {
                             TieuDe = a.sTieuDe,
                             TieuDeMeta = a.sTenTieuDecMeta,
                             Image = a.sImages,
                             XemTruoc = a.sXemTruoc,
                             NoiDung = a.sNoiDung,
                             NguoiDang = a.sNguoiTao,
                             NgayTao = a.dNgayTao,
                             ID = a.IDBaiViet

                         }).AsEnumerable().Select(x => new tbl_BaiViet()
                         {
                             sTieuDe = x.TieuDe,
                             sTenTieuDecMeta = x.TieuDeMeta,
                             sImages = x.Image,
                             sXemTruoc = x.XemTruoc,
                             sNoiDung = x.NoiDung,
                             sNguoiTao = x.NguoiDang,
                             dNgayTao = x.NgayTao,
                             IDBaiViet = x.ID
                         });
            return model.OrderByDescending(x => x.dNgayTao).ToPagedList(page, pageSize);
        }
        public List<tbl_Tag> ListTags(long id)
        {
            var model = (from a in db.tbl_Tag
                         join b in db.tbl_TagBaiViet
                         on a.TagID equals b.TagID
                         where b.IDBaiViet == id
                         select new
                         {
                             TagID = b.TagID,
                             sTenTag = a.TagID
                         }).AsEnumerable().Select(x => new tbl_Tag()
                         {
                             TagID = x.TagID,
                             sTenTag = x.sTenTag
                         });
            return model.ToList();

        }
        public int CountContent()
        {
            return db.tbl_BaiViet.Count(x=>x.bStatus == true);
        }
        public bool Delete(int id)
        {
            try
            {
                var baiviet = db.tbl_BaiViet.Find(id);
                db.tbl_BaiViet.Remove(baiviet);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool ChangeStatusBaiViet(long id)
        {
            var baiviet = db.tbl_BaiViet.Find(id);
            baiviet.bStatus = !baiviet.bStatus;
            db.SaveChanges();
            return !baiviet.bStatus;
        }
    }
}
