using System;
using System.Collections.Generic;

namespace Physics
{
     public class SnowBall : PhysicalObjectBase
     {
          public SnowBall(IPhysicalObject origin, List<STPosition> path) : base(origin.Presenter)
          {
               Origin = origin;
               Trajectory = new Trajectory(path);
               EOL = DateTime.UtcNow + TimeSpan.FromSeconds(10);
               Sprite = 'o';
          }

          public SnowBall(IPhysicalObject origin, STPosition target) : base(origin.Presenter)
          {
               Origin = origin;
               List<STPosition> path = new List<STPosition> { origin.STPosition, target };
               Trajectory = new Trajectory(path);
               EOL = DateTime.UtcNow + TimeSpan.FromSeconds(10);
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

          /// <summary>
          /// TODO needs to change. Part of a lazy implementation.
          /// </summary>
          /// <param name="origin"></param>
          /// <param name="target"></param>
          /// <param name="speed"></param>
          /// <returns></returns>
          public static void ThrowSnowball(IPhysicalObject origin, Vector target, double speed, double errorMils = 0.1)
          {
               //Vector pos = origin.STPosition.Position;
               double range = (target - origin.STPosition.Position).Magnitude;
               Vector accuracyModifier
                    = new Vector(_random.Randouble(1), _random.Randouble(1)).Unit() * errorMils * range;
               target = target + accuracyModifier;
               STPosition stTarget = new STPosition(target, DateTime.UtcNow.AddSeconds(range / speed));
               // TODO figure out if we want to use this for more complex paths
               //List<STPosition> path = new List<STPosition> { origin.STPosition, stTarget };
               origin.Presenter.Add(new SnowBall(origin, stTarget));
          }

          public override void CollideWith(IPhysicalObject collider)
          {
               if (collider == this)
                    return;
               if (collider == Origin)
                    return;

               if (collider.GetType() == typeof(SnowBall))
               {
                    Presenter.Remove(this);
                    Presenter.Remove(collider);
               }
          }
     }
}
