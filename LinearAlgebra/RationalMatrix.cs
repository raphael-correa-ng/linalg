using LinAlg;

namespace Linalg
{
    public class RationalMatrix : AbstractMatrix<RationalMatrix, Rational>
    {
        public RationalMatrix()
            : this(0, 0)
        {
        }

        public RationalMatrix(int rows, int columns)
            : this(new Rational[rows, columns])
        {
        }

        public RationalMatrix(Rational[,] matrix)
            : base(matrix, Rational.ZERO, Rational.ONE, new Rational(-1))
        {
        }

        protected override Rational AddComponent(Rational t0, Rational t1) => t0.Add(t1);

        protected override Rational SubComponent(Rational t0, Rational t1) => t0.Sub(t1);

        protected override Rational MulComponent(Rational t0, Rational t1) => t0.Mul(t1);

        protected override Rational DivComponent(Rational t0, Rational t1) => t0.Div(t1);

        public static RationalMatrix operator +(RationalMatrix a, RationalMatrix b) => a.Add(b);

        public static RationalMatrix operator -(RationalMatrix a, RationalMatrix b) => a.Sub(b);

        public static RationalMatrix operator *(RationalMatrix a, RationalMatrix b) => a.Mul(b);

        public static RationalMatrix operator *(RationalMatrix a, Rational scalar) => a.Mul(scalar);

        public static RationalMatrix operator /(RationalMatrix a, Rational scalar) => a.Div(scalar);
    }
}