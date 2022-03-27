using System;
using System.Collections.Generic;
using System.Text;

namespace Physics
{
     public class Player : PhysicalObjectBase
     {
          public double MaxPlayerSpeed { get; set; } = 1;
          public double MaxSnowballSpeed { get; set; } = 10;
     }
}
