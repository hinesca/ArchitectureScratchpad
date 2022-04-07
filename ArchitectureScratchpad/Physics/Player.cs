using System;
using System.Collections.Generic;

namespace Physics
{
     public class Player : PhysicalObjectBase
     {
          public Player(PresentationCollection presenter) : base(presenter)
          {
               List<STPosition> path =
                    new List<STPosition>
                    {
                         new STPosition(new Vector(250, 250, 0)),
                         new STPosition(new Vector(250, 250, 0), DateTime.UtcNow + TimeSpan.FromSeconds(1))
                    };
               Trajectory = new Trajectory(path);
               UncertaintyS = 20;
               EOL = DateTime.MaxValue;
               Sprite = '\u2603';
               Presenter.Add(this);

               Hit += OnHit;
          }

          protected static readonly Random _random = Random.Instance;

          public double MaxPlayerSpeed { get; set; } = 30;
          public double SnowballSpeed { get; set; } = 200;
          public double SnowballError { get; set; } = 0.1;
          public DateTime BirthTime { get; set; } = DateTime.UtcNow;

          private double _uncertaintyS;
          public override double UncertaintyS
          { 
               get { return _uncertaintyS; }
               set
               {
                    _uncertaintyS = value;
                    NotifyPropertyChanged();
               }
          }

          public int IncomingHits { get; set; }
          public int OutgoingHits { get; set; }
          public int Score
          {
               get
               {
                    int score = OutgoingHits - IncomingHits;
                    if (score < 0)
                    {
                         OutgoingHits = 0;
                         IncomingHits = 0;
                         BirthTime = DateTime.UtcNow;
                         return 0;
                    }
                    else return score;
               }
          }
          public double Effectiveness
          {
               get
               {
                    DateTime now = DateTime.UtcNow;
                    double value = 0;
                    if (now - BirthTime < TimeSpan.FromSeconds(10))
                         return value;

                    value = Score / (1 + (now - BirthTime).TotalSeconds / 10);
                    UncertaintyS = 10 + value; // bigger target based on effectiveness
                    return value;
               }
          }

          public double SpriteSize
          {
               get
               {
                    return UncertaintyS;
               }
          }

          public void ThrowSnowball(Vector target)
          {
               SnowBall.ThrowSnowball(this, target, SnowballSpeed, SnowballError);
               NotifyPropertyChanged(nameof(Score));
               NotifyPropertyChanged(nameof(Effectiveness));
          }

          public void UpdateTrajectory(Vector target) // TODO move to PhysicalObjectBase, AddRange(path), and check/enforce speed
          {
               STPosition origin = new STPosition(STPosition);
               double range = (target - origin.Position).Magnitude;
               double speed = MaxPlayerSpeed;
               DateTime tot = DateTime.UtcNow.AddSeconds(range / speed);
               List<STPosition> path = new List<STPosition> { origin, new STPosition(target, tot) };
               Trajectory = new Trajectory(path);
          }

          public override void CollideWith(IPhysicalObject collider)
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
                    base.CollideWith(collider);
               }
          }

          private void OnHit(object sender = null, EventArgs args = null) // TODO do this differently
          {
               NotifyPropertyChanged(nameof(IncomingHits));
               NotifyPropertyChanged(nameof(OutgoingHits));
               NotifyPropertyChanged(nameof(Score));
               NotifyPropertyChanged(nameof(Effectiveness));
               NotifyPropertyChanged(nameof(UncertaintyS));
               NotifyPropertyChanged(nameof(SpriteSize));
          }
     }
}
