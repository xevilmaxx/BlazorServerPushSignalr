using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Components;
using BlazorSignalrTest.Statics;
using BlazorSignalrTest.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BlazorSignalrTest.Controllers
{
    public class IndexController : ComponentBase
    {
        private HubConnection _hubConnection;

        [Inject]
        public NavigationManager Navigation { get; set; }

        protected List<string> _receivedMessages = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            //base.OnInitialized();
            //.WithUrl($"{this.Navigation.BaseUri}/signalr")
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(@$"{Navigation.BaseUri}signalr")
                .Build();

            _hubConnection.On<string>("MessageReceived", message =>
            {
                _receivedMessages.Add(message);

                this.StateHasChanged();
            });

            await _hubConnection.StartAsync();
        }

        protected void TrigSignalR()
        {
            _hubConnection.SendAsync("SendMessageAsync", "Alive - " + new Random().Next());
        }

        protected void TrigSignalRServer()
        {
            //This is using local connection
            //_=SignalrHub0.Instance.SendMessageAsync("hello");

            //This is using server connection
            //Those are same, but in different variants
            //SignalrServerSide.HubContext.Clients.All.SendAsync("MessageReceived", "Server Alive - " + new Random().Next());
            _ = new SignalrHub0(SignalrServerSide.HubContext).SendMessageAsync("Server Alive - " + new Random().Next());
        }

        public ValueTask DisposeAsync()
        {
            return _hubConnection.DisposeAsync();
        }
    }
}
