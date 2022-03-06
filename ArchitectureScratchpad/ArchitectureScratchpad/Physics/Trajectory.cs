using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchitectureScratchpad.Physics
{
     public class Trajectory
     {
          public DateTime TZero {get; set;}
          public double[] InitialPosition { get; set; }
          public double[] InitialVelocity { get; set; }
     }
}
