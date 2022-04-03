using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
          }
          public static MainVM Instance { get; } = new MainVM();

          #endregion **************************************************************************************************
          #region ***************************************** Logic *****************************************************
          public RelayCommand UpdatePlayerTrajectoryCommand;

          private void UpdatePlayerTrajectory(object o)
          {
               STPosition target = o as STPosition;
               double range = (target.Position - Player.STPosition.Position).Magnitude;
               double speed = Player.MaxPlayerSpeed;
               target.Time = DateTime.UtcNow.AddSeconds(speed / range);
               STPosition[] path = new STPosition[] { Player.STPosition, target };
               Player.Trajectory = new Trajectory(path);
          }

          public RelayCommand ThrowSnowballCommand;
          private void ThrowSnowball(object o)
          {
               STPosition target = o as STPosition;
               double range = (target.Position - Player.STPosition.Position).Magnitude;
               double speed = Player.MaxSnowballSpeed;
               target.Time = DateTime.UtcNow.AddSeconds(speed / range);
               STPosition[] path = new STPosition[] { Player.STPosition, target };
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

          Canvas Canvas { get; set; }

          public ObservableCollection<IPhysicalObject> PhysicalObjects { get; set; }
               = new ObservableCollection<IPhysicalObject>();

          #endregion **************************************************************************************************
     }
}
