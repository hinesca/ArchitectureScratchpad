﻿namespace Physics
{
     public class Player : PhysicalObjectBase
     {
          public Player()
          {
               Trajectory = new Trajectory(new STPosition(new Vector(250, 250, 0)));
               Sprite = '\u2603';
          }

          public double MaxPlayerSpeed { get; set; } = 1;
          public double MaxSnowballSpeed { get; set; } = 10;
     }
}
