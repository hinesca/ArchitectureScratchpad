namespace ArchitectureScratchpad.Physics
{
     public interface IPhycicalObject
     {
          public double[] Position { get; }
          public Trajectory Trajectory { get; set; }
          public object Sprite { get; set; }
     }
}
