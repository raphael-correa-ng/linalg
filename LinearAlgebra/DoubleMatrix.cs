using LinAlg;

namespace Linalg
{
    public class DoubleMatrix: AbstractMatrix<DoubleMatrix, double>
    {
        public DoubleMatrix()
            : this(0, 0)
        {
        }

        public DoubleMatrix(int rows, int cols)
            : this(new double[0, 0])
        {
        }

        public DoubleMatrix(double[,] matrix)
            : base(matrix, 0.0, 1.0)
        {
        }

        protected override double AddComponent(double t0, double t1) => t0 + t1;

        protected override double SubComponent(double t0, double t1) => t0 - t1;

        protected override double MulComponent(double t0, double t1) => t0 * t1;

        protected override double DivComponent(double t0, double t1) => t0 / t1;

        public static DoubleMatrix operator +(DoubleMatrix a, DoubleMatrix b) => a.Add(b);

        public static DoubleMatrix operator -(DoubleMatrix a, DoubleMatrix b) => a.Sub(b);

        public static DoubleMatrix operator *(DoubleMatrix a, DoubleMatrix b) => a.Mul(b);

        public static DoubleMatrix operator *(DoubleMatrix a, double scalar) => a.Mul(scalar);

        public override double Determinant()
        {
            if (!IsSquare())
                throw new ArgumentException("Only square matrices have a determinant");

            int N = Rows; // == Columns

            if (N == 1)
                return Data[0, 0];

            if (N == 2)
                return Data[0, 0] * Data[1, 1] - Data[0, 1] * Data[1, 0];

            double det = 0.0;

            for (int i = 0; i < N; i++)
            {
                double sign = Math.Pow(-1, i);
                DoubleMatrix subMatrix = GetBlock(1, 0, N - 1, i).Combine(GetBlock(1, i + 1, N - 1, N - 1 - i));
                double subDeterminant = subMatrix.Determinant();
                det += sign * Data[0, i] * subDeterminant;
            }

            return det;
        }
    }
}