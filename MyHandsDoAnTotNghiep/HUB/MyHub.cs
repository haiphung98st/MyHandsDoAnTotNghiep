using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Model.EF;

namespace MyHandsDoAnTotNghiep.HUB
{
    public class MyHub : Hub
    {
        //private static string cnntr = ConfigurationManager.ConnectionStrings["MyHandsDbContext"].ConnectionString;

        //public void Hello()
        //{

        //    Clients.All.hello();
        //}
        //[HubMethodName("sendMessages")]
        //public static void SendMessages()
        //{
        //    IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
        //    context.Clients.All.updateMessages();
        //}
        //MyHandsDbContext db = null;
        //public void Send(string stitle, string sChiTiet)
        //{
        //    // Call the addNewMessageToPage method to update clients.
        //    Clients.All.addNewMessageToPage(stitle, sChiTiet);

        //    var notification = new tbl_Notification
        //    {
        //        //CommentID = Guid.NewGuid(), // or find a way to autoincrement an int 
        //        sTittle = stitle,
        //        sChiTiet = sChiTiet
        //    };
        //    db.tbl_Notification.Add(notification);
        //    db.SaveChanges();
        //}

        //public override Task OnConnected()
        //{
        //    var comments = db.tbl_Notification.ToList(); // some sort of cache would be good here
        //    foreach (var comment in comments)
        //    {
        //        Clients.Client(Context.ConnectionId).addNewMessageToPage(comment.sTittle, comment.sChiTiet);
        //    }
        //    return base.OnConnected();
        //}
    }
}