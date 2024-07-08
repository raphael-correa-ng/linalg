using LinAlg;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Linalg.Tests
{
    [TestClass()]
    public class ParallelMatrixMultiplicatorTests
    {
        [TestMethod()]
        public void TestMultiplyNonSquare()
        {
            // Arrange
            DoubleMatrix A = Utils.RandomDoubleMatrix(50, 75);
            DoubleMatrix B = Utils.RandomDoubleMatrix(75, 50);

            // Act
            AbstractMatrix<DoubleMatrix, double> actual = new ParallelMatrixMultiplicator()
                .Multiply(A, B, 25);

            // Assert
            Assert.AreEqual(actual, A * B);
        }

        [TestMethod()]
        public void TestMultiplySquare()
        {
            // Arrange
            DoubleMatrix A = Utils.RandomDoubleMatrix(50, 50);
            DoubleMatrix B = Utils.RandomDoubleMatrix(50, 50);

            // Act
            AbstractMatrix<DoubleMatrix, double> actual = new ParallelMatrixMultiplicator()
                .Multiply(A, B, 10);

            // Assert
            Assert.AreEqual(actual, A * B);
        }

        [TestMethod()]
        public void TestMultiplyShouldFailNotEvenlyPartitioned()
        {
            // Arrange
            DoubleMatrix A = Utils.RandomDoubleMatrix(50, 50);
            DoubleMatrix B = Utils.RandomDoubleMatrix(50, 50);

            // Act & Assert
            Assert.ThrowsException<IndexOutOfRangeException>(() => 
                new ParallelMatrixMultiplicator().Multiply(A, B, 15));
        }
    }
}