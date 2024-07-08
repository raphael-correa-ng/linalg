using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linalg.Tests
{
    [TestClass()]
    public class RationalVectorTests
    {
        [TestMethod()]
        public void TestAdd()
        {
            // Arrange
            RationalVector a = V(1, 2, 3);
            RationalVector b = V(3, 4, 5);

            // Act
            RationalVector actual = a + b;

            // Assert
            Assert.AreEqual(actual, V(4, 6, 8));
        }

        [TestMethod()]
        public void TestAddShouldFailDifferentDimensions()
        {
            // Arrange
            RationalVector a = V(1, 2, 3);
            RationalVector b = V(3, 4, 5, 6);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => a + b);
        }

        [TestMethod()]
        public void TestSub()
        {
            // Arrange
            RationalVector a = V(1, 2, 3);
            RationalVector b = V(3, 4, 5);

            // Act
            RationalVector actual = a - b;

            // Assert
            Assert.AreEqual(actual, V(-2, -2, -2));
        }

        [TestMethod()]
        public void TestSubShouldFailDifferentDimensions()
        {
            // Arrange
            RationalVector a = V(1, 2, 3);
            RationalVector b = V(3, 4, 5, 6);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => a - b);
        }

        [TestMethod()]
        public void TestMul()
        {
            // Arrange
            RationalVector a = V(1, 2, 3);
            Rational scalar = new Rational(3);

            // Act
            RationalVector actual = a * scalar;

            // Assert
            Assert.AreEqual(actual, V(3, 6, 9));
        }

        [TestMethod()]
        public void TestDiv()
        {
            // Arrange
            RationalVector a = V(3, 6, 9);
            Rational scalar = new Rational(3);

            // Act
            RationalVector actual = a / scalar;

            // Assert
            Assert.AreEqual(actual, V(1, 2, 3));
        }

        [TestMethod()]
        public void TestDotProd()
        {
            // Arrange
            RationalVector a = V(3, 6, 9);
            RationalVector b = V(1, 2, 3);

            // Act
            Rational actual = a * b;

            // Assert
            Assert.AreEqual(actual, new Rational(42));
        }

        [TestMethod()]
        public void TestDotProdShouldFailDifferentDimensions()
        {
            // Arrange
            RationalVector a = V(3, 6, 9);
            RationalVector b = V(1, 2, 3, 4);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => a * b);
        }

        [TestMethod()]
        public void TestNormalize()
        {
            // Arrange
            RationalVector a = V(3, 6, 9);

            // Act
            RationalVector actual = a.Normalize();

            // Assert
            Assert.AreEqual(actual.Length(), Rational.ONE);
        }

        [TestMethod()]
        public void TestNorm()
        {
            // Arrange
            RationalVector a = V(3, 6, 9);

            // Act
            Rational actual = a.Length();

            // Assert
            Assert.AreEqual(actual, new Rational(11));
        }

        private static RationalVector V(params long[] nominators)
        {
            return new RationalVector(nominators.Select(nom => new Rational(nom)).ToList());
        }
    }
}