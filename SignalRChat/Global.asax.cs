using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.AspNet.SignalR;

namespace SignalRChat
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterSQLNotifications();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }


        private void RegisterSQLNotifications()
        {
            string connectionString = @"server=10.251.10.21;database=MARSTest;uid=sa;password=martinrea;";
            SqlDependency.Start(connectionString);
            string commandText = @"Select Id from dbo.MobileMessages";

            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    connection.Open();
                    var sqlDependency = new SqlDependency(command);


                    sqlDependency.OnChange += sqlDependency_OnChange;

                    // NOTE: You have to execute the command, or the notification will never fire.
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                    }
                }
            }
        }

        void sqlDependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info == SqlNotificationInfo.Insert)
            {
                //This is how signalrHub can be accessed outside the SignalR Hub MyHub.cs file
                // you can add your business logic here, like what exactly needs to be broadcasted
                var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                context.Clients.All.sendNotifications();
                context.Clients.All.broadcastMessage("SQL","Se acaba de Insertar");
            }

            if (e.Info == SqlNotificationInfo.Update)
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                context.Clients.All.sendNotifications();
                context.Clients.All.broadcastMessage("SQL", "Se acaba de actualizar");
            }
            //Call the RegisterSQLNotifications method again
            RegisterSQLNotifications();
        }

    }
}