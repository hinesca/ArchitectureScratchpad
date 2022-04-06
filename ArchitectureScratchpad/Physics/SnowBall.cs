using System;

namespace Physics
{
     public class SnowBall : PhysicalObjectBase
     {
          public SnowBall(STPosition[] path)
          {
               Trajectory = new Trajectory(path);
               EOL = DateTime.UtcNow + TimeSpan.FromSeconds(10);
               Sprite = 'o';
          }

          /// <summary>
          /// TODO needs to change. Part of a lazy implementation.
          /// </summary>
          /// <param name="origin"></param>
          /// <param name="target"></param>
          /// <param name="speed"></param>
          /// <returns></returns>
          public static SnowBall ThrowSnowball(STPosition origin, STPosition target, double speed)
          {
               double range = (target.Position - origin.Position).Magnitude;
               target.Time = DateTime.UtcNow.AddSeconds(range / speed);
               origin.Next = target;
               STPosition[] path = new STPosition[] { origin, target };
               return new SnowBall(path);
          }
     }
}
