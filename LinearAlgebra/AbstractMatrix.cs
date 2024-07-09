using Linalg;
using System.Text;

namespace LinAlg
{
    public abstract class AbstractMatrix<Matrix, T> where Matrix : AbstractMatrix<Matrix, T>, new()
    {
        public T[,] Data { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public AbstractMatrix(T[,] data)
        {
            Set(data);
        }

        public T this[int i, int j]
        {
            get { return Data[i, j]; }
            private set { Data[i, j] = value; }
        }

        public abstract T Determinant();

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

        public Matrix GetBlock(int a, int b, int rows, int columns)
        {
            if (a + rows > Rows || b + columns > Columns)
                throw new IndexOutOfRangeException("Submatrix out of bounds.");

            T[,] block = new T[rows, columns];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    block[i, j] = this[i + a, j + b];

            return new Matrix().Set(block);
        }

        public void SetBlock(int a, int b, AbstractMatrix<Matrix, T> block)
        {
            if (block.Rows + a > Rows || block.Columns + b > Columns)
                throw new IndexOutOfRangeException("Submatrix out of bounds.");

            for (int i = 0; i < block.Rows; i++)
                for (int j = 0; j < block.Columns; j++)
                    this[i + a, j + b] = block[i, j];
        }

        public Matrix Combine(AbstractMatrix<Matrix, T> other)
        {
            if (Rows != other.Rows)
                throw new ArgumentException("Cannot combine matrix with different number of rows");

            Matrix combined = new Matrix().Set(new T[Rows, Columns + other.Columns]);

            combined.SetBlock(0, 0, this);
            combined.SetBlock(0, Columns, other);

            return combined;
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

        public static bool operator ==(AbstractMatrix<Matrix, T> a, AbstractMatrix<Matrix, T> b) =>
            a is null && b is null || (a?.Equals(b)).GetValueOrDefault(false);

        public static bool operator !=(AbstractMatrix<Matrix, T> a, AbstractMatrix<Matrix, T> b) => 
            !(a == b);
    }
}