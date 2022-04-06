using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace Physics
{
     public class PresentationCollection : ObservableCollection<IPhysicalObject>, IDisposable
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
                    int i = 0;
                    while (Presenting)
                    {
                         if (i == 10)
                         {
                              ClearDeadWood();
                              i = 0;
                         }
                         await Task.Delay(refreshIntervalMs);
                         OnCollectionChanged(
                              new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                         i++;
                    }
                    await Task.Delay(1000);
               }

               Dispose();
          }

          private void ClearDeadWood()
          {
               DateTime now = DateTime.UtcNow;
               IPhysicalObject[] items = Items.ToArray();
               foreach (IPhysicalObject po in items)
               {
                    if (now > po.EOL)
                         Remove(po);
               }
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
