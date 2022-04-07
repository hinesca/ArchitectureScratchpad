using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Physics
{
     public class PresentationCollection : List<IPhysicalObject>, INotifyCollectionChanged, IDisposable //ObservableCollection<IPhysicalObject>
     {
          public PresentationCollection(int refreshIntervalMs) : base()
          {
               Start(refreshIntervalMs);
          }

          private bool _disposed = false;
          public bool Instantiated { get; set; } = true;
          public bool Presenting { get; set; } = true;

          public event NotifyCollectionChangedEventHandler CollectionChanged;

          protected  void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
          {
               if (CollectionChanged != null)
                    CollectionChanged.Invoke(this, e);
          }

          private async void Start(int refreshIntervalMs)
          {
               while (Instantiated)
               {
                    while (Instantiated && Presenting)
                    {
                         await Task.Delay(refreshIntervalMs);
                         HitTest(ToArray());
                         OnCollectionChanged(
                              new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    }
                    await Task.Delay(1000);
               }
               Dispose();
          }

          public static void HitTest(IPhysicalObject[] collection) // TODO Move this somewhere else
          {
               DateTime now = DateTime.UtcNow;
               for (int i = 0; i < collection.Length; i++)
               {
                    IPhysicalObject poi = collection[i];
                    if (now > poi.EOL)
                    {
                         poi.Dispose();
                         continue;
                    }

                    Vector poiPos = poi.STPosition.Position;
                    for (int j = i + 1; j < collection.Length; j++)
                    {
                         IPhysicalObject poj = collection[j];
                         Vector pojPos = poj.STPosition.Position;
                         if ((pojPos - poiPos).Magnitude < poi.UncertaintyS + poj.UncertaintyS)
                         {
                              poi.CollideWith(poj);
                              poj.CollideWith(poi);
                         }
                    }
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
