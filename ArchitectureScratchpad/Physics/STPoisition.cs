using System;

namespace Physics
{
     /// <summary>
     /// Position in both space and time.
     /// This class is intended to form a linked-list path through space and time.
     /// </summary>
     public class STPosition
     {
          public STPosition()
          {
               Position = new Vector();
               Time = DateTime.UtcNow;
          }

          public STPosition(STPosition stPosition)
          {
               Position = stPosition.Position;
               Time = DateTime.UtcNow;
          }

          public STPosition(Vector position)
          {
               Position = position;
               Time = DateTime.UtcNow;
          }

          public STPosition(Vector position, DateTime time)
          {
               Position = position;
               Time = time;
          }

          public STPosition(Vector position, TimeSpan fromNow)
          {
               Position = position;
               Time = DateTime.UtcNow + fromNow;
          }

          public Vector Position { get; set; }
          public DateTime Time { get; set; }
          public double UncertaintyS { get; set; } = 0;
          public double UncertaintyTms { get; set; } = 0;
     }
}
