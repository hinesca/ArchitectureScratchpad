using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Desktop
{
     public class VMBase : INotifyPropertyChanged
     {
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
