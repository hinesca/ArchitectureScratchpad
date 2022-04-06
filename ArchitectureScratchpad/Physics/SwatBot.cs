using System;
using System.Threading.Tasks;

namespace Physics
{
     public class SwatBot : PhysicalObjectBase
     {
          /// <summary>
          /// TODO Lazy implementation. Change.
          /// </summary>
          public SwatBot(Player player, PresentationCollection presentor)
          {
               HoastPlayer = player;
               HoastPresentor = presentor;
               Sprite = '\u2603';
               EOL = DateTime.MaxValue;
               StartAsync();
          }

          private static readonly Random _random = new Random();

          private async void StartAsync()
          {
               int snowballThrowCount = 0;
               Trajectory = new Trajectory(GetPath(HoastPlayer.STPosition.Position));
               int nextChange = 5 + _random.Next(15); // 5 - 20 throws of the snowball

               while (DateTime.UtcNow < EOL)
               {
                    int awaitMs = 500 + _random.Next(1500); // 0.5 - 2 seconds
                    await Task.Delay(awaitMs);
                    ThrowSnowball();
                    if (snowballThrowCount % nextChange == 0)
                    {
                         Trajectory = new Trajectory(GetPath());
                         nextChange = 5 + _random.Next(15); // 5 - 20 throws of the snowball
                         snowballThrowCount = 0;
                    }
               }
          }

          private STPosition[] GetPath()
          {
               Vector p1 = HoastPlayer.STPosition.Position;
               Vector p2 = new Vector(p1.X + Randouble(100), p1.Y + Randouble(100));
               return new STPosition[] { STPosition, new STPosition(p2, TimeSpan.FromSeconds(3)) };
          }

          private STPosition[] GetPath(Vector p0)
          {
               Vector p1 = new Vector(p0.X + Randouble(100), p0.Y + Randouble(100));
               Vector p2 = new Vector(p1.X + Randouble(100), p1.Y + Randouble(100));
               return new STPosition[] { new STPosition(p1), new STPosition(p2, TimeSpan.FromSeconds(3)) };
          }

          // TODO make random library
          private double Randouble(double pmRange)
          {
               return (-1 + 2 * _random.NextDouble()) * pmRange;
          }

          private void ThrowSnowball()
          {
               Vector p1 = new Vector(Randouble(10), Randouble(10));
               STPosition target = new STPosition(HoastPlayer.STPosition.Position + p1);
               HoastPresentor.Add(SnowBall.ThrowSnowball(STPosition, target, HoastPlayer.MaxSnowballSpeed));
          }

          PresentationCollection HoastPresentor { get; }

          Player HoastPlayer { get; }
     }
}
