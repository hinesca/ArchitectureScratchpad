using System;

namespace Physics
{
     public interface IPhysicalObject : IDisposable
     {
          public STPosition STPosition { get; }
          public Trajectory Trajectory { get; set; }
          public double UncertaintyS { get; set; }
          public double UncertaintyTms { get; set; }
          public object Sprite { get; set; }
          public DateTime EOL { get; set; }
          public IPhysicalObject Parent { get; set; }
          public RealTimeEngine Presenter {get; set;}
          public void Interact(IPhysicalObject colider, DateTime now);
     }
}
