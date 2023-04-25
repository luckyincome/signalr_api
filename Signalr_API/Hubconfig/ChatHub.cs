using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace Signalr_API.Hubconfig
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("transferchartdata", message);
        }
    }
 
}
