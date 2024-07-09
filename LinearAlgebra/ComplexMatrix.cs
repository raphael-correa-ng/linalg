using LinAlg;

namespace Linalg
{
    public class ComplexMatrix: AbstractMatrix<ComplexMatrix, Complex>
    {
        public ComplexMatrix()
            : base(new Complex[0, 0])
        {
        }

        public ComplexMatrix(int rows, int columns)
            : base(new Complex[rows, columns])
        {
        }

        public ComplexMatrix(Complex[,] matrix)
            : base(matrix)
        {
        }

        protected override Complex AddComponent(Complex t0, Complex t1) => t0.Add(t1);

        protected override Complex SubComponent(Complex t0, Complex t1) => t0.Sub(t1);

        protected override Complex MulComponent(Complex t0, Complex t1) => t0.Mul(t1);

        protected override Complex DivComponent(Complex t0, Complex t1) => t0.Div(t1);

        public static ComplexMatrix operator +(ComplexMatrix a, ComplexMatrix b) => a.Add(b);

        public static ComplexMatrix operator -(ComplexMatrix a, ComplexMatrix b) => a.Sub(b);

        public static ComplexMatrix operator *(ComplexMatrix a, ComplexMatrix b) => a.Mul(b);

        public static ComplexMatrix operator *(ComplexMatrix a, Complex scalar) => a.Mul(scalar);

        public static ComplexMatrix operator /(ComplexMatrix a, Complex scalar) => a.Div(scalar);

        public override Complex Determinant()
        {
            if (!IsSquare())
                throw new ArgumentException("Only square matrices have a determinant");

            int N = Rows; // == Columns

            if (N == 1)
                return Data[0, 0];

            if (N == 2)
                return Data[0, 0] * Data[1, 1] - Data[0, 1] * Data[1, 0];

            Complex det = new Complex(new Rational(0));

            for (int i = 0; i < N; i++)
            {
                Complex sign = new Complex(new Rational((long)Math.Pow(-1, i)));
                ComplexMatrix subMatrix = GetBlock(1, 0, N - 1, i).Combine(GetBlock(1, i + 1, N - 1, N - 1 - i));
                Complex subDeterminant = subMatrix.Determinant();
                det += sign * Data[0, i] * subDeterminant;
            }

            return det;
        }

        public override ComplexMatrix Identity()
        {
            if (!IsSquare())
                throw new ArgumentException("Non-square matrices do not have an identity");

            int N = Rows; // == Columns

            Complex[,] identity = new Complex[N, N];

            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    identity[i, j] = new Complex(i == j ? Rational.ONE : Rational.ZERO);

            return new ComplexMatrix(identity);
        }
    }
}