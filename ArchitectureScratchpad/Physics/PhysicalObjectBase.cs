using System;

namespace Physics
{
     public class PhysicalObjectBase : IPhysicalObject
     {
          STPosition _stPosition;
          public STPosition STPosition
          {
               get
               {
                    DateTime now = DateTime.UtcNow;
                    if (_stPosition == null || _stPosition.Time < now + TimeSpan.FromMilliseconds(10))
                         _stPosition = Trajectory.GetPosition();
                    return _stPosition;
               }
          }

          public double ViewportX
          {
               get { return STPosition.Position.X; }
          }

          public double ViewportY
          {
               get { return STPosition.Position.Y; }
          }

          public Trajectory Trajectory { get; set; }
          public DateTime EOL { get; set; }
          public object Sprite { get; set; }
          public IPhysicalObject Parent { get; set; }
     }
}
*/
