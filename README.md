## Linear Algebra in C#

This is a math library I created to learn C#. It showcases OOP, generics, operator overloading, LINQ, threads, and unit testing.

The top level classes in this library are AbstractMatrix, Rational, Complex, Vector, and the Arithmetical interface that defines the +-*/ operations.

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

B / 2/5:
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
7/2+5/7i   -1/3-2i   6-1/3i
   -4-2i  5/6+7/2i   1-3/2i
    -1-i  7/6-3/8i  -7/3+3i

B:
 3/2+2/3i    -2-1/8i     2-1/2i
-9/7-1/4i     7/3-2i  -2/3-8/3i
-3/2-6/5i  -5/6-7/2i   3/5-9/4i

A + B:
  5+29/21i  -7/3-17/8i       8-5/6i
-37/7-9/4i   19/6+3/2i    1/3-25/6i
-5/2-11/5i   1/3-31/8i  -26/15+3/4i

A - B:
   2+1/21i   5/3-15/8i        4+1/6i
-19/7-7/4i  -3/2+11/2i      5/3+7/6i
  1/2+1/5i     2+25/8i  -44/15+21/4i

A * B:
-1973/420-269/420i  -8999/504-26801/1008i  6421/1260-14867/1260i
 -6857/840-373/40i           191/18+35/4i    -1079/360-1747/180i
 2243/480-386/105i          391/24+55/12i        193/180+121/45i

A * 4/7+4i:
-6/7+706/49i  164/21-52/21i  100/21+500/21i
 40/7-120/7i  -284/21+16/3i      46/7+22/7i
  24/7-32/7i   13/6+187/42i   -40/3-160/21i

B * 4/7+4i:
-38/21+134/21i  -9/14-113/14i     22/7+54/7i
   13/49-37/7i   28/3+172/21i    72/7-88/21i
138/35-234/35i   284/21-16/3i  327/35+39/35i


-------MATRIX OF RATIONAL NUMBERS DEMO-------
A determinant:
955/588

B determinant:
34/45

A transpose:
   0  2  -1/7
-8/7  7   4/7
 1/4  2   1/3

B transpose:
-1/4  5/4    -6
-1/3  1/3     2
-2/5  6/5  -4/3

A inverse:
 140/191  308/955  -2373/955
-112/191   21/955    294/955
 252/191   96/955   1344/955

A * A inverse (should be the identity matrix):
1  0  0
0  1  0
0  0  1

B inverse:
 -64/17  -28/17  -6/17
-249/34  -93/34  -9/34
 405/68  225/68  15/34

B * B inverse (should be the identity matrix):
1  0  0
0  1  0
0  0  1


-------PARALLEL MATRIX MULTIPLICATION DEMO-------
Multiplying two matrices 500x750 and 750x500
Starting parallel...
Parallel: 00:00:01.8720457
Starting serial...
Serial: 00:00:07.1717443
Verify results are equal: True
```