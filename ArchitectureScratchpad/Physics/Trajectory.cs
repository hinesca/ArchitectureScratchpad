using System;

namespace Physics
{
     public class Trajectory
     {
          public Trajectory(TrajectoryType trajectoryType, double[] initialPosition,
               double[] initialVelocity, double[] finalPosition = null)
          {
               TrajectoryType = trajectoryType;
               InitialPosition = initialPosition;
               InitialVelocity = initialVelocity;
               FinalPosition = finalPosition;
          }

          public Trajectory(Trajectory parent, double[] parentOffset)
          {
               ParentTrajectory = parent;
               ParentOffset = parentOffset;
          }
          
          public TrajectoryType TrajectoryType { get; set; }
          public DateTime InitialTime { get; set; } = DateTime.Now;
          public double[] InitialVelocity { get; set; }
          public double[] InitialPosition { get; set; }
          public double[] FinalPosition { get; set; }          
          public Trajectory ParentTrajectory { get; set; }
          public double[] ParentOffset { get; set; }
     }

     public enum TrajectoryType { Ground, Linear, Orbit, Offset, Simulated }
}
