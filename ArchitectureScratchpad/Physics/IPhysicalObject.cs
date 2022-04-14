using System;

namespace Physics
{
     public interface IPhysicalObject : IDisposable
     {
          public Trajectory Trajectory { get; set; }
          public double UncertaintyS { get; set; }
          public double UncertaintyT { get; set; }
          public object Sprite { get; set; }
          public DateTime EOL { get; set; }
          public IPhysicalObject Parent { get; set; }
          public RealTimeEngine RTEngine {get; set;}
          public SpaceTimePos GetPosition(DateTime now);
          public void Interact(IPhysicalObject colider, DateTime now);
     }
}
