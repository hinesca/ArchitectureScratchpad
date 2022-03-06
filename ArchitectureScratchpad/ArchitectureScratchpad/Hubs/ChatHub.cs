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
               if (message == "throw snowball") {
                    update_snowball = true;
                    snowBall.Trajectory = new Physics.Trajectory();
                    snowBall.Trajectory.TZero = System.DateTime.Now;
                    snowBall.Trajectory.InitialPosition = new double[] { 0.0, 0.0, 0.0 };
                    snowBall.Trajectory.InitialVelocity = new double[] { 10.0, 0.0, 0.0 };
                }
               if (update_snowball)
                {
                message = System.String.Format(snowBall.Sprite + " x={0}",snowBall.Position[0]);
                for (int i = 1; i < 10; i++)
                {
                    message = System.String.Format(snowBall.Sprite + " x={0}", snowBall.Position[0]);
                    await Clients.All.SendAsync("ReceiveMessage", user, message);
                }
                }
               await Clients.All.SendAsync("ReceiveMessage", user, message);
          }
        public bool update_snowball = false;
        public Physics.SnowBall snowBall = new Physics.SnowBall();
    }
}
