using Physics;
using System;

namespace Desktop
{
     public class PlayerVM : VMBase
     {
          //public PlayerVM(Player player)
          //{
          //     Player = player;
          //}
          //public static DateTime BirthTime = DateTime.UtcNow;

          //public Player Player { get; }
          //public Trajectory Trajectory { get { return Player.Trajectory; } }
          //public STPosition STPosition { get { return Player.STPosition; } }

          //public int IncomingHits
          //{ 
          //     get
          //     {
          //          return Player.IncomingHits;
          //     }
          //     set
          //     {
          //          Player.IncomingHits = value;
          //     }
          //}

          //public int OutgoingHits
          //{
          //     get
          //     {
          //          return Player.OutgoingHits;
          //     }
          //     set
          //     {
          //          Player.OutgoingHits = value;
          //     }
          //}
          //public int Score
          //{
          //     get
          //     {
          //          int score = OutgoingHits - IncomingHits;
          //          if (score < 0)
          //          {
          //               OutgoingHits = 0;
          //               IncomingHits = 0;
          //               BirthTime = DateTime.UtcNow;
          //               return 0;
          //          }
          //          else return score;
          //     }
          //}
          //public double Effectiveness
          //{
          //     get
          //     {
          //          double value = 10 * Score / (DateTime.UtcNow - BirthTime).TotalSeconds;
          //          Player.UncertaintyS = 10 + value; // bigger target based on effectiveness
          //          return value;
          //     }
          //}

          //private void OnHit(object sender, EventArgs args)
          //{
          //     NotifyPropertyChanged(nameof(Score));
          //     NotifyPropertyChanged(nameof(Effectiveness));
          //}

          //public void ThrowSnowball(Vector target) // TODO fix lazy implementation
          //{
          //     Player.ThrowSnowball(target);
          //}

          //public void UpdateTrajectory(Vector target)
          //{
          //     Player.UpdateTrajectory(target);
          //}
     }
}
