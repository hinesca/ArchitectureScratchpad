using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Physics
{
     public class PhysicalObjectBase : IPhysicalObject, IDisposable, INotifyPropertyChanged
     {
          public PhysicalObjectBase(PresentationCollection presenter)
          {
               Presenter = presenter;
          }

          public event EventHandler Hit;

          STPosition _stPosition;
          public STPosition STPosition
          {
               get
               {
                    DateTime now = DateTime.UtcNow;
                    if (_stPosition == null || _stPosition.Time < now + TimeSpan.FromMilliseconds(10))
                         _stPosition = Trajectory.GetPosition();
                    _stPosition.UncertaintyS = UncertaintyS;
                    _stPosition.UncertaintyTms = UncertaintyTms;
                    return _stPosition;
               }
          }

          public virtual void CollideWith(IPhysicalObject o)
          {
               if (o.Equals(this))
                    return;

               EventArgs args = new EventArgs();
               if (Hit != null)
                    Hit.Invoke(this, args);
          }

          public bool _disposed = false;
          public void Dispose()
          {
               if (_disposed)
                    return;
               Presenter.Remove(this);
               EOL = DateTime.UtcNow - TimeSpan.FromMilliseconds(100);
               _disposed = true;
          }

          public double ViewportX
          {
               get { return STPosition.Position.X; }
          }

          public double ViewportY
          {
               get { return STPosition.Position.Y; }
          }

          public Trajectory Trajectory { get; set; }
          public virtual double UncertaintyS { get; set; } = 1;
          public double UncertaintyTms { get; set; } = 10;
          public DateTime EOL { get; set; }
          public object Sprite { get; set; }
          public IPhysicalObject Parent { get; set; }
          public PresentationCollection Presenter { get; set; }

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