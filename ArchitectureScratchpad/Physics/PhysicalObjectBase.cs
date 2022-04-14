using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Physics
{
     public class PhysicalObjectBase : IPhysicalObject, IDisposable, INotifyPropertyChanged
     {
          public PhysicalObjectBase(RealTimeEngine presenter)
          {
               RTEngine = presenter;
          }

          public event EventHandler Hit;

          public SpaceTimePos GetPosition(DateTime now)
          {
               return Trajectory.GetPosition(now);
          }

          public Vector GetVelocity(DateTime now)
          {
               return Trajectory.GetVelocity(now);
          }

          public virtual void Interact(IPhysicalObject o, DateTime now)
          {
               if (o.Equals(this))
                    return;

               EventArgs args = new EventArgs(); // TODO put now in new type of args (or don't use... will it be needed?)
               if (Hit != null)
                    Hit.Invoke(this, args);
          }

          public bool _disposed = false;
          public void Dispose()
          {
               if (_disposed)
                    return;
               RTEngine.Remove(this);
               EOL = DateTime.UtcNow;
               _disposed = true;
          }

          public double ViewportX
          {
               get { return GetPosition(DateTime.UtcNow).S.X; }
          }

          public double ViewportY
          {
               get { return GetPosition(DateTime.UtcNow).S.Y; }
          }

          public Trajectory Trajectory { get; set; }
          public virtual double UncertaintyS { get; set; } = 1;
          public double UncertaintyT { get; set; } = 10;
          public DateTime EOL { get; set; }
          public object Sprite { get; set; }
          public IPhysicalObject Parent { get; set; }
          public RealTimeEngine RTEngine { get; set; }

          public event PropertyChangedEventHandler PropertyChanged;
          /// <summary>
          /// This method is called in the setter of properties that inherit this class.
          /// The CallerMemberName attribute that is applied to the optional propertyName
          /// causes the property name of the caller to be substituted as an argument
          /// </summary>
          /// <param name="propertyName"></param>
          protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
          {
               PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
          }
     }
}