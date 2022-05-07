using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Physics
{
     public class RealTimeEngine : List<IPhysicalObject>, IPhysicalObject, INotifyCollectionChanged, IDisposable
     {
          public RealTimeEngine(int refreshIntervalMs = 10) : base()
          {
               RefreshIntervalMs = refreshIntervalMs;

               // Each player will exist in one RTE
               // and RTEs merge on interaction (overlapping uncertainty). TODO make configurable
               // The primary purpose of uncertainty is to help mitigate latency issues.
               UncertaintyS = 10000;
               UncertaintyT = 1;
               EOL = DateTime.MaxValue;
               Start();
          }

          private bool _disposed = false;
          public bool Running { get; set; }
          public int RefreshIntervalMs { get; set; }

          // IPhysicalObject
          public Trajectory Trajectory { get; set; }
          public double UncertaintyS { get; set; }
          public double UncertaintyT { get; set; }
          public object Sprite { get; set; }
          public DateTime EOL { get; set; }
          public IPhysicalObject Parent { get; set; }
          public RealTimeEngine RTEngine { get; set; }

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

                    // now is set once and propagates through static methods
                    DateTime now = DateTime.UtcNow;
                    Step();
                    OnCollectionChanged(
                         new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
               }
          }

          public void Interact(IPhysicalObject po, DateTime now)
          {
               if (po.GetType() == typeof(RealTimeEngine))
               {
                    AddRange((RealTimeEngine)po);
                    po.Dispose();
               }
               else
               {
                    Add(po);
                    Interact(this, now);
               }
          }

          private void Step()
          {
               DateTime now = DateTime.UtcNow;

               for (int i = 0; i < Count; i++)
               {
                    IPhysicalObject poi = this[i];

                    // The spacial uncertainty of a real time engine is the magnitude of the furthest point from the center of this RTE
                    // or 10,000 units (pixels in the most simple context). TODO Make this 10,000 unit minimum uncertainty configurable.
                    double posMag = poi.Trajectory.GetPosition(now).S.Magnitude;
                    if (posMag > UncertaintyS)
                    {
                         UncertaintyS = posMag;
                    }

                    if (now > poi.EOL)
                    {
                         poi.Dispose();
                         continue;
                    }

                    Vector poiPos = poi.Trajectory.GetPosition(now).S;
                    for (int j = i + 1; j < Count; j++)
                    {
                         IPhysicalObject poj = this[j];
                         Vector pojPos = poj.Trajectory.GetPosition(now).S;
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

          public SpaceTimePos GetPosition(DateTime now)
          {
               throw new NotImplementedException();
          }
     }
}
