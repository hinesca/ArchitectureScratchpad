using System;
using System.Collections.Generic;

namespace Physics
{
     public class SnowBall : PhysicalObjectBase
     {
          public SnowBall(IPhysicalObject origin, List<SpaceTimePos> path) : base(origin.RTEngine)
          {
               Origin = origin;
               Trajectory = new Trajectory(path);
               EOL = DateTime.UtcNow + TimeSpan.FromSeconds(2);
               Sprite = 'o';
          }

          public SnowBall(IPhysicalObject origin, SpaceTimePos target) : base(origin.RTEngine)
          {
               DateTime now = DateTime.UtcNow;
               Origin = origin;
               List<SpaceTimePos> path = new List<SpaceTimePos> { origin.Trajectory.GetPosition(now), target };
               Trajectory = new Trajectory(path);
               EOL = now + TimeSpan.FromSeconds(2);
               Sprite = 'o';
          }

          protected static readonly Random _random = Random.Instance;

          public IPhysicalObject Origin { get; set; }

          public double SpriteSize
          {
               get
               {
                    return UncertaintyS + 10;
               }
          }

          public override void Interact(IPhysicalObject collider, DateTime now)
          {
               if (collider == this)
                    return;
               if (collider == Origin)
                    return;

               if (collider.GetType() == typeof(SnowBall))
               {
                    collider.Dispose();
                    Dispose();
               }
          }
     }
}
