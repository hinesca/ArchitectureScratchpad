using System;
using System.Collections.Generic;
using System.Text;

namespace Physics
{
     public class Random
     {
          // this is a singleton
          private Random()
          { }
          public static Random Instance { get; } = new Random();

          private static readonly System.Random _random = new System.Random();

          public double Randouble(double pmRange)
          {
               return (-1 + 2 * _random.NextDouble()) * pmRange;
          }

          public int Next(int max)
          {
               return _random.Next(max);
          }

          public int Next(int min, int max)
          {
               return _random.Next(min, max);
          }
     }
}
