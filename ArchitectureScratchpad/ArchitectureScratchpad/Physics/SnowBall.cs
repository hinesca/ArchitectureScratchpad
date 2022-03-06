using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchitectureScratchpad.Physics
{
     public class SnowBall : PhysicalObjectBase
     {
          
          new public object Sprite { get; set; } = 'o';
          new public double Mass { get; set; } = 0.5;
     }
}
