using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SiegeCombat.Hubs
{
    public class HubChat : Hub
    {
        public void MandarMensaje()
        {
            Clients.All.mandarMensaje();
        }
    }
}