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
               DataContext = MainVM.Instance;
          }

          private void Canvas_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
          {
               // TODO: This breaks MVVM. Figure out how to get the xy from mouse into a command parameter in the XAML.
               MainVM vm = DataContext as MainVM;
               vm.
          }

          private void Canvas_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
          {

          }
     }
}
