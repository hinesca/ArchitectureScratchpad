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
          public SwatBot(Player player, RealTimeEngine presenter) : base (presenter)
          {
               HoastPlayer = player;
               Sprite = '\u2603';
               EOL = DateTime.MaxValue;
               StartAsync();
               Hit += OnHit;
          }

          private async void StartAsync()
          {
               DateTime now = DateTime.UtcNow;
               long longNow = now.Ticks;
               DateTime nowAgain = DateTime.FromBinary(longNow);
               if (now == nowAgain)
               {
                    longNow++;
               }
               ChangeTrajectory(now);
               int snowballThrowCount = 0;
               int nextChange = 2 + _random.Next(3); // 2 - 5 throws of the snowball

               while (now < EOL)
               {
                    // when aggressionModifier reaches zero @ 100 hits, bot throws a snowball every 0.2 seconds
                    int aggressionModifier = 1000 - 10 * HoastPlayer.OutgoingHits;
                    if (aggressionModifier < 0)
                         aggressionModifier = 0;

                    int awaitMs = 200 + _random.Next(2 * aggressionModifier); // 0.2 - 2 seconds
                    await Task.Delay(awaitMs);
                    now = DateTime.UtcNow;
                    Vector lead = CalculateLead(this, HoastPlayer, SnowballSpeed, now);
                    ThrowSnowball(HoastPlayer.GetPosition(now).S + lead, now);
                    snowballThrowCount++;
                    if (snowballThrowCount == nextChange)
                    {
                         ChangeTrajectory(now);
                         nextChange = 3 + _random.Next(50 / (1 + aggressionModifier));
                         snowballThrowCount = 0;
                    }
               }
          }

          public static Vector CalculateLead(
               IPhysicalObject origin, IPhysicalObject target, double interceptorSpeed, DateTime now)
          {
               Vector targetVelocity = target.Trajectory.GetVelocity(now);
               Vector targetVector = origin.Trajectory.GetPosition(now).S - target.Trajectory.GetPosition(now).S;
               double tot = targetVector.Magnitude / interceptorSpeed;
               Vector lead = targetVelocity * tot;
               // also aim a bit down range from target
               lead = lead - 0.1 * targetVector;
               return lead;
          }

          private void ChangeTrajectory(DateTime now)
          {
               Vector p1 = HoastPlayer.GetPosition(now).S;
               Vector target = new Vector(p1.X + _random.Double(300), p1.Y + _random.Double(300));
               MoveTo(target, now);
          }

          Player HoastPlayer { get; }
     }
}
