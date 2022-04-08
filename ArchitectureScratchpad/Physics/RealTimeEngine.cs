using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Physics
{
     public class RealTimeEngine : List<IPhysicalObject>, INotifyCollectionChanged, IDisposable// TODO, IPhysicalObject
     {
          public RealTimeEngine(int refreshIntervalMs = 10) : base()
          {
               RefreshIntervalMs = refreshIntervalMs;
               Start();
          }

          private bool _disposed = false;
          public bool Running { get; set; }
          public int RefreshIntervalMs { get; set; }

          public event NotifyCollectionChangedEventHandler CollectionChanged;

          protected void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
          {
               if (CollectionChanged != null)
                    CollectionChanged.Invoke(this, e);
          }

          private async void Start()
          {
               Running = true;
               while (Running)
               {
                    await Task.Delay(RefreshIntervalMs);
                    // TODO Performance and deterministic enhancements by propagating now through static methods
                    DateTime now = DateTime.UtcNow;
                    Interact(ToArray(), now);
                    OnCollectionChanged(
                         new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
               }
               // Dispose(); TODO maybe we want to freeze for serialization? idk, think about it.
          }

          public static void Interact(IPhysicalObject[] collection, DateTime now)
          {
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
                              poi.Interact(poj, now);
                              poj.Interact(poi, now);
                         }
                    }
               }
          }

          public void Dispose()
          {
               if (_disposed)
                    return;

               Running = false;
               foreach (IPhysicalObject po in ToArray())
               {
                    po.Dispose(); // expected to remove from collection
               }
               Clear(); // redundant if above is true
               _disposed = true;
          }
     }
}
