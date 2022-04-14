using System;

namespace Physics
{
     /// <summary>
     /// Position in both space and time.
     /// </summary>
     public class SpaceTimePos
     {
          public SpaceTimePos()
          {
               S = new Vector();
               T = DateTime.UtcNow;
          }

          public SpaceTimePos(SpaceTimePos stPosition)
          {
               S = stPosition.S;
               T = DateTime.UtcNow;
          }

          public SpaceTimePos(Vector position)
          {
               S = position;
               T = DateTime.UtcNow;
          }

          public SpaceTimePos(Vector position, DateTime time)
          {
               S = position;
               T = time;
          }

          public SpaceTimePos(Vector position, TimeSpan fromNow)
          {
               S = position;
               T = DateTime.UtcNow + fromNow;
          }

          public Vector S { get; set; }
          public DateTime T { get; set; }
          public double UncertaintyS { get; set; } = 0;
          public double UncertaintyT { get; set; } = 0;
     }
}
