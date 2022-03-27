﻿namespace Physics
{
     public interface IPhysicalObject
     {
          public double[] Position { get; }
          public Trajectory Trajectory { get; set; }
          public object Sprite { get; set; }
          public IPhysicalObject Parent { get; set; }
     }
}