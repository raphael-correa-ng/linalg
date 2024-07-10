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
            : base(matrix, 0.0, 1.0, -1.0)
        {
        }

        protected override double AddComponent(double t0, double t1) => t0 + t1;

        protected override double SubComponent(double t0, double t1) => t0 - t1;

        protected override double MulComponent(double t0, double t1) => t0 * t1;

        protected override double DivComponent(double t0, double t1) => t0 / t1;
    }
}