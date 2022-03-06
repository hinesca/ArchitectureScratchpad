using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchitectureScratchpad.Physics
{
     public class ObjectPhysics
     {
          public void ApplyImpulse(IPhysicalObject physicalObject, double [,] ImpulseForces)
          {
            // Calculate the net velocity vector
            // f = m*a -> a = f/m -> a = dv/dt -> v = v_0 + a*dt
            DateTime newT = DateTime.Now;
            double[] current_position = physicalObject.Position;
            TimeSpan dt = newT - physicalObject.Trajectory.TZero;
            for (int f = 0; f<ImpulseForces.Length; f++)
            {
                for (int i=0; i < physicalObject.Trajectory.InitialVelocity.Length; i++)
                {
                    physicalObject.Trajectory.InitialVelocity[i] += ImpulseForces[f,i] / physicalObject.Mass * dt.TotalSeconds;
                }
            }
              // Update the intial position
              for (int i=0; i < physicalObject.Trajectory.InitialPosition.Length; i++)
              {
                    physicalObject.Trajectory.InitialPosition[i] = current_position[i] + physicalObject.Trajectory.InitialVelocity[i] * dt.TotalSeconds;
              }
              // Update the initial time
              physicalObject.Trajectory.TZero = newT;
          }
     }
}