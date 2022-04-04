using System;
using Physics;

namespace Desktop
{
     public class MainVM : VMBase
     {
          #region ***************************************** Constructors **********************************************
          private MainVM()
          {
               UpdatePlayerTrajectoryCommand = new RelayCommand(UpdatePlayerTrajectory);
               ThrowSnowballCommand = new RelayCommand(ThrowSnowball, CanUpdateThrowsnowball);
               Player = new Player();
               PhysicalObjects.Add(Player);
          }
          public static MainVM Instance { get; } = new MainVM();

          #endregion **************************************************************************************************
          #region ***************************************** Logic *****************************************************
          public RelayCommand UpdatePlayerTrajectoryCommand;

          private void UpdatePlayerTrajectory(object o)
          {
               STPosition target = o as STPosition;
               STPosition origin = new STPosition(Player.STPosition);
               double range = (target.Position - Player.STPosition.Position).Magnitude / 10;
               double speed = Player.MaxPlayerSpeed;
               target.Time = DateTime.UtcNow.AddSeconds(range / speed);
               //origin.Next = target;
               STPosition[] path = new STPosition[] { origin, target };
               Player.Trajectory = new Trajectory(path);
          }

          public RelayCommand ThrowSnowballCommand;
          private void ThrowSnowball(object o)
          {
               STPosition target = o as STPosition;
               STPosition origin = new STPosition(Player.STPosition);
               double range = (target.Position - Player.STPosition.Position).Magnitude / 10;
               double speed = Player.MaxSnowballSpeed;
               target.Time = DateTime.UtcNow.AddSeconds(range / speed);
               //origin.Next = target;
               STPosition[] path = new STPosition[] { origin, target };
               PhysicalObjects.Add(new SnowBall(path));
          }
          private bool CanUpdateThrowsnowball(object o)
          {
               return true; // Keep simple for now. This is equivalent to a null predicate. 
          }

          #endregion **************************************************************************************************
          #region ***************************************** Properties ************************************************
          private Player _player = new Player();
          public Player Player
          {
               get { return _player; }
               set
               { 
                    _player = value;
                    NotifyPropertyChanged();
               }
          }

          public double CanvasWidth { get; set; } = 500;
          public double CanvasHeight { get; set; } = 500;

          public PresentationCollection<IPhysicalObject> PhysicalObjects { get; set; }
               = new PresentationCollection<IPhysicalObject>(100);

          #endregion **************************************************************************************************
     }
}
