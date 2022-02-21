using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArchitectureScratchpad.Hubs
{
     public class ChatHub : Hub
     {
          public async Task SendMessage(string user, string message)
          {
               List<Task> listOfTasks = new List<Task>();

               await Clients.All.SendAsync("ReceiveMessage", user, message);
          }
     }
}
