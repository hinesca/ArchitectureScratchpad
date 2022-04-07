using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Physics
{
     public class SwatBot : Player
     {
          /// <summary>
          /// TODO Lazy implementation. Change.
          /// </summary>
          public SwatBot(Player player, PresentationCollection presenter) : base (presenter)
          {
               HoastPlayer = player;
               Sprite = '\u2603';
               EOL = DateTime.MaxValue;
               StartAsync();
          }

          private async void StartAsync()
          {
               int snowballThrowCount = 0;
               ChangeTrajectory();
               int nextChange = 2 + _random.Next(3); // 2 - 5 throws of the snowball

               while (DateTime.UtcNow < EOL)
               {
                    // when effectivnessModifyer reaches zero @ 100 hits, bot throws a snowball every 0.2 seconds
                    int effectivnessModifyer = 1000 - 10 * HoastPlayer.OutgoingHits;
                    if (effectivnessModifyer < 0)
                         effectivnessModifyer = 0;

                    int awaitMs = 200 + _random.Next(2 * effectivnessModifyer); // 0.2 - 2 seconds
                    await Task.Delay(awaitMs);
                    Vector lead = CalculateLead(this, HoastPlayer, SnowballSpeed);
                    ThrowSnowball(HoastPlayer.STPosition.Position + lead);
                    snowballThrowCount++;
                    if (snowballThrowCount == nextChange)
                    {
                         ChangeTrajectory();
                         nextChange = 3 + _random.Next(50 / (1 + effectivnessModifyer));
                         snowballThrowCount = 0;
                    }
               }
          }

          public static Vector CalculateLead(IPhysicalObject origin, IPhysicalObject target, double interceptorSpeed)
          {
               Vector targetVelocity = target.Trajectory.Velocity;
               Vector targetVector = origin.STPosition.Position - target.STPosition.Position;
               double tot = targetVector.Magnitude / interceptorSpeed;
               Vector lead = targetVelocity * tot;
               // also aim a bit down range from target
               lead = lead - 0.2 * targetVector;
               return lead;
          }

          private void ChangeTrajectory()
          {
               Vector p1 = HoastPlayer.STPosition.Position;
               Vector target = new Vector(p1.X + _random.Randouble(300), p1.Y + _random.Randouble(300));
               UpdateTrajectory(target);
          }

          Player HoastPlayer { get; }
     }
}
