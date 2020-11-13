using Microsoft.AspNet.SignalR;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyHandsDoAnTotNghiep.HUB
{
    public class NotificationComponents
    {
        public void RegisterNotification(DateTime currentTime)
        {
            string cnntr = ConfigurationManager.ConnectionStrings["MyHandsDbContext"].ConnectionString;
            string sqlCommand = @"SELECT [sChiTiet],[sTittle] from [dbo].[tbl_Notification] where [dNgayTB] > @AddedOn";
            using (SqlConnection con = new SqlConnection(cnntr))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, con);
                cmd.Parameters.AddWithValue("@AddedOn", currentTime);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.Notification = null;
                SqlDependency sqlDep = new SqlDependency(cmd);
                sqlDep.OnChange += sqlDep_OnChange;
                //we must have to execute the command here
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // nothing need to add here now
                }
            }
        }
        void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            //or you can also check => if (e.Info == SqlNotificationInfo.Insert) , if you want notification only for inserted record
            if (e.Type == SqlNotificationType.Change)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= sqlDep_OnChange;

                //from here we will send notification message to client
                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
                notificationHub.Clients.All.notify("added");
                //re-register notification
                RegisterNotification(DateTime.Now);
            }
        }

        public List<tbl_Notification> GetContacts(DateTime afterDate)
        {
            using (MyHandsDbContext dc = new MyHandsDbContext())
            {
                return dc.tbl_Notification.Where(a => a.dNgayTB > afterDate).OrderByDescending(a => a.dNgayTB).ToList();
            }
        }
    }
}