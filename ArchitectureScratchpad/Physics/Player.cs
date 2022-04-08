using System;
using System.Collections.Generic;

namespace Physics
{
     public class Player : PhysicalObjectBase
     {
          public Player(RealTimeEngine presenter) : base(presenter)
          {
               List<STPosition> path =
                    new List<STPosition>
                    {
                         new STPosition(new Vector(250, 250, 0)),
                         new STPosition(new Vector(250, 250, 0), DateTime.UtcNow + TimeSpan.FromSeconds(1))
                    };
               Trajectory = new Trajectory(path);
               UncertaintyS = 10;
               EOL = DateTime.MaxValue;
               Sprite = '\u2603';
               Presenter.Add(this);
               Hit += OnHit;
          }

          protected static readonly Random _random = Random.Instance;

          public double PlayerSpeed { get; set; } = 30;
          public double SnowballSpeed { get; set; } = 200;
          public double SnowballError { get; set; } = 0.1;
          public int IncomingHits { get; set; }
          public int OutgoingHits { get; set; }
          public int Score
          {
               get
               {
                    int score = OutgoingHits - IncomingHits;
                    if (score < -10)
                    {
                         // mercy reset
                         score = 0;
                         OutgoingHits = 0;
                         IncomingHits = 0;
                    }
                    else if (score < 0)
                    {
                         score = 0;
                    }
                    SpriteSize = 10 + score; // bigger target based on score
                    return score;
               }
          }

          public double SpriteSize
          {
               get
               {
                    return UncertaintyS;
               }
               set
               {
                    UncertaintyS = value;
                    NotifyPropertyChanged();
               }
          }

          public void ThrowSnowball(Vector target)
          {
               SnowBall.ThrowSnowball(this, target, SnowballSpeed, SnowballError);
          }

          public void UpdateTrajectory(Vector target) // TODO move to PhysicalObjectBase, AddRange(path), and check/enforce speed
          {
               STPosition origin = new STPosition(STPosition);
               double range = (target - origin.Position).Magnitude;
               double speed = PlayerSpeed;
               DateTime tot = DateTime.UtcNow.AddSeconds(range / speed);
               List<STPosition> path = new List<STPosition> { origin, new STPosition(target, tot) };
               Trajectory = new Trajectory(path);
          }

          public override void Interact(IPhysicalObject collider, DateTime now)
          {
               if (collider == this)
                    return;

               if (collider.GetType() == typeof(SnowBall))
               {
                    SnowBall snowBall = collider as SnowBall;
                    if (snowBall.Origin == this)
                         return;

                    Player opposingPlayer = snowBall.Origin as Player;
                    opposingPlayer.OutgoingHits++;
                    IncomingHits++;
                    snowBall.Dispose();
                    base.Interact(collider, now);
                    OnHit(); // TODO smells?
               }
          }

          protected void OnHit(object sender = null, EventArgs args = null) // TODO smells?
          {
               NotifyPropertyChanged(nameof(IncomingHits));
               NotifyPropertyChanged(nameof(OutgoingHits));
               NotifyPropertyChanged(nameof(Score));
               NotifyPropertyChanged(nameof(SpriteSize));
          }
     }
}
