namespace Physics
{
     public interface IPhysicalObject
     {
          public STPosition Position { get; }
          public Trajectory Trajectory { get; set; }
          public object Sprite { get; set; }
          public IPhysicalObject Parent { get; set; }
     }
}
