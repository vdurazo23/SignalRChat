using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRChat
{
    public class ChatHub : Hub
    {
        public static readonly System.Timers.Timer _Timer = new System.Timers.Timer();

        static ChatHub()
        {
            _Timer.Interval = 2000;
            _Timer.Elapsed += TiemerElapsed;
            _Timer.Start();
        }

        private static void TiemerElapsed(object sender, ElapsedEventArgs e)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext("ChatHub");
            hub.Clients.All.HeartBeat(DateTime.UtcNow.ToString());
        }

        public void Hello()
        {

            Clients.All.hello();
        }

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }
        public void MensajeaUsuario(string name, string message,string from)
        {
            Clients.All.addNewMessageToPage(name, message,from);
        }
        public void UpdateScore(string local, string visitante)
        {
            Clients.All.BroadcastScore(local, visitante);
        }
        public void UpdateDatetime(string fecha)
        {
            Clients.All.BroadcastDateTime(fecha);
        }
    }
}