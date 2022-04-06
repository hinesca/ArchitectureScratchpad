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
          public double PositionUncertainty { get; set; } = 0;
          public double TimeUncertainty { get; set; } = 0;

          private STPosition _next;
          /// <summary>
          /// Returns the next position in time and space.
          /// A new position will be generated in the future if one does not exist.
          /// </summary>
          public STPosition Next
          {
               get
               {
                    DateTime now = DateTime.UtcNow;
                    if (_next == null)
                    {
                         _next = new STPosition(Position, now + TimeSpan.FromSeconds(1));
                    }
                    else if (_next.Time > now)
                    {
                         _next = _next.Next;
                    }
                    return _next;
               }
               set
               {
                    _next = value;
               }
          }

          public STPosition Previous { get; set; }
     }
}
