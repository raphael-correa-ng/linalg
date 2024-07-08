using LinAlg;
using System.Diagnostics;

namespace Linalg
{
    public class ParallelMatrixMultiplicator
    {
        public AbstractMatrix<Matrix, T> Multiply<Matrix, T>(AbstractMatrix<Matrix, T> a, AbstractMatrix<Matrix, T> b, int blockSize)
             where Matrix : AbstractMatrix<Matrix, T>, new()
        {
            if (!a.CanMultiplyWith(b))
                throw new ArgumentException();

            int ar = a.Rows;
            int ac = a.Columns;
            int br = b.Rows;
            int bc = b.Columns;

            if (ar % blockSize != 0 || ac % blockSize != 0 || 
                br % blockSize != 0 || bc % blockSize != 0)
                throw new IndexOutOfRangeException();

            Matrix result = new Matrix().Set(new T[ar, bc]);

            int processCount = (ar * bc) / (blockSize * blockSize);
            int blocksPerProcess = ac / blockSize; 
            int multiplicationsPerBlock = bc / blockSize;

            Thread[] threads = new Thread[processCount];

            for (int i = 0; i < processCount; i++)
            {
                AbstractMatrix<Matrix, T>[] blocksA = new AbstractMatrix<Matrix, T>[blocksPerProcess];
                AbstractMatrix<Matrix, T>[] blocksB = new AbstractMatrix<Matrix, T>[blocksPerProcess];

                int row = (i / multiplicationsPerBlock) * blockSize;
                int col = (i % multiplicationsPerBlock) * blockSize;

                for (int j = 0; j < blocksPerProcess; j++)
                {
                    blocksA[j] = a.GetBlock(row, j * blockSize, blockSize, blockSize);
                    blocksB[j] = b.GetBlock(j * blockSize, col, blockSize, blockSize);
                }

                threads[i] = new Thread(() => Multiply(result, blocksA, blocksB, row, col, blockSize));
                threads[i].Start();
            }

            foreach (Thread t in threads)
               t.Join();

            return result;
        }

        private static void Multiply<Matrix, T>(
            AbstractMatrix<Matrix, T> result,
            AbstractMatrix<Matrix, T>[] blocksA,
            AbstractMatrix<Matrix, T>[] blocksB, 
            int row,
            int col,
            int size) where Matrix : AbstractMatrix<Matrix, T>, new()
        {
            Debug.Assert(blocksA.Length == blocksB.Length);

            AbstractMatrix<Matrix, T> sumOfProducts = null;

            for (int k = 0; k < blocksA.Length; k++)
            {
                AbstractMatrix<Matrix, T> toAdd = blocksA[k].Mul(blocksB[k]);
                sumOfProducts = sumOfProducts == null ? toAdd : sumOfProducts.Add(toAdd);
            }

            result.SetBlock(row, col, sumOfProducts);
        }
    }
}
