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
          public PresentationCollection Presenter {get; set;}
          public void CollideWith(IPhysicalObject colider);
     }
}
