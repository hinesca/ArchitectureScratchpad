using Physics;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Desktop
{
     public class PresentationCollection<T> : ObservableCollection<T>, IDisposable
     {
          public PresentationCollection(int refreshIntervalMs) : base()
          {
               PresentAsync(refreshIntervalMs);
          }

          private bool _disposed = false;
          public bool Instantiated { get; set; } = true;
          public bool Presenting { get; set; } = true;

          public override event NotifyCollectionChangedEventHandler CollectionChanged;

          protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
          {
               if (CollectionChanged != null)
                    CollectionChanged.Invoke(this, e);
          }

          private async void PresentAsync(int refreshIntervalMs)
          {
               while (Instantiated)
               {
                    while (Presenting)
                    {
                         await Task.Delay(refreshIntervalMs);
                         OnCollectionChanged(
                              new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    }
                    await Task.Delay(1000);
               }

               Dispose();
          }

          public void Dispose()
          {
               if (_disposed)
                    return;

               Presenting = false;
               Instantiated = false;
               Clear();
               _disposed = true;
          }
     }
}
