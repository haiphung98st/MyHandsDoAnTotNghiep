using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Common;

namespace Model.DAO
{
    public class UserDao
    {
        MyHandsDbContext db = null;
        public UserDao()
        {
            db = new MyHandsDbContext();
        }
        public long Insert(tbl_TaiKhoan entity)

        {
            db.tbl_TaiKhoan.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }


        public long InsertForFacebook(tbl_TaiKhoan entity)
        {
            var user = db.tbl_TaiKhoan.SingleOrDefault(x => x.sTenTaiKhoan == entity.sTenTaiKhoan);
            if (user == null)
            {
                db.tbl_TaiKhoan.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            else
            {
                return user.ID;
            }

        }


        public bool Update(tbl_TaiKhoan entity)
        {
            try {
                var user = db.tbl_TaiKhoan.Find(entity.ID);
                user.sHoTen = entity.sHoTen;
                if (!string.IsNullOrEmpty(entity.sMatKhau))
                {
                    user.sMatKhau = entity.sMatKhau;
                }
                user.sDiaChi = entity.sDiaChi;
                user.sEmail = entity.sEmail;
                user.sSDT = entity.sSDT;
                user.sNguoiSua = entity.sNguoiSua;
                user.dNgaySua = DateTime.Now;
                user.bStatus = entity.bStatus;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
        public IEnumerable<tbl_TaiKhoan> ListAllPaging(string SearchUser, int page,int pageSize)
        {
           IQueryable<tbl_TaiKhoan> model = db.tbl_TaiKhoan;
            if (!string.IsNullOrEmpty(SearchUser))
            {
                model = model.Where(x => x.sTenTaiKhoan.Contains(SearchUser) || x.sHoTen.Contains(SearchUser));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }

        public tbl_TaiKhoan getByID(string userName)
        {
            return db.tbl_TaiKhoan.SingleOrDefault(x => x.sTenTaiKhoan == userName);
        }
        public tbl_TaiKhoan viewDetailByID(int id)
        {
            return db.tbl_TaiKhoan.Find(id);
        }

        public List<string> GetListCredential(string userName)
        {
            var user = db.tbl_TaiKhoan.Single(x => x.sTenTaiKhoan == userName);
            var data = (from a in db.tbl_Permissions
                        join b in db.tbl_PhanQuyens on a.PhanQuyenID equals b.ID
                        join c in db.tbl_Roles on a.RoleID equals c.ID
                        where b.ID == user.sQuyen
                        select new
                        {
                            RoleID = a.RoleID,
                            PhanQuyenID = a.PhanQuyenID
                        }).AsEnumerable().Select(x => new tbl_Permission()
                        {
                            RoleID = x.RoleID,
                            PhanQuyenID = x.PhanQuyenID
                        });
            return data.Select(x => x.RoleID).ToList();

        }

        public int Login(String userName, String password,bool isAdmin = false)
        {

            var result = db.tbl_TaiKhoan.SingleOrDefault(x => x.sTenTaiKhoan == userName );
            if(result == null)
            {
                return 0; //tài khoản ko tồn tại
            }
            else
            {
                if(isAdmin == true )
                {
                    if((result.sQuyen == CommonConstants.ADMIN_GROUP || result.sQuyen == CommonConstants.MOD_GROUP))
                    {
                        if (result.bStatus == false)
                        {
                            return -1; //tk bị khóa
                        }
                        else
                        {
                            if (result.sMatKhau == password)
                            {
                                return 1;//đăng nhập thành công
                            }
                            else
                            {
                                return -2; //tk hoặc mk ko đúng
                            }
                        }
                    }
                    else
                    {
                        return -3;
                    }
                    
                }
                else
                {
                    if (result.bStatus == false)
                    {
                        return -1; //tk bị khóa
                    }
                    else
                    {
                        if (result.sMatKhau == password)
                        {
                            return 1;//đăng nhập thành công
                        }
                        else
                        {
                            return -2; //tk hoặc mk ko đúng
                        }
                    }
                }
                
            }
        }

        public int ChangePassword(String username,String oldpass)
        {

           var result = db.tbl_TaiKhoan.SingleOrDefault(x => x.sTenTaiKhoan == username);
           if (result.sMatKhau == oldpass)
           {
            return 1;//đổi mk thành công
           }
           else
           {
            return 0; //k ko đúng
           }
                   
        }
        public bool Delete(int id)
        {
            try{
                 var user = db.tbl_TaiKhoan.Find(id);
                 db.tbl_TaiKhoan.Remove(user);
                 db.SaveChanges();
                 return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }
        public bool ChangeStatusUser(long id)
        {
            var user = db.tbl_TaiKhoan.Find(id);
            user.bStatus = !user.bStatus;
            db.SaveChanges();
            return !user.bStatus;
        }
        public bool checkExistUsername(string username)
        {
            return db.tbl_TaiKhoan.Count(x => x.sTenTaiKhoan == username) > 0;
        }
        public bool checkExistUserEmail(string email)
        {
            return db.tbl_TaiKhoan.Count(x => x.sEmail == email) > 0;
        }
        public bool checkExistUserPhone(string phone)
        {
            return db.tbl_TaiKhoan.Count(x => x.sSDT == phone) > 0;
        }
    }
}
