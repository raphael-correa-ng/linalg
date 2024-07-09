## Linear Algebra in C#

This is a math library I created to learn C#. It showcases OOP, generics, operator overloading, LINQ, threads, and unit testing.

The top level classes in this library are AbstractMatrix, Rational, Complex, Vector, and the Arithmetical interface that defines +-*/ operations.

There are three implementations of AbstractMatrix: a Rational-, Complex-, and DoubleMatrix (one can easily implement Int-, Long-, and FloatMatrix as well).

Both Rational and Complex implement the Arithmetical interface.

Rationals are simply fractions consisting of a nominator and a denominator (long).

Complex is a number consisting of a real and an imaginary part. I used composition here and made real and imaginary of type Rational. 

A RationalVector consists of Rational objects. I could have created ComplexVector and DoubleVector, but I did not, since that concept is already displayed in the matrices.

Matrices can be multiplied in parallel, which is implemented by ParallelMatrixMultiplicator. See below for the results. Not as fast as my C++ version!

This was intended as an exercise for learning C#, and as such, it is not meant to be performant. :)


## Sample Output

```
-------VECTOR DEMO-------
A:
[-7/4, 4/5, 7/8, -2/7, 1]

B:
[-3/2, -3/4, -4, -4/7, 1/2]

A + B:
[-13/4, 1/20, -25/8, -6/7, 3/2]

A - B:
[-1/4, 31/20, 39/8, 2/7, 1/2]

A * B (dot product):
-1591/1960

A * 2/5:
[-7/10, 8/25, 7/20, -4/35, 2/5]

B * 2/5:
[-3/5, -3/10, -8/5, -8/35, 1/5]

A / 2/5:
[-35/8, 2, 35/16, -5/7, 5/2]

B /* 2/5:
[-15/4, -15/8, -10, -10/7, 5/4]

A normalized:
[-490/659, 224/659, 245/659, -80/659, 280/659]

B normalized:
[-14/41, -7/41, -112/123, -16/123, 14/123]

Length of A:
659/280

Length of B:
123/28


-------MATRIX OF COMPLEX NUMBERS DEMO-------
A:
 3/2-4/3i    7/6-8i    1+6/5i
-7/8-7/8i       2-i  1/2-7/3i
    1/7-i  1/5-9/2i   -1-2/3i

B:
-5/3-1/8i    1+1/8i  -3/8-1/5i
  -3+3/4i    -2/7-i  -1/6-5/8i
    1/2-i  1/8-9/7i  -8/7-3/4i

A determinant:
-53611/3150+20233/1575i

B determinant:
-5463251/1128960-1151459/188160i

A + B:
-1/6-35/24i    13/6-63/8i         5/8+i
 -31/8-1/8i       12/7-2i    1/3-71/24i
    9/14-2i  13/40-81/14i  -15/7-17/12i

A - B:
19/6-29/24i    1/6-65/8i   11/8+7/5i
 17/8-13/8i         16/7  2/3-41/24i
      -5/14  3/40-45/14i   1/7+1/12i

A * B:
  23/15+19087/720i      -4199/840-93/80i  -31583/5040-2213/1680i
  -383/64+845/192i  -2363/448-4883/1344i   -21011/6720+1643/960i
523/420+13411/840i     -369/70+1097/840i      -4127/1680+149/60i

A * 3/7-3i:
-47/14-71/14i     -47/2-97/14i  141/35-87/35i
      -3+9/4i      -15/7-45/7i    -95/14-5/2i
 -144/49-6/7i  -939/70-177/70i    -17/7+19/7i

B * 3/7-3i:
-61/56+277/56i     45/56-165/56i  -213/280+291/280i
 27/28+261/28i      -153/49+3/7i     -109/56+13/56i
 -39/14-27/14i  -213/56-363/392i    -537/196+87/28i

A transpose:
3/2-4/3i  -7/8-7/8i     1/7-i
  7/6-8i        2-i  1/5-9/2i
  1+6/5i   1/2-7/3i   -1-2/3i

B transpose:
-5/3-1/8i    -3+3/4i      1/2-i
   1+1/8i     -2/7-i   1/8-9/7i
-3/8-1/5i  -1/6-5/8i  -8/7-3/4i


-------PARALLEL MATRIX MULTIPLICATION DEMO-------
Multiplying two matrices of 500x750 and 750x500
Starting parallel...
Parallel: 00:00:22.8715471
Starting serial...
Serial: 00:00:44.7796338
Verify results are equal: True
```