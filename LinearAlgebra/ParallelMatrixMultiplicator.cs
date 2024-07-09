using LinAlg;
using System.Diagnostics;

namespace Linalg
{
    public class ParallelMatrixMultiplicator
    {
        public AbstractMatrix<Matrix, T> Multiply<Matrix, T>(AbstractMatrix<Matrix, T> a, AbstractMatrix<Matrix, T> b, int subMatrixSize)
             where Matrix : AbstractMatrix<Matrix, T>, new()
        {
            if (!a.CanMultiplyWith(b))
                throw new ArgumentException();

            int ar = a.Rows;
            int ac = a.Columns;
            int br = b.Rows;
            int bc = b.Columns;

            if (ar % subMatrixSize != 0 || ac % subMatrixSize != 0 || 
                br % subMatrixSize != 0 || bc % subMatrixSize != 0)
                throw new IndexOutOfRangeException();

            Matrix result = new Matrix().Set(new T[ar, bc]);

            int processCount = (ar * bc) / (subMatrixSize * subMatrixSize);
            int subMatrixsPerProcess = ac / subMatrixSize; 
            int multiplicationsPerBlock = bc / subMatrixSize;

            Thread[] threads = new Thread[processCount];

            for (int i = 0; i < processCount; i++)
            {
                AbstractMatrix<Matrix, T>[] subMatrixsA = new AbstractMatrix<Matrix, T>[subMatrixsPerProcess];
                AbstractMatrix<Matrix, T>[] subMatrixsB = new AbstractMatrix<Matrix, T>[subMatrixsPerProcess];

                int row = (i / multiplicationsPerBlock) * subMatrixSize;
                int col = (i % multiplicationsPerBlock) * subMatrixSize;

                for (int j = 0; j < subMatrixsPerProcess; j++)
                {
                    subMatrixsA[j] = a.GetSubMatrix(row, j * subMatrixSize, subMatrixSize, subMatrixSize);
                    subMatrixsB[j] = b.GetSubMatrix(j * subMatrixSize, col, subMatrixSize, subMatrixSize);
                }

                threads[i] = new Thread(() => Multiply(result, subMatrixsA, subMatrixsB, row, col, subMatrixSize));
                threads[i].Start();
            }

            foreach (Thread t in threads)
               t.Join();

            return result;
        }

        private static void Multiply<Matrix, T>(
            AbstractMatrix<Matrix, T> result,
            AbstractMatrix<Matrix, T>[] subMatrixsA,
            AbstractMatrix<Matrix, T>[] subMatrixsB, 
            int row,
            int col,
            int size) where Matrix : AbstractMatrix<Matrix, T>, new()
        {
            Debug.Assert(subMatrixsA.Length == subMatrixsB.Length);

            AbstractMatrix<Matrix, T> sumOfProducts = null;

            for (int k = 0; k < subMatrixsA.Length; k++)
            {
                AbstractMatrix<Matrix, T> toAdd = subMatrixsA[k].Mul(subMatrixsB[k]);
                sumOfProducts = sumOfProducts == null ? toAdd : sumOfProducts.Add(toAdd);
            }

            result.SetSubMatrix(row, col, sumOfProducts);
        }
    }
}
