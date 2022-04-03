namespace Physics
{
     public class SnowBall : PhysicalObjectBase
     {
          public SnowBall(STPosition[] path)
          {
               Trajectory = new Trajectory(path);
               Sprite = 'o';
          }
     }
}
