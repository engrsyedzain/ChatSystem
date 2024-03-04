using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatSystem.Models
{

    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            //Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            return base.OnConnectedAsync();
        }
        public async Task SendMessage(string user, string message)
        {
            //message send to all users
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public Task SendMessageToGroup(string sender, string receiver, string message, string connectionId)
        {
            //message send to receiver only
            return Clients.Client(connectionId).SendAsync("ReceiveMessage", sender, message);
        }
    }

}
