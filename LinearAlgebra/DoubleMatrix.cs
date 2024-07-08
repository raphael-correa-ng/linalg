using LinAlg;

namespace Linalg
{
    public partial class DoubleMatrix: AbstractMatrix<DoubleMatrix, double>
    {
        public DoubleMatrix()
            : base(new double[0, 0])
        {
        }

        public DoubleMatrix(double[,] matrix)
            : base(matrix)
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
    }
}