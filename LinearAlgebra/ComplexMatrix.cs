﻿using LinAlg;

namespace Linalg
{
    public partial class ComplexMatrix: AbstractMatrix<ComplexMatrix, Complex>
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
    }
}