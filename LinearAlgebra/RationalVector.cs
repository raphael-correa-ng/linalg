using System.Collections;

namespace Linalg
{
    public class RationalVector
    {
        private readonly Rational[] Vector;

        public int Dimension { get; } = 0;

        public RationalVector(int n) 
            : this(GetNewVector(n))
        {
        }

        public RationalVector(IEnumerable<Rational> vector) {
            this.Vector = vector.ToArray();
            Dimension = this.Vector.Length;
        }

        public Rational this[int i]
        {
            get { return Vector[i]; }
        }

        public IEnumerator GetEnumerator()
        {
            return Vector.GetEnumerator();
        }

        public RationalVector Add(RationalVector that)
        {
            AssertLength(this, that, "Cannot add vectors of different dimensions");

            return new RationalVector(Vector.Zip(that.Vector, (a, b) => a + b));
        }

        public RationalVector Sub(RationalVector that)
        {
            AssertLength(this, that, "Cannot subtract vectors of different dimensions");

            return new RationalVector(Vector.Zip(that.Vector, (a, b) => a - b));
        }

        public RationalVector Mul(Rational scalar)
        {
            return new RationalVector(Vector.Select(r => r * scalar));
        }

        public RationalVector Div(Rational scalar)
        {
            return new RationalVector(Vector.Select(r => r / scalar));
        }

        public RationalVector PointWiseMul(RationalVector that)
        {
            return new RationalVector(Vector.Zip(that.Vector, (a, b) => a * b));
        }

        public RationalVector PointWiseDiv(RationalVector that)
        {
            return new RationalVector(Vector.Zip(that.Vector, (a, b) => a / b));
        }

        public Rational DotProd(RationalVector that)
        {
            AssertLength(this, that, "Cannot compute dot product of vectors of different dimensions");
            
            return Vector.Zip(that.Vector, (a, b) => a * b)
                .Aggregate(Rational.ZERO, (result, next) => result.Add(next));
        }

        public RationalVector Normalize()
        {
            return this / Length();
        }

        public Rational Length()
        {
            return DotProd(this).Sqrt();
        }

        private static Rational[] GetNewVector(int n)
        {
            Rational[] vector = new Rational[n];
            for (int i = 0; i < n; i++)
            {
                vector[i] = Rational.ZERO;
            }
            return vector;
        }

        private static void AssertLength(RationalVector a, RationalVector b, String message)
        {
            if (a.Dimension != b.Dimension)
                throw new ArgumentException(message);
        }

        public override int GetHashCode()
        {
            return Vector
                .Select(r => r.GetHashCode())
                .Aggregate(17, (result, next) => result + 31 * result + next);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is RationalVector))
                return false;

            RationalVector that = (RationalVector)obj;

            if (Dimension != that.Dimension)
                return false;

            for (int i = 0; i < Dimension; i++)
                if (this[i] != that[i])
                    return false;

            return true;
        }

        public override string ToString()
        {
            return "[" + string.Join(", ", (object[])Vector) + "]";
        }

        public static bool operator ==(RationalVector a, RationalVector b) =>
            a.Equals(b);

        public static bool operator !=(RationalVector a, RationalVector b) =>
           !a.Equals(b);

        public static RationalVector operator +(RationalVector a, RationalVector b) =>
            a.Add(b);

        public static RationalVector operator -(RationalVector a, RationalVector b) =>
            a.Sub(b);

        public static Rational operator *(RationalVector a, RationalVector b) =>
            a.DotProd(b);

        public static RationalVector operator *(RationalVector a, Rational scalar) =>
            a.Mul(scalar);

        public static RationalVector operator /(RationalVector a, Rational scalar) =>
            a.Div(scalar);

        public static explicit operator double[](RationalVector a) =>
            a.Vector.Select(r => (double)r).ToArray();
    }
}
