using System;
using System.Collections.Generic;
using System.Text;

namespace Physics
{
     public class STPosition
     {
          public STPosition()
          {
               Position = new Vector();
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

          public Vector Position { get; set; }
          public DateTime Time { get; set; }
          public double PositionUncertainty { get; set; } = 0;
          public double TimeUncertainty { get; set; } = 0;
     }
}
