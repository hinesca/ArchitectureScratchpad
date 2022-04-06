using System;

namespace Physics
{
     public interface IPhysicalObject
     {
          public STPosition STPosition { get; }
          public Trajectory Trajectory { get; set; }
          public object Sprite { get; set; }
          public DateTime EOL { get; set; }
          public IPhysicalObject Parent { get; set; }
     }
}
