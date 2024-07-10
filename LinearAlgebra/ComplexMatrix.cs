using LinAlg;

namespace Linalg
{
    public class ComplexMatrix: AbstractMatrix<ComplexMatrix, Complex>
    {
        public ComplexMatrix()
            : this(0, 0)
        {
        }

        public ComplexMatrix(int rows, int columns)
            : this(new Complex[rows, columns])
        {
        }

        public ComplexMatrix(Complex[,] matrix)
            : base(matrix, Complex.ZERO, Complex.ONE, new Complex(new Rational(-1)))
        {
        }

        protected override Complex AddComponent(Complex t0, Complex t1) => t0.Add(t1);

        protected override Complex SubComponent(Complex t0, Complex t1) => t0.Sub(t1);

        protected override Complex MulComponent(Complex t0, Complex t1) => t0.Mul(t1);

        protected override Complex DivComponent(Complex t0, Complex t1) => t0.Div(t1);
    }
}