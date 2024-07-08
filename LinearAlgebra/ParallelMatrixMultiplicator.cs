using LinAlg;
using System.Diagnostics;

namespace Linalg
{
    public class ParallelMatrixMultiplicator
    {
        public DoubleMatrix Multiply(DoubleMatrix a, DoubleMatrix b, int blockSize)
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

            DoubleMatrix result = new DoubleMatrix(new double[ar, bc]);

            int processCount = (ar * bc) / (blockSize * blockSize);
            int blocksPerProcess = ac / blockSize; 
            int multiplicationsPerBlock = bc / blockSize;

            Thread[] threads = new Thread[processCount];

            for (int i = 0; i < processCount; i++)
            {
                DoubleMatrix[] blocksA = new DoubleMatrix[blocksPerProcess];
                DoubleMatrix[] blocksB = new DoubleMatrix[blocksPerProcess];

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

        private static void Multiply(
            DoubleMatrix result,
            DoubleMatrix[] blocksA,
            DoubleMatrix[] blocksB, 
            int row,
            int col,
            int size)
        {
            Debug.Assert(blocksA.Length == blocksB.Length);

            DoubleMatrix sumOfProducts = null;

            for (int k = 0; k < blocksA.Length; k++)
            {
                DoubleMatrix toAdd = blocksA[k].Mul(blocksB[k]);
                sumOfProducts = sumOfProducts == null ? toAdd : sumOfProducts + toAdd;
            }

            result.SetBlock(row, col, sumOfProducts);
        }
    }
}
