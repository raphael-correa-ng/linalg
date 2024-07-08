using System.Collections;

namespace Linalg
{
    public partial class Vector
    {
        private readonly Rational[] vector;
        public int Dimension { get; } = 0;

        public Vector(int n) 
            : this(GetNewVector(n))
        {
        }

        public Vector(IEnumerable<Rational> vector) {
            this.vector = vector.ToArray();
            Dimension = this.vector.Length;
        }

        public Rational this[int i]
        {
            get { return vector[i]; }
        }

        public IEnumerator GetEnumerator()
        {
            return vector.GetEnumerator();
        }

        public Vector Add(Vector that)
        {
            AssertLength(this, that, "Cannot add vectors of different dimensions");

            return new Vector(vector.Zip(that.vector, (a, b) => a + b));
        }

        public Vector Sub(Vector that)
        {
            AssertLength(this, that, "Cannot subtract vectors of different dimensions");

            return new Vector(vector.Zip(that.vector, (a, b) => a - b));
        }

        public Vector Mul(Rational scalar)
        {
            return new Vector(vector.Select(r => r * scalar));
        }

        public Vector Div(Rational scalar)
        {
            return new Vector(vector.Select(r => r / scalar));
        }

        public Rational DotProd(Vector that)
        {
            AssertLength(this, that, "Cannot compute dot product of vectors of different dimensions");
            
            return vector.Zip(that.vector, (a, b) => a * b)
                .Aggregate(Rational.ZERO, (result, next) => result.Add(next));
        }

        public Vector Normalize()
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

        private static void AssertLength(Vector a, Vector b, String message)
        {
            if (a.Dimension != b.Dimension)
                throw new ArgumentException(message);
        }

        public override int GetHashCode()
        {
            return vector
                .Select(r => r.GetHashCode())
                .Aggregate(17, (result, next) => result + 31 * result + next);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Vector))
                return false;

            Vector that = (Vector)obj;

            if (Dimension != that.Dimension)
                return false;

            for (int i = 0; i < Dimension; i++)
                if (this[i] != that[i])
                    return false;

            return true;
        }

        public override string ToString()
        {
            return "[" + string.Join(", ", (object[])vector) + "]";
        }

        public static bool operator ==(Vector a, Vector b) =>
            a.Equals(b);

        public static bool operator !=(Vector a, Vector b) =>
           !a.Equals(b);

        public static Vector operator +(Vector a, Vector b) =>
            a.Add(b);

        public static Vector operator -(Vector a, Vector b) =>
            a.Sub(b);

        public static Rational operator *(Vector a, Vector b) =>
            a.DotProd(b);

        public static Vector operator *(Vector a, Rational scalar) =>
            a.Mul(scalar);

        public static Vector operator /(Vector a, Rational scalar) =>
            a.Div(scalar);

        public static explicit operator double[](Vector a) =>
            a.vector.Select(r => (double)r).ToArray();
    }
}
