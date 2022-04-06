using System;

namespace Physics
{
     public class Player : PhysicalObjectBase
     {
          public Player()
          {
               STPosition[] path =
                    new STPosition[]
                    {
                         new STPosition(new Vector(250, 250, 0)),
                         new STPosition(new Vector(250, 250, 0), DateTime.UtcNow + TimeSpan.FromSeconds(1))
                    };
               Trajectory = new Trajectory(path);
               EOL = DateTime.MaxValue;
               Sprite = '\u2603';
          }

          public double MaxPlayerSpeed { get; set; } = 10;
          public double MaxSnowballSpeed { get; set; } = 100;
     }
}
