using System;
using static System.Math; // https://docs.microsoft.com/en-us/dotnet/api/system.math?view=net-6.0

namespace Physics
{
     public static class Calculator
     {
          public static double[] GetPosition(Trajectory trajectory)
          {
               switch (trajectory.TrajectoryType)
               {
                    case TrajectoryType.Ground:
                         return trajectory.InitialPosition;
                    case TrajectoryType.Linear:
                         return GetLinearPosition();
                    case TrajectoryType.Offset:
                         return GetOffsetPosition();
                    case TrajectoryType.Orbit:
                         return GetOrbitalPosition();
                    case TrajectoryType.Simulated:
                         return GetSimulatedPosition();
               }

               if (trajectory.FinalPosition != null)
                    return trajectory.FinalPosition;
               
               return null;
          }

          // ******************  TODO Implement with parameters that makes sense **************************************
          // ******************  Create and update trajectories and physical objects as needed ************************
          private static double[] GetLinearPosition()
          {
               throw new NotImplementedException();
          }

          private static double[] GetOffsetPosition()
          {
               throw new NotImplementedException();
          }

          private static double[] GetOrbitalPosition()
          {
               throw new NotImplementedException();
          }

          private static double[] GetSimulatedPosition()
          {
               throw new NotImplementedException();
          }
          // ****************************  TODO Implement with parameters that makes sense ****************************
     }
}
