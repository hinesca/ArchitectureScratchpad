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
               PhysicalObjects.Add(new SwatBot(Player, PhysicalObjects));
          }
          public static MainVM Instance { get; } = new MainVM();

          #endregion **************************************************************************************************
          #region ***************************************** Logic *****************************************************
          public RelayCommand UpdatePlayerTrajectoryCommand;

          private void UpdatePlayerTrajectory(object o)
          {
               STPosition target = o as STPosition;
               STPosition origin = new STPosition(Player.STPosition);
               double range = (target.Position - Player.STPosition.Position).Magnitude;
               double speed = Player.MaxPlayerSpeed;
               target.Time = DateTime.UtcNow.AddSeconds(range / speed);
               origin.Next = target;
               STPosition[] path = new STPosition[] { origin, target };
               Player.Trajectory = new Trajectory(path);
          }

          public RelayCommand ThrowSnowballCommand;
          private void ThrowSnowball(object o)
          {
               PhysicalObjects.Add(
                    SnowBall.ThrowSnowball(
                         new STPosition(Player.STPosition), o as STPosition, Player.MaxSnowballSpeed));
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

          private double _canvasWidth = 500;
          public double CanvasWidth
          {
               get { return _canvasWidth; }
               set
               { 
                    _canvasWidth = value;
                    NotifyPropertyChanged();
               }
          }

          private double _canvasHeight = 500;
          public double CanvasHeight
          {
               get { return _canvasHeight; }
               set
               { 
                    _canvasHeight = value;
                    NotifyPropertyChanged();
               }
          }

          public PresentationCollection PhysicalObjects { get; set; }
               = new PresentationCollection(10);

          #endregion **************************************************************************************************
     }
}
