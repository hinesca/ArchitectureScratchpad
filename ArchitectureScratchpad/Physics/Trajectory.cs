using System;
using System.Linq;

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

          private STPosition _nextPointOnPath;
          public STPosition NextPointOnPath
          {
               get
               {
                    DateTime now = DateTime.UtcNow;
                    if (_nextPointOnPath == null || _nextPointOnPath.Time < now)
                    {
                         // get the first point in the future
                         _nextPointOnPath = Path.FirstOrDefault(p => p.Time > now);
                         if (_nextPointOnPath == null)
                              _nextPointOnPath = Path.Last().Next; // .Next will generate a new point in the future
                    }
                    return _nextPointOnPath;
               }
          }

          private STPosition _previousPointOnPath;
          public STPosition PreviousPointOnPath
          {
               get
               {
                    DateTime now = DateTime.UtcNow;
                    if (_previousPointOnPath == null || _previousPointOnPath.Time > now)
                    {
                         _previousPointOnPath = Path.LastOrDefault(p => p.Time > now);
                         if (_previousPointOnPath == null)
                              _previousPointOnPath = Path.Last();
                    }
                    return _previousPointOnPath;
               }
          }

          public Vector Velocity
          {
               get
               {
                    // TODO: Review
                    DateTime now = DateTime.UtcNow;

                    if (now > Path.Last().Time)
                         return new Vector(); // Zero vector

                    TimeSpan interval = NextPointOnPath.Time - PreviousPointOnPath.Time;
                    return NextPointOnPath.Position - PreviousPointOnPath.Position * (1 / interval.TotalSeconds);
               }
          }

          public Trajectory ParentTrajectory { get; set; }
          public STPosition ParentOffset { get; set; }

          public STPosition GetPosition()
          {
               return GetPosition(this);
          }

          public static STPosition GetPosition(Trajectory trajectory)
          {
               switch (trajectory.TrajectoryType)
               {
                    case TrajectoryType.Grounded:
                         return new STPosition(trajectory.Path[0]);
                    case TrajectoryType.Linear:
                         return GetPositionOnPath(trajectory);
                    case TrajectoryType.Path:
                         return GetPositionOnPath(trajectory);
                    case TrajectoryType.Offset:
                         return GetOffsetPosition(trajectory);
                    case TrajectoryType.Orbit:
                         return GetOrbitalPosition(trajectory);
                    default:
                         return null;
               }
          }

          private static STPosition GetPositionOnPath(Trajectory trajectory)
          {
               DateTime now = DateTime.UtcNow;

               if (now > trajectory.Path.Last().Time)
                    return new STPosition(trajectory.Path.Last().Position, now);

               TimeSpan interval = now - trajectory.PreviousPointOnPath.Time;
               return new STPosition(trajectory.PreviousPointOnPath.Position
                    + trajectory.Velocity * interval.TotalSeconds, now);
          }

          private static STPosition GetOffsetPosition(Trajectory trajectory)
          {
               throw new NotImplementedException();
          }

          private static STPosition GetOrbitalPosition(Trajectory trajectory)
          {
               throw new NotImplementedException();
          }
     }

     public enum TrajectoryType { Grounded, Linear, Path, Offset, Orbit }
}
