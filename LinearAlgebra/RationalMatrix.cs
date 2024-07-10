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
    }
}