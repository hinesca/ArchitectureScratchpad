using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
               Player.Trajectory = new Trajectory(TrajectoryType.Linear, Player.Position, Pla);
          }

          public RelayCommand ThrowSnowballCommand;
          private void ThrowSnowball(object o)
          {

          }
          private bool CanUpdateThrowsnowball(object o)
          {
               return true; // Keep simple for now. This is equivalent to a null predicate. 
          }

          #endregion **************************************************************************************************
          #region ***************************************** Properties ************************************************
          private Player _player;
          public Player Player
          {
               get { return _player; }
               set
               { 
                    _player = value;
                    NotifyPropertyChanged();
               }
          }

          public ObservableCollection<IPhysicalObject> PhysicalObjects { get; set; }
               = new ObservableCollection<IPhysicalObject>();

          #endregion **************************************************************************************************
     }
}
