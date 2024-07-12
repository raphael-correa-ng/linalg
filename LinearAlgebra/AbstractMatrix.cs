using System.Text;

namespace Linalg
{
    public abstract class AbstractMatrix<Matrix, T> 
        where Matrix : AbstractMatrix<Matrix, T>, new() 
        where T : IComparable<T>
    {
        public T[,] Data { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        private T Zero;
        private T One;
        private T NegativeOne;

        public AbstractMatrix(T[,] data, T zero, T one, T negativeOne)
        {
            Set(data);
            Zero = zero;
            One = one;
            NegativeOne = negativeOne;
        }

        public T this[int i, int j]
        {
            get { return Data[i, j]; }
            internal set { Data[i, j] = value; }
        }

        protected abstract T AddComponent(T t0, T t1);

        protected abstract T SubComponent(T t0, T t1);

        protected abstract T MulComponent(T t0, T t1);

        protected abstract T DivComponent(T t0, T t1);

        public Matrix Add(AbstractMatrix<Matrix, T> that)
        {
            if (!IsSameDimensionsAs(that))
                throw new ArgumentException("Cannot add matrices of different dimensions.");

            T[,] result = new T[Rows, Columns];

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    result[i, j] = AddComponent(this[i, j], that[i, j]);

            return new Matrix().Set(result);
        }

        public Matrix Sub(AbstractMatrix<Matrix, T> that)
        {
            if (!IsSameDimensionsAs(that))
                throw new ArgumentException("Cannot add matrices of different dimensions.");

            T[,] result = new T[Rows, Columns];

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    result[i, j] = SubComponent(this[i, j], that[i, j]);

            return new Matrix().Set(result);
        }

        public Matrix Mul(AbstractMatrix<Matrix, T> that)
        {
            if (!CanMultiplyWith(that))
                throw new ArgumentException("Cannot multiply matrices of such dimensions.");

            T[,] result = new T[Rows, that.Columns];

            for (int i = 0; i < that.Columns; i++)
                for (int j = 0; j < Columns; j++)
                    for (int k = 0; k < Rows; k++)
                    {
                        T toAdd = MulComponent(this[k, j], that[j, i]);
                        result[k, i] = result[k, i] == null ? toAdd : AddComponent(result[k, i], toAdd);
                    }

            return new Matrix().Set(result);
        }

        public Matrix Mul(T scalar)
        {
            T[,] result = new T[Rows, Columns];

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    result[i, j] = MulComponent(this[i, j], scalar);

            return new Matrix().Set(result);
        }

        public Matrix Div(T scalar)
        {
            T[,] result = new T[Rows, Columns];

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    result[i, j] = DivComponent(this[i, j], scalar);

            return new Matrix().Set(result);
        }

        public Matrix Transpose()
        {
            T[,] transposed = new T[Columns, Rows];

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    transposed[j, i] = this[i, j];

            return new Matrix().Set(transposed);
        }

        public T Determinant()
        {
            if (!IsSquare())
                throw new ArgumentException("Only square matrices have a determinant");

            int N = Rows; // == Columns

            if (N == 1)
                return Data[0, 0];

            if (N == 2)
                return SubComponent(MulComponent(Data[0, 0], Data[1, 1]), MulComponent(Data[0, 1], Data[1, 0]));

            T det = Zero;

            for (int i = 0; i < N; i++)
            {
                T sign = Math.Pow(-1, i) == 1.0 ? One : NegativeOne;
                Matrix subMatrix = GetSubMatrix(1, 0, N - 1, i).CombineHorizontally(GetSubMatrix(1, i + 1, N - 1, N - 1 - i));
                T subDeterminant = subMatrix.Determinant();
                det = AddComponent(det, MulComponent(MulComponent(sign, Data[0, i]), subDeterminant));
            }

            return det;
        }

        public Matrix Identity()
        {
            if (!IsSquare())
                throw new ArgumentException("Non-square matrices do not have an identity");

            int N = Rows; // == Columns

            T[,] identity = new T[N, N];

            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    identity[i, j] = i == j ? One : Zero;

            return new Matrix().Set(identity);
        }

        public Matrix Inverse()
        {
            if (!IsSquare() || Determinant().Equals(Zero))
                throw new ArgumentException("Matrix not invertible");

            int N = Rows; // == Columns

            Matrix copy = new Matrix().Set((T[,])Data.Clone());
            Matrix inverse = copy.Identity();

            for (int i = 0; i < N; i++)
            {
                int j = i;
                int maxIndex = i;
                T maxVal = copy[i, j];
                for (int k = i + 1; k < N; k++)
                    if (copy[k, j].CompareTo(maxVal) == 1)
                    {
                        maxVal = copy[k, j];
                        maxIndex = k;
                    }

                if (!maxVal.Equals(Zero))
                {
                    Matrix temp = copy.GetSubMatrix(i, 0, 1, N);
                    copy.SetSubMatrix(i, 0, copy.GetSubMatrix(maxIndex, 0, 1, N));
                    copy.SetSubMatrix(maxIndex, 0, temp);
                    temp = inverse.GetSubMatrix(i, 0, 1, N); ;
                    inverse.SetSubMatrix(i, 0, inverse.GetSubMatrix(maxIndex, 0, 1, N));
                    inverse.SetSubMatrix(maxIndex, 0, temp);
                    for (int l = 0; l < N; l++)
                    {
                        copy[i, l] = DivComponent(copy[i, l], maxVal);
                        inverse[i, l] = DivComponent(inverse[i, l], maxVal);
                    }
                }

                for (int m = 0; m < N; m++)
                {
                    if (m == i) continue;
                    T factor = copy[m, i];
                    for (int n = 0; n < N; n++)
                    {
                        copy[m, n] = SubComponent(copy[m, n], MulComponent(factor, copy[i, n]));
                        inverse[m, n] = SubComponent(inverse[m, n], MulComponent(factor, inverse[i, n]));
                    }
                }
            }

            return inverse;
        }

        public Matrix GetSubMatrix(int a, int b, int rows, int columns)
        {
            if (a + rows > Rows || b + columns > Columns)
                throw new IndexOutOfRangeException("Submatrix out of bounds.");

            T[,] subMatrix = new T[rows, columns];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    subMatrix[i, j] = this[i + a, j + b];

            return new Matrix().Set(subMatrix);
        }

        public void SetSubMatrix(int a, int b, AbstractMatrix<Matrix, T> subMatrix)
        {
            if (subMatrix.Rows + a > Rows || subMatrix.Columns + b > Columns)
                throw new IndexOutOfRangeException("Submatrix out of bounds.");

            for (int i = 0; i < subMatrix.Rows; i++)
                for (int j = 0; j < subMatrix.Columns; j++)
                    this[i + a, j + b] = subMatrix[i, j];
        }

        public bool IsSameDimensionsAs(AbstractMatrix<Matrix, T> that)
        {
            return Rows == that.Rows && Columns == that.Columns;
        }

        public bool CanMultiplyWith(AbstractMatrix<Matrix, T> that)
        {
            return Columns == that.Rows;
        }

        public bool IsSquare()
        {
            return Rows == Columns;
        }

        public override int GetHashCode()
        {
            int result = 17;
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    result += 31 * result + this[i, j].GetHashCode();

            return result;
        }

        public override bool Equals(Object obj)
        {
            if (!(obj is Matrix))
                return false;

            Matrix that = (Matrix) obj;

            if (!IsSameDimensionsAs(that))
                return false;

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    if (!this[i, j].Equals(that[i, j]))
                        return false;

            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    sb.Append(string.Format("{0," + (WidthOfColumn(j) + (j == 0 ? 0 : 2)) + "}", this[i, j]));

                if (i != Rows - 1)
                    sb.Append("\n");
            }
            return sb.ToString();

            int WidthOfColumn(int j)
            {
                int width = 0;
                for (int i = 0; i < Rows; i++)
                {
                    int w = this[i, j].ToString().Length;
                    if (w > width)
                        width = w;
                }
                return width;
            }
        }

        internal Matrix Set(T[,] data)
        {
            Data = data;
            Rows = data.GetLength(0);
            Columns = data.GetLength(1);
            return (Matrix) this;
        }

        internal Matrix CombineHorizontally(AbstractMatrix<Matrix, T> other)
        {
            if (Rows != other.Rows)
                throw new ArgumentException("Cannot combine matrix with different number of rows");

            Matrix combined = new Matrix().Set(new T[Rows, Columns + other.Columns]);

            combined.SetSubMatrix(0, 0, this);
            combined.SetSubMatrix(0, Columns, other);

            return combined;
        }

        public static Matrix operator +(AbstractMatrix<Matrix, T> a, AbstractMatrix<Matrix, T> b) => a.Add(b);

        public static Matrix operator -(AbstractMatrix<Matrix, T> a, AbstractMatrix<Matrix, T> b) => a.Sub(b);

        public static Matrix operator *(AbstractMatrix<Matrix, T> a, AbstractMatrix<Matrix, T> b) => a.Mul(b);

        public static Matrix operator *(AbstractMatrix<Matrix, T> a, T scalar) => a.Mul(scalar);
     
        public static Matrix operator /(AbstractMatrix<Matrix, T> a, T scalar) => a.Div(scalar);

        public static bool operator ==(AbstractMatrix<Matrix, T> a, AbstractMatrix<Matrix, T> b) =>
            a is null && b is null || (a?.Equals(b)).GetValueOrDefault(false);

        public static bool operator !=(AbstractMatrix<Matrix, T> a, AbstractMatrix<Matrix, T> b) => 
            !(a == b);
    }
}