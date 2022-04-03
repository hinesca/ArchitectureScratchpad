using System;

namespace Physics
{
     public class PhysicalObjectBase : IPhysicalObject
     {
          public STPosition STPosition
          {
               get
               {
                    return Trajectory.GetPosition();
               }
          }
          public Trajectory Trajectory { get; set; }
          public object Sprite { get; set; }
          public IPhysicalObject Parent { get; set; }
     }
}
