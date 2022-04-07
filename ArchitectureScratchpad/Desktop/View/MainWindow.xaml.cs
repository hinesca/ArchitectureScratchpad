using System.Windows;

namespace Desktop
{
     /// <summary>
     /// Interaction logic for MainWindow.xaml
     /// </summary>
     public partial class MainWindow : Window
     {
          public MainWindow()
          {
               InitializeComponent();
               DataContext = VM;
          }

          private MainVM VM = MainVM.Instance;

          private void Canvas_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
          {
               if (VM.ThrowSnowballCommand.CanExecute(null))
               {
                    Point mousePoint = e.GetPosition(Canvas);
                    VM.ThrowSnowballCommand.Execute(new Physics.Vector(mousePoint.X, mousePoint.Y, 0));
               }
          }

          private void Canvas_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
          {
               if (VM.UpdatePlayerTrajectoryCommand.CanExecute(null))
               {
                    Point mousePoint = e.GetPosition(Canvas);
                    VM.UpdatePlayerTrajectoryCommand.Execute(new Physics.Vector(mousePoint.X, mousePoint.Y, 0));
               }
          }
     }
}
