using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.SignalR;
using System.Reflection;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ChatSystem.Models
{

    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;

        public ChatHub(AppDbContext context)
        {
            _context = context;
        }

        public override Task OnConnectedAsync()
        {
            //Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);

            var param1 = Context.GetHttpContext().Request.Query["senderId"].ToString();

            int senderId = Convert.ToInt32(param1);
            var member = _context.MemberConnections.SingleOrDefault(s => s.MemberId == senderId);
            member.ConnectionId = Context.ConnectionId;
            _context.SaveChanges();


            return base.OnConnectedAsync();
        }
      
        public async Task SendMessage(string user, string message)
        {            
            //message send to all users
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public Task SendMessageToGroup(string senderId, string receiverId, string sender, string message, string dateTime)
        {
           
            var memberReceiver = _context.MemberConnections.SingleOrDefault(s => s.MemberId == Convert.ToInt32(receiverId));

            var date = Convert.ToDateTime(dateTime);

            var msg = new Message
            {
                ReceiverId = Convert.ToInt32(receiverId),
                SenderId = Convert.ToInt32(senderId),
                MessageText = message,
                CreatedDate = date,
            };

            _context.Messages.Add(msg);
            _context.SaveChanges();

            return Clients.Client(memberReceiver.ConnectionId).SendAsync("ReceiveMessage", sender, message, date.ToString("dd/MMM/yyyy : hh:mm:ss tt"));
        }

    }

}
