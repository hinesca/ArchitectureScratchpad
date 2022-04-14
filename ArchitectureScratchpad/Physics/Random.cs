using System;
using System.Collections.Generic;
using System.Text;

namespace Physics
{
     public class Random : System.Random
     {
          // this is a singleton
          private Random()
          { }
          public static Random Instance { get; } = new Random();

          public double Double(double pmRange = 1)
          {
               return (-1 + 2 * NextDouble()) * pmRange;
          }
     }
}
