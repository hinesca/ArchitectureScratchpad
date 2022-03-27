using System;

namespace Physics
{
     public class PhysicalObjectBase : IPhysicalObject
     {
          public double[] Position
          {
               get
               {
                    double[] returnPosition = Trajectory.InitialPosition;
                    TimeSpan dt = DateTime.Now - Trajectory.InitialTime;
                    for (int i=0; i < Trajectory.InitialPosition.Length; i++)
                    {
                         returnPosition[i] += Trajectory.InitialVelocity[i] * dt.TotalSeconds;
                    }
                    return returnPosition;
               }
          }
          public Trajectory Trajectory { get; set; }
          public object Sprite { get; set; }
          public IPhysicalObject Parent { get; set; }
     }
}
