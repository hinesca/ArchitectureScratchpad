using System;
using System.Collections;
using System.Collections.Generic;

namespace Physics
{
     public class Vector : IEnumerable<double>
     {
          public Vector(double x = 0, double y = 0, double z = 0)
          {
               X = x;
               Y = y;
               Z = z;
          }

          private double[] _backingArray = new double[] { 0, 0, 0 };
          public double X
          {
               get { return _backingArray[0]; }
               set { _backingArray[0] = value; }
          }
          public double Y
          {
               get { return _backingArray[1]; }
               set { _backingArray[1] = value; }
          }
          public double Z
          {
               get { return _backingArray[2]; }
               set { _backingArray[2] = value; }
          }

          public double this[int index]
          {
               get { return _backingArray[index]; }
               set { _backingArray[index] = value; }
          }

          // TODO check the math, finish implementation
          public Vector Normalized()
          {
               return this * (1 / Magnitude);
          }

          public double Magnitude
          {
               get { return Math.Sqrt(X * X + Y * Y + Z * Z); }
          }

          public static Vector operator +(Vector v) => v;
          public static Vector operator -(Vector v) => new Vector(-v.X, -v.Y, -v.Z);
          public static Vector operator +(Vector v, Vector u)
              => new Vector(v.X + u.X, v.Y + u.Y, v.Z + u.Z);

          public static Vector operator -(Vector v, Vector u)
              => v + (-u);

          /// <summary>
          /// Dot product of two vectors
          /// </summary>
          /// <param name="v"></param>
          /// <param name="u"></param>
          /// <returns></returns>
          public static double operator *(Vector v, Vector u)
              => v.X * u.X + v.Y * u.Y + v.Z * u.Z;

          public static Vector operator *(Vector v, double s)
              => new Vector(v.X * s, v.Y * s, v.Z);

          public static Vector Cross(Vector v, Vector u)
          {
               throw new NotImplementedException("Cross product not implemented");
          }


          public override string ToString() => $"({X}, {Y}, {Z})";

          public IEnumerator GetEnumerator()
          {
               yield return _backingArray;
          }

          IEnumerator<double> IEnumerable<double>.GetEnumerator()
          {
               yield return X;
               yield return Y;
               yield return Z;
          }

          public void Clear()
          {
               _backingArray = new double[] { 0, 0, 0 };
          }
     }
}
