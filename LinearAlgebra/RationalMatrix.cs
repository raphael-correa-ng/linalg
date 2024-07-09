using LinAlg;

namespace Linalg
{
    public partial class RationalMatrix : AbstractMatrix<RationalMatrix, Rational>
    {
        public RationalMatrix()
            : base(new Rational[0, 0])
        {
        }

        public RationalMatrix(int rows, int columns)
            : base(new Rational[rows, columns])
        {
        }

        public RationalMatrix(Rational[,] matrix)
            : base(matrix)
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

        public override Rational Determinant()
        {
            if (!IsSquare())
                throw new ArgumentException("Only square matrices have a determinant");

            int N = Rows; // == Columns

            if (N == 1)
                return Data[0, 0];

            if (N == 2)
                return Data[0, 0] * Data[1, 1] - Data[0, 1] * Data[1, 0];

            Rational det = new Rational(0);

            for (int i = 0; i < N; i++)
            {
                Rational sign = new Rational((long)Math.Pow(-1, i));
                RationalMatrix subMatrix = GetBlock(1, 0, N - 1, i).Combine(GetBlock(1, i + 1, N - 1, N - 1 - i));
                Rational subDeterminant = subMatrix.Determinant();
                det += sign * Data[0, i] * subDeterminant;
            }

            return det;
        }
    }
}