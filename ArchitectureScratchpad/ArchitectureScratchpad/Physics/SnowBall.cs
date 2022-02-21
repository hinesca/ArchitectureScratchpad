using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchitectureScratchpad.Physics
{
     public class SnowBall : IPhycicalObject
     {
          public double[] Position
          {
               get
               {
                    double[] returnPosition = Trajectory.IntitalPosition;
                    TimeSpan dt = DateTime.Now - Trajectory.TZero;
                    for (int i=0; i < Trajectory.IntitalPosition.Length; i++)
                    {
                         returnPosition[i] += Trajectory.InitialVelocity[i] * dt.TotalSeconds;
                    }
                    return returnPosition;
               }          
          }
          public Trajectory Trajectory { get; set; }
          public object Sprite { get; set; } = 'o';
     }
}
