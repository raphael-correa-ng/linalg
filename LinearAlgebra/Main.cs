using Linalg;
using LinAlg;
using System.Diagnostics;

class MatrixDemo
{
    public static void Main()
    {
        VectorDemo();
        Console.ReadLine();
        ComplexMatrixDemo();
        Console.ReadLine();
        ParallelUnsafeMatrixDemo();
        Console.ReadLine();
    }

    private static void VectorDemo()
    {
        Console.WriteLine("-------VECTOR DEMO-------");
        RationalVector A = Utils.RandomVector(5, 9, 9);
        RationalVector B = Utils.RandomVector(5, 9, 9);

        Console.WriteLine("A:");
        Console.WriteLine(A);
        Console.WriteLine();
        Console.WriteLine("B:");
        Console.WriteLine(B);
        Console.WriteLine();

        Console.WriteLine("A + B:");
        Console.WriteLine(A + B);
        Console.WriteLine();

        Console.WriteLine("A - B:");
        Console.WriteLine(A - B);
        Console.WriteLine();

        Console.WriteLine("A * B (dot product):");
        Console.WriteLine(A * B);
        Console.WriteLine();

        Rational scalar = Utils.RandomRational(9, 9);
        Console.WriteLine($"A * {scalar}:");
        Console.WriteLine(A * scalar);
        Console.WriteLine();
        Console.WriteLine($"B * {scalar}:");
        Console.WriteLine(B * scalar);
        Console.WriteLine();
        Console.WriteLine($"A / {scalar}:");
        Console.WriteLine(A / scalar);
        Console.WriteLine();
        Console.WriteLine($"B /* {scalar}:");
        Console.WriteLine(B / scalar);
        Console.WriteLine();

        Console.WriteLine("A normalized:");
        Console.WriteLine(A.Normalize());
        Console.WriteLine();
        Console.WriteLine("B normalized:");
        Console.WriteLine(B.Normalize());
        Console.WriteLine();

        Console.WriteLine("Length of A:");
        Console.WriteLine(A.Length());
        Console.WriteLine();
        Console.WriteLine("Length of B:");
        Console.WriteLine(B.Length());
        Console.WriteLine();

        Console.WriteLine("A normalized:");
        Console.WriteLine(A.Normalize());
        Console.WriteLine();
        Console.WriteLine("B normalized:");
        Console.WriteLine(B.Normalize());
        Console.WriteLine();

        Console.WriteLine("Length of A normalized (should always be 1 or close enough due to rounding errors):");
        Console.WriteLine(A.Normalize().Length());
        Console.WriteLine();
        Console.WriteLine("Length of B normalized (should always be 1 or close enough due to rounding errors):");
        Console.WriteLine(B.Normalize().Length());
        Console.WriteLine();
    }

    private static void ComplexMatrixDemo()
    {
        Console.WriteLine("-------MATRIX OF COMPLEX NUMBERS DEMO-------");
        ComplexMatrix A = Utils.RandomComplexMatrix(3, 3);
        ComplexMatrix B = Utils.RandomComplexMatrix(3, 3);

        Console.WriteLine("A:");
        Console.WriteLine(A);
        Console.WriteLine();
        Console.WriteLine("B:");
        Console.WriteLine(B);
        Console.WriteLine();

        Console.WriteLine("A + B:");
        Console.WriteLine(A + B);
        Console.WriteLine();

        Console.WriteLine("A - B:");
        Console.WriteLine(A - B);
        Console.WriteLine();

        Console.WriteLine("A * B:");
        Console.WriteLine(A * B);
        Console.WriteLine();

        Complex scalar = Utils.RandomComplex(9, 9);
        Console.WriteLine($"A * {scalar}:");
        Console.WriteLine(A * scalar);
        Console.WriteLine();
        Console.WriteLine($"B * {scalar}:");
        Console.WriteLine(B * scalar);
        Console.WriteLine();

        Console.WriteLine("A transpose:");
        Console.WriteLine(A.Transpose());
        Console.WriteLine();
        Console.WriteLine("B transpose:");
        Console.WriteLine(B.Transpose());
        Console.WriteLine();
    }

    public static void ParallelUnsafeMatrixDemo()
    {
        Console.WriteLine("-------PARALLEL MATRIX MULTIPLICATION DEMO-------");
        ParallelMatrixMultiplicator multiplicator = new ParallelMatrixMultiplicator();

        DoubleMatrix A = Utils.RandomDoubleMatrix(500, 750);
        DoubleMatrix B = Utils.RandomDoubleMatrix(750, 500);

        Console.WriteLine("Multiplying two matrices {0}x{1} and {2}x{3}", A.Rows, A.Columns, B.Rows, B.Columns);

        Stopwatch stopWatch = new Stopwatch();

        Console.WriteLine("Starting parallel...");
        stopWatch.Start();
        AbstractMatrix<DoubleMatrix, double> parallelResult = multiplicator.Multiply(A, B, 250);
        stopWatch.Stop();
        Console.WriteLine($"Parallel: {stopWatch.Elapsed}");

        stopWatch.Restart();

        Console.WriteLine("Starting serial...");
        stopWatch.Start();
        DoubleMatrix serialResult = A * B;
        stopWatch.Stop();
        Console.WriteLine($"Serial: {stopWatch.Elapsed}");

        Console.WriteLine($"Verify results are equal: {parallelResult == serialResult}");
    }
}
