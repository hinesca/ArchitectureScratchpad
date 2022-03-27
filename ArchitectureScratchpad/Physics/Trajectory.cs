using System;

namespace ArchitectureScratchpad.Physics
{
     public class Trajectory
     {
          public DateTime TZero { get; set; } = DateTime.Now;
          public double[] IntitalPosition { get; set; }
          public double[] InitialVelocity { get; set; }
     }
}
