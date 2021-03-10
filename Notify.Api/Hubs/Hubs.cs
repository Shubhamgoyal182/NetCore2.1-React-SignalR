using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Notify.Api.Model;

namespace Notify.Api.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SendMessage(NotifyMessage message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
