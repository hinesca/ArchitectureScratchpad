using System;
using System.Collections.Generic;

namespace Physics
{
     public class Player : PhysicalObjectBase
     {
          public Player(RealTimeEngine presenter) : base(presenter)
          {
               List<SpaceTimePos> path =
                    new List<SpaceTimePos>
                    {
                         new SpaceTimePos(new Vector(250, 250, 0)),
                         new SpaceTimePos(new Vector(250, 250, 0), DateTime.UtcNow + TimeSpan.FromSeconds(1))
                    };
               Trajectory = new Trajectory(path);
               UncertaintyS = 10;
               PlayerSpeed = 100;
               SnowballSpeed = 500;
               SnowballMaxRange = 500;
               SnowballError = 0.1;
               EOL = DateTime.MaxValue;
               Sprite = '\u2603';
               RTEngine.Add(this);
               Hit += OnHit;
          }

          protected static readonly Random _random = Random.Instance;

          public double PlayerSpeed { get; set; }
          public double SnowballSpeed { get; set; }
          public double SnowballError { get; set; }
          public double SnowballMaxRange { get; set; }
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

          public void ThrowSnowball(Vector target, DateTime now)
          {
               SpaceTimePos originSTP = Trajectory.GetPosition(now);
               Vector origin = originSTP.S;
               double range = (target - origin).Magnitude;
               if (range > SnowballMaxRange)
               {
                    range = SnowballMaxRange;
                    target = origin + (target - origin).Unit() * range;
               }
               Vector accuracyModifier
                    = new Vector(_random.Double(), _random.Double()).Unit() * SnowballError * range;
               target = target + accuracyModifier;
               SpaceTimePos targetSTP = new SpaceTimePos(target, now.AddSeconds(range / SnowballSpeed));
               List<SpaceTimePos> path = new List<SpaceTimePos> { originSTP, targetSTP };
               RTEngine.Add(new SnowBall(this, path));
          }

          public void MoveTo(Vector target, DateTime now, double speed = 0)
          {
               if (speed == 0)
                    speed = PlayerSpeed;
               
               SpaceTimePos origin = Trajectory.GetPosition(now);
               double range = (target - origin.S).Magnitude;

               DateTime tot = now.AddSeconds(range / speed);
               List<SpaceTimePos> path = new List<SpaceTimePos> { origin, new SpaceTimePos(target, tot) };
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

                    Player aggressor = snowBall.Origin as Player;
                    aggressor.OutgoingHits++;
                    IncomingHits++;
                    Vector bump = snowBall.GetVelocity(now).Unit() * 20;

                    MoveTo(GetPosition(now).S + bump, now, PlayerSpeed * 2);
                    base.Interact(collider, now);
                    snowBall.Dispose();
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
