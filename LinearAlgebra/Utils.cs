namespace Linalg
{
    public class Utils
    {
        private static readonly Random r = new Random();

        public static long Lcm(long a, long b)
        {
            if (Math.Abs(a) == Math.Abs(b))
                return Math.Abs(a);

            return Math.Abs(a * b) / Gcd(a, b);
        }

        public static long Gcd(long a, long b)
        {
            if (a % b == 0)
                return b;

            return Gcd(b, a % b);
        }

        public static RationalVector RandomVector(int length, int maxNom, int maxDen)
        {
            return new RationalVector(Enumerable.Range(0, length).Select(i => RandomRational(maxNom, maxDen)));
        }

        public static Rational RandomRational(int maxNom, int maxDen)
        {
            return new Rational(r.Next(-maxNom, maxNom), r.Next(1, maxDen)); 
        }

        public static Complex RandomComplex(int maxNom, int maxDen)
        {
            return new Complex(
                RandomRational(maxNom, maxDen), 
                RandomRational(maxNom, maxDen));
        }

        public static ComplexMatrix RandomComplexMatrix(int rows, int columns)
        {
            Complex[,] matrixData = new Complex[rows, columns];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    matrixData[i, j] = RandomComplex(9, 9);

            return new ComplexMatrix(matrixData);
        }

        public static RationalMatrix RandomRationalMatrix(int rows, int columns)
        {
            Rational[,] matrixData = new Rational[rows, columns];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    matrixData[i, j] = RandomRational(9, 9);

            return new RationalMatrix(matrixData);
        }

        public static DoubleMatrix RandomDoubleMatrix(int rows, int columns)
        {
            Random random = new Random();
            double[,] data = new double[rows, columns];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    data[i, j] = random.Next(-10, 10);
            return new DoubleMatrix(data);
        }
    }
}