using System;
using System.Collections.Generic;
using System.Linq;

namespace Physics
{
     public class Trajectory : List<SpaceTimePos>
     {
          public Trajectory(SpaceTimePos position)
          {
               Add(position);
               _previousPointOnPath = position;
               TrajectoryType = TrajectoryType.Grounded;
          }

          public Trajectory(List<SpaceTimePos> path)
          {
               AddRange(path);
               _previousPointOnPath = path[0];
               TrajectoryType = TrajectoryType.Path;
          }

          public Trajectory(Trajectory parent, SpaceTimePos parentOffset)
          {
               ParentTrajectory = parent;
               ParentOffset = parentOffset;
               TrajectoryType = TrajectoryType.Offset;
          }

          public TrajectoryType TrajectoryType { get; set; }


          private SpaceTimePos _nextPointOnPath;
          public SpaceTimePos NextPointOnPath(DateTime now)
          {
               if (_nextPointOnPath == null || _nextPointOnPath.T < now)
               {
                    // get the first point in the future
                    _nextPointOnPath = this.FirstOrDefault(p => p.T > now);
                    if (_nextPointOnPath == null) // generate new position in the arbitrary future
                    {
                         _nextPointOnPath
                              = new SpaceTimePos(this.Last().S, DateTime.MaxValue);
                         Add(_nextPointOnPath);
                    }
               }
               return _nextPointOnPath;
          }

          private SpaceTimePos _previousPointOnPath;
          public SpaceTimePos PreviousPointOnPath
          {
               get
               {
                    DateTime now = DateTime.UtcNow;
                    if (_previousPointOnPath == null || _previousPointOnPath.T > now)
                    {
                         // get the last point in the past
                         _previousPointOnPath = this.LastOrDefault(p => p.T > now);
                         if (_previousPointOnPath == null) // generate new position now
                         {
                              _previousPointOnPath = new SpaceTimePos(this.First().S, now);
                              Add(_previousPointOnPath);
                         }
                    }
                    return _previousPointOnPath;
               }
          }

          public Trajectory ParentTrajectory { get; set; }
          public SpaceTimePos ParentOffset { get; set; }

          public SpaceTimePos GetPosition(DateTime now)
          {
               return GetPosition(this, now);
          }

          public static SpaceTimePos GetPosition(Trajectory trajectory, DateTime now)
          {
               switch (trajectory.TrajectoryType)
               {
                    //case TrajectoryType.Grounded:
                    //     return new STPosition(trajectory[0].S, now);
                    case TrajectoryType.Path:
                         return GetPositionOnPath(trajectory, now);
                    //case TrajectoryType.Offset:
                    //     return GetOffsetPosition(trajectory, now);
                    //case TrajectoryType.Orbit:
                    //     return GetOrbitalPosition(trajectory, now);
                    default:
                         return null;
               }
          }

          private static SpaceTimePos GetPositionOnPath(Trajectory trajectory, DateTime now)
          {
               if (now > trajectory.Last().T)
                    return new SpaceTimePos(trajectory.Last().S, now);

               TimeSpan interval = now - trajectory.PreviousPointOnPath.T;
               Vector velocity = GetVelocity(trajectory, now);
               return new SpaceTimePos(trajectory.PreviousPointOnPath.S + velocity * interval.TotalSeconds, now);
          }

          private static SpaceTimePos GetOffsetPosition(Trajectory trajectory, DateTime now)
          {
               throw new NotImplementedException();
          }

          private static SpaceTimePos GetOrbitalPosition(Trajectory trajectory, DateTime now)
          {
               throw new NotImplementedException();
          }

          public Vector GetVelocity(DateTime now)
          {
               return GetVelocity(this, now);
          }
          public static Vector GetVelocity(Trajectory trajectory, DateTime now)
          {
               if (now > trajectory.Last().T)
                    return new Vector(); // Zero vector

               SpaceTimePos next = trajectory.NextPointOnPath(now);
               SpaceTimePos previous = trajectory.PreviousPointOnPath;
               TimeSpan interval = next.T - previous.T;
               if (interval.TotalSeconds > 0)
                    return (next.S - previous.S) * (1 / interval.TotalSeconds);
               else return new Vector(); // Zero vector
          }
     }

     public enum TrajectoryType { Grounded, Linear, Path, Offset, Orbit }
}
