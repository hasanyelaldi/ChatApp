using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ChatApp.WebUI
{
    public class ChatHub : Hub
    {
        public void Send(string username, string message , string userimage , string messagetime)
        {
            Clients.All.sendMessage(username, message , userimage , messagetime);
        }
    }
}