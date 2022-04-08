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
               Player = new Player(PhysicalObjects);
               SwatBot = new SwatBot(Player, PhysicalObjects);
          }
          public static MainVM Instance { get; } = new MainVM();

          #endregion **************************************************************************************************
          #region ***************************************** Logic *****************************************************

          public RelayCommand UpdatePlayerTrajectoryCommand;

          private void UpdatePlayerTrajectory(object o)
          {
               Player.UpdateTrajectory(o as Vector);
          }

          public RelayCommand ThrowSnowballCommand;
          private void ThrowSnowball(object o)
          {
               Player.ThrowSnowball(o as Vector);
          }

          private bool CanUpdateThrowsnowball(object o)
          {
               return true; // Keep simple for now. This is equivalent to a null predicate. 
          }

          #endregion **************************************************************************************************
          #region ***************************************** Properties ************************************************

          public Player Player { get; }
          public Player SwatBot { get; }

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

          public RealTimeEngine PhysicalObjects { get; set; }
               = new RealTimeEngine(5);

          #endregion **************************************************************************************************
     }
}
