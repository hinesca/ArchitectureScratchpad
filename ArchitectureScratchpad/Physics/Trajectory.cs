using System;

namespace Physics
{
     public class Trajectory
     {
          public Trajectory(STPosition position)
          {
               Path = new STPosition[] { position };
               TrajectoryType = TrajectoryType.Grounded;
          }

          public Trajectory(STPosition initialPosition, STPosition finalPosition)
          {
               Path = new STPosition[] { initialPosition, finalPosition };
               TrajectoryType = TrajectoryType.Linear;
          }

          public Trajectory(STPosition[] path)
          {
               Path = path;
               TrajectoryType = TrajectoryType.Path;
          }

          public Trajectory(Trajectory parent, STPosition parentOffset)
          {
               ParentTrajectory = parent;
               ParentOffset = parentOffset;
          }
          
          public TrajectoryType TrajectoryType { get; set; }
          public STPosition[] Path { get; set; }
          public Trajectory ParentTrajectory { get; set; }
          public STPosition ParentOffset { get; set; }

          public static STPosition GetPosition(Trajectory trajectory)
          {
               switch (trajectory.TrajectoryType)
               {
                    case TrajectoryType.Grounded:
                         return trajectory.Path[0];
                    case TrajectoryType.Linear:
                         return GetLinearPosition();
                    case TrajectoryType.Path:
                         return GetPositionOnPath();
                    case TrajectoryType.Offset:
                         return GetOffsetPosition();
                    case TrajectoryType.Orbit:
                         return GetOrbitalPosition();
                    default:
                         return null;
               }
          }

          // ******************  TODO Implement with parameters that makes sense **************************************
          // ******************  Create and update trajectories and physical objects as needed ************************
          private static STPosition GetLinearPosition()
          {
               throw new NotImplementedException();
          }

          private static STPosition GetOffsetPosition()
          {
               throw new NotImplementedException();
          }

          private static STPosition GetOrbitalPosition()
          {
               throw new NotImplementedException();
          }

          private static STPosition GetPositionOnPath()
          {
               throw new NotImplementedException();
          }
          // ****************************  TODO Implement with parameters that makes sense ****************************




     }

     public enum TrajectoryType { Grounded, Linear, Path, Offset, Orbit }
}
