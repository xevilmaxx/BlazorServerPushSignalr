using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSignalrTest.Hubs
{
    /// <summary>
    /// Sources:
    /// https://newbedev.com/how-to-get-signalr-hub-context-in-a-asp-net-core
    /// https://stackoverflow.com/questions/51968201/invoking-signalr-hub-not-working-for-asp-net-core-web-api/51981886#51981886
    /// </summary>
    public class SignalrHub0 : Hub
    {

        public IHubContext<SignalrHub0> HubContext { get; private set; }

        public SignalrHub0 (IHubContext<SignalrHub0> Context)
        {
            HubContext = Context;
        }

        public async Task SendMessageAsync(string message)
        {
            if(HubContext.Clients != null)
            {
                await HubContext.Clients.All.SendAsync("MessageReceived", message);
            }
        }

    }
}
