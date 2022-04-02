using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

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

          private double[] _backingArray = new double[] {0, 0, 0};
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

          public int Count => throw new NotImplementedException();

          public bool IsSynchronized => throw new NotImplementedException();

          public object SyncRoot => throw new NotImplementedException();

          public bool IsReadOnly => throw new NotImplementedException();

          public static Vector operator +(Vector a) => a;
          public static Vector operator -(Vector a) => new Vector(-a.X, -a.Y, -a.Z);
          public static Vector operator +(Vector a, Vector b)
              => new Vector(a.X + b.X, b.Y + a.Y, a.Z + b.Z);

          public static Vector operator -(Vector a, Vector b)
              => a + (-b);

          public static Vector operator *(Vector a, Vector b)
              => throw new NotImplementedException();//new Vector(a.X * b.X, a.Y * b.Y, a.Z * a.Z);

          public static Vector operator *(Vector a, double s)
              => new Vector(a.X * s, a.Y * s, a.Z);


          public override string ToString() => $"({X}, {Y}, {Z})";

          public IEnumerator GetEnumerator()
          {
               yield return X;
               yield return Y;
               yield return Z;
          }

          public void Clear()
          {
               X = 0;
               Y = 0;
               Z = 0;
          }

          IEnumerator<double> IEnumerable<double>.GetEnumerator()
          {
               yield return X;
               yield return Y;
               yield return Z;
          }
     }
}
