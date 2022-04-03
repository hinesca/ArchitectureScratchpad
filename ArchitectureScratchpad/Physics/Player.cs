namespace Physics
{
     public class Player : PhysicalObjectBase
     {
          public Player()
          {
               Trajectory = new Trajectory(new STPosition(new Vector(500, 500, 0)));
               Sprite = '\u2603';
          }
          
          public double MaxPlayerSpeed { get; set; } = 10;
          public double MaxSnowballSpeed { get; set; } = 100;
     }
}
