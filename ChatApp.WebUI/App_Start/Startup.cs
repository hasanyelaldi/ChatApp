﻿using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(ChatApp.Startup))]
 
namespace ChatApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}