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
    public class NotificationDAO
    {
        MyHandsDbContext db = null;
        public NotificationDAO()
        {
            db = new MyHandsDbContext();
        }
        public IEnumerable<tbl_Notification> ListAllPaging(string SearchNotification, int page, int pageSize)
        {
            IQueryable<tbl_Notification> model = db.tbl_Notification;
            if (!string.IsNullOrEmpty(SearchNotification))
            {
                model = model.Where(x => x.sTittle.Contains(SearchNotification));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public long Create(tbl_Notification notification)
        {

            notification.dNgayTB = DateTime.Now;
            db.tbl_Notification.Add(notification);
            db.SaveChanges();
            return notification.ID;
        }

        public bool Edit(tbl_Notification notification)
        {
            try
            {
                var addNoti = db.tbl_Notification.Find(notification.ID);
                addNoti.sTittle = notification.sTittle;
                addNoti.sHinhAnh = notification.sHinhAnh;
                addNoti.IDkhachhang = notification.IDkhachhang;
                addNoti.sChiTiet = notification.sChiTiet;
                addNoti.bApplyForAll = notification.bApplyForAll;
                addNoti.dNgayTB = DateTime.Now;
                db.SaveChanges();
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public tbl_Notification GetNotificationByID(long id)
        {
            return db.tbl_Notification.Find(id);
        }
    }
}
