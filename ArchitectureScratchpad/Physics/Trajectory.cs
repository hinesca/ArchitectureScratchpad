using System;
using System.Collections.Generic;
using System.Linq;

namespace Physics
{
     public class Trajectory
     {
          public Trajectory(STPosition position)
          {
               PathForward = new List<STPosition> { position };
               _previousPointOnPath = position;
               TrajectoryType = TrajectoryType.Grounded;
          }

          public Trajectory(List<STPosition> path)
          {
               PathForward = path;
               _previousPointOnPath = path[0];
               TrajectoryType = TrajectoryType.Path;
          }

          public Trajectory(Trajectory parent, STPosition parentOffset)
          {
               ParentTrajectory = parent;
               ParentOffset = parentOffset;
               TrajectoryType = TrajectoryType.Offset;
          }

          public TrajectoryType TrajectoryType { get; set; }
          public List<STPosition> PathForward { get; set; }

          private STPosition _pathEndPoint;
          public STPosition PathEndPoint
          {
               get
               {
                    DateTime now = DateTime.UtcNow;
                    if (_pathEndPoint == null || _pathEndPoint.Time > now)
                         _pathEndPoint = NextPointOnPath;

                    return _pathEndPoint;
               }
          }

          private STPosition _nextPointOnPath;
          public STPosition NextPointOnPath
          {
               get
               {
                    DateTime now = DateTime.UtcNow;
                    if (_nextPointOnPath == null || _nextPointOnPath.Time < now)
                    {
                         // get the first point in the future
                         _nextPointOnPath = PathForward.FirstOrDefault(p => p.Time > now);
                         if (_nextPointOnPath == null) // generate new position in future
                         {
                              _nextPointOnPath
                                   = new STPosition(PathEndPoint.Position, now + TimeSpan.FromMilliseconds(10));
                              PathForward.Add(_nextPointOnPath);
                         }
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
                    STPosition[] path = PathForward.ToArray();
                    
                    for (int i = 0; i < path.Length; i++)
                    {
                         if (path[i].Time < now)
                         {
                              _previousPointOnPath = path[i];
                              PathForward.Remove(_previousPointOnPath);
                         }
                         else return _previousPointOnPath;
                    }
                    return _previousPointOnPath;
               }
          }

          /// <summary>
          /// This velocity is calculated as the average velocity between the next and previous positions.
          /// </summary>
          public Vector Velocity
          {
               get
               {
                    // TODO: Review
                    DateTime now = DateTime.UtcNow;

                    if (now > PathEndPoint.Time)
                         return new Vector(); // Zero vector

                    STPosition next = NextPointOnPath;
                    STPosition previous = PreviousPointOnPath;
                    TimeSpan interval = next.Time - previous.Time;
                    if (interval.TotalSeconds > 0)
                         return (next.Position - previous.Position) * (1 / interval.TotalSeconds);
                    else return new Vector(); // Zero vector
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
                         return new STPosition(trajectory.PathForward[0]);
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

               if (now > trajectory.PathEndPoint.Time)
                    return new STPosition(trajectory.PathEndPoint.Position, now);

               TimeSpan interval = now - trajectory.PreviousPointOnPath.Time;
               Vector velocity = trajectory.Velocity;
               double speed = velocity.Magnitude; // TODO incorporate into uncertainty or remove
               return new STPosition(trajectory.PreviousPointOnPath.Position + velocity * interval.TotalSeconds, now);
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
