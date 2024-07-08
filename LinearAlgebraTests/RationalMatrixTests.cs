using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linalg.Tests
{
    [TestClass()]
    public class RationalMatrixTests
    {
        [TestMethod()]
        public void TestAdd()
        {
            // Arrange
            RationalMatrix a = new RationalMatrix(
                new Rational[,] {
                    { R(1), R(2), R(3) },
                    { R(2), R(3), R(4) },
                    { R(3), R(4), R(5) }
                });

            RationalMatrix b = new RationalMatrix(
                new Rational[,] {
                    { R(4), R(5), R(6) },
                    { R(5), R(6), R(7) },
                    { R(6), R(7), R(8) }
                });

            // Act
            RationalMatrix actual = a + b;

            // Assert
            RationalMatrix expected = new RationalMatrix(
                new Rational[,] {
                    { R(5), R(7), R(9) },
                    { R(7), R(9), R(11) },
                    { R(9), R(11), R(13) }
                });

            Assert.AreEqual(actual, expected);
            Assert.AreEqual(actual - b, a);
        }

        [TestMethod()]
        public void TestAddShouldFailDifferentDimensions()
        {
            // Arrange
            RationalMatrix a = new RationalMatrix(
                new Rational[,] {
                    { R(1), R(2), R(3) },
                    { R(2), R(3), R(4) },
                    { R(3), R(4), R(5) }
                });

            RationalMatrix b = new RationalMatrix(
                new Rational[,] {
                    { R(4), R(5), R(6) },
                    { R(5), R(6), R(7) }
                });

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => a + b);
        }

        [TestMethod()]
        public void TestSub()
        {
            // Arrange
            RationalMatrix a = new RationalMatrix(
                new Rational[,] {
                    { R(1), R(2), R(3) },
                    { R(2), R(3), R(4) },
                    { R(3), R(4), R(5) }
                });

            RationalMatrix b = new RationalMatrix(
                new Rational[,] {
                    { R(4), R(5), R(6) },
                    { R(5), R(6), R(7) },
                    { R(6), R(7), R(8) }
                });

            // Act
            RationalMatrix actual = a - b;

            // Assert
            RationalMatrix expected = new RationalMatrix(
                new Rational[,] {
                    { R(-3), R(-3), R(-3) },
                    { R(-3), R(-3), R(-3) },
                    { R(-3), R(-3), R(-3) }
                });

            Assert.AreEqual(actual, expected);
            Assert.AreEqual(actual + b, a);
        }

        [TestMethod()]
        public void TestSubShouldFailDifferentDimensions()
        {
            // Arrange
            RationalMatrix a = new RationalMatrix(
                new Rational[,] {
                    { R(1), R(2), R(3) },
                    { R(2), R(3), R(4) },
                    { R(3), R(4), R(5) }
                });

            RationalMatrix b = new RationalMatrix(
                new Rational[,] {
                    { R(4), R(5), R(6) },
                    { R(5), R(6), R(7) }
                });

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => a - b);
        }

        [TestMethod()]
        public void TestMulSquare()
        {
            // Arrange
            RationalMatrix a = new RationalMatrix(
                new Rational[,] {
                    { R(1), R(2), R(3) },
                    { R(2), R(3), R(4) },
                    { R(3), R(4), R(5) }
                });

            RationalMatrix b = new RationalMatrix(
                new Rational[,] {
                    { R(4), R(5), R(6) },
                    { R(5), R(6), R(7) },
                    { R(6), R(7), R(8) }
                });

            // Act
            RationalMatrix actual = a * b;

            // Assert
            RationalMatrix expected = new RationalMatrix(
                new Rational[,] {
                    { R(32), R(38), R(44) },
                    { R(47), R(56), R(65) },
                    { R(62), R(74), R(86) }
                });

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void TestMulNonSquare()
        {
            // Arrange
            RationalMatrix a = new RationalMatrix(
                new Rational[,] {
                    { R(1), R(2), R(3) },
                    { R(2), R(3), R(4) },
                    { R(3), R(4), R(5) },
                    { R(4), R(5), R(6) }
                });

            RationalMatrix b = new RationalMatrix(
                new Rational[,] {
                    { R(5), R(6), R(7), R(6) },
                    { R(6), R(7), R(8), R(9) },
                    { R(7), R(8), R(9), R(10) }
                });

            // Act
            RationalMatrix actual = a * b;

            // Assert
            RationalMatrix expected = new RationalMatrix(
                new Rational[,] {
                    { R(38), R(44), R(50), R(54) },
                    { R(56), R(65), R(74), R(79) },
                    { R(74), R(86), R(98), R(104) },
                    { R(92), R(107), R(122), R(129) }
                });

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void TestMulShouldFailIncompatibleDimensions()
        {
            // Arrange
            RationalMatrix a = new RationalMatrix(
                new Rational[,] {
                    { R(1), R(2), R(3) },
                    { R(2), R(3), R(4) },
                    { R(3), R(4), R(5) }
                });

            RationalMatrix b = new RationalMatrix(
                new Rational[,] {
                    { R(5), R(6), R(7), R(6) },
                    { R(6), R(7), R(8), R(9) }
                });

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => a * b);
        }

        [TestMethod()]
        public void TestTranspose()
        {
            // Arrange
            RationalMatrix a = new RationalMatrix(
                new Rational[,] {
                    { R(1), R(2), R(3) },
                    { R(2), R(3), R(4) },
                    { R(3), R(4), R(5) },
                    { R(4), R(5), R(6) }
                });

            // Act
            RationalMatrix actual = a.Transpose();

            // Assert
            RationalMatrix expected = new RationalMatrix(
                new Rational[,] {
                    { R(1), R(2), R(3), R(4) },
                    { R(2), R(3), R(4), R(5) },
                    { R(3), R(4), R(5), R(6) }
                });

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void TestGetBlock()
        {
            // Arrange
            RationalMatrix a = new RationalMatrix(
                new Rational[,] {
                    { R(1), R(2), R(3) },
                    { R(2), R(3), R(4) },
                    { R(3), R(4), R(5) },
                    { R(4), R(5), R(6) }
                });

            // Act
            RationalMatrix actual = a.GetBlock(2, 1, 2, 2);

            // Assert
            RationalMatrix expected = new RationalMatrix(
                new Rational[,] {
                    { R(4), R(5) },
                    { R(5), R(6) }
                });

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void TestGetBlockShouldFailIndexOutOfBounds()
        {
            // Arrange
            RationalMatrix a = new RationalMatrix(
                new Rational[,] {
                    { R(1), R(2), R(3) },
                    { R(2), R(3), R(4) },
                    { R(3), R(4), R(5) },
                    { R(4), R(5), R(6) }
                });

            // Act & Assert
            Assert.ThrowsException<IndexOutOfRangeException>(() => a.GetBlock(2, 1, 3, 3));
        }

        [TestMethod()]
        public void TestSetBlock()
        {
            // Arrange
            RationalMatrix a = new RationalMatrix(
                new Rational[,] {
                    { R(1), R(2), R(3) },
                    { R(2), R(3), R(4) },
                    { R(3), R(4), R(5) },
                    { R(4), R(5), R(6) }
                });

            RationalMatrix block = new RationalMatrix(
                new Rational[,] {
                    { R(0), R(0) },
                    { R(0), R(0) }
                });

            // Act
            a.SetBlock(2, 1, block);

            // Assert
            RationalMatrix expected = new RationalMatrix(
                new Rational[,] {
                    { R(1), R(2), R(3) },
                    { R(2), R(3), R(4) },
                    { R(3), R(0), R(0) },
                    { R(4), R(0), R(0) }
                });

            Assert.AreEqual(a, expected);
        }

        [TestMethod()]
        public void TestSetBlockShouldFailIndexOutOfBounds()
        {
            // Arrange
            RationalMatrix a = new RationalMatrix(
                new Rational[,] {
                    { R(1), R(2), R(3) },
                    { R(2), R(3), R(4) },
                    { R(3), R(4), R(5) },
                    { R(4), R(5), R(6) }
                });

            RationalMatrix block = new RationalMatrix(
                new Rational[,] {
                    { R(0), R(0), R(0) },
                    { R(0), R(0), R(0) }
                });

            // Act & Assert
            Assert.ThrowsException<IndexOutOfRangeException>(() => a.SetBlock(2, 1, block));
        }

        private static Rational R(long nom)
        {
            return new Rational(nom);
        }
    }
}