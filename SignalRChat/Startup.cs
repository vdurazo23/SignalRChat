using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalRChat.Startup))]

namespace SignalRChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Para obtener más información acerca de cómo configurar su aplicación, visite http://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
            //app.MapSignalR<NfcConnection>("/echo");
        }
    }

    //public class NfcConnection : PersistentConnection
    //{
    //    protected override Task OnConnected(IRequest request, string connectionId)
    //    {
    //        string msg = string.Format(
    //            "A new user {0} has just joined. (ID: {1})",
    //            request.QueryString["name"], connectionId);
    //        return Connection.Broadcast(msg);
    //    }

    //    protected override Task OnReceived(IRequest request, string connectionId, string data)
    //    {

    //        string msg = string.Format(
    //            "{0}: {1}", request.QueryString["name"], data);
    //        return Connection.Broadcast(msg);
    //    }
    //}
}
