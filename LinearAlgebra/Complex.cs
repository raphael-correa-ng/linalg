namespace Linalg
{
    public sealed class Complex : Arithmetical<Complex>, ICloneable
    {
        public static readonly Complex ZERO = new Complex(new Rational(0));

        public Rational Real { get; private set; } = new Rational(0);
        public Rational Imag { get; private set; } = new Rational(0);

        public Complex()
            : this(new Rational(0))
        {
        }

        public Complex(Rational real)
            : this(real, new Rational(0))
        {
        }

        public Complex(Rational real, Rational imag)
        {
            Real = real;
            Imag = imag;
        }

        public double Magnitude()
        {
            return Math.Sqrt(Math.Pow(Real.Magnitude(), 2) + Math.Pow(Imag.Magnitude(), 2));
        }

        public Complex Add(Complex that)
        {
            return new Complex(
                Real + that.Real, 
                Imag + that.Imag);
        }

        public Complex Sub(Complex that)
        {
            return new Complex(
                Real - that.Real, 
                Imag - that.Imag);
        }

        public Complex Mul(Complex that)
        {
            return new Complex(
                (Real * that.Real) - (Imag * that.Imag),
                (Real * that.Imag) + (Imag * that.Real));
        }

        public Complex Div(Complex that)
        {
            Complex conj = that.ComplexConjugate();
            Complex nom = this * conj;
            Rational den = (that.Real * that.Real) + (that.Imag * that.Imag);
            return new Complex(nom.Real / den, nom.Imag / den);
        }

        public Complex ComplexConjugate()
        {
            return new Complex(Real, Imag.Mul(new Rational(-1)));
        }

        public object Clone()
        {
            return new Complex((Rational)Real.Clone(), (Rational)Imag.Clone());
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Complex))
                return false;

            Complex that = (Complex)obj;

            return Real == that.Real
                && Imag == that.Imag;
        }

        public override int GetHashCode()
        {
            int result = 17;
            result += 31 * result + Real.GetHashCode();
            result += 31 * result + Imag.GetHashCode();
            return result;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}",
                Real != Rational.ZERO ? Real.ToString() : (Imag != Rational.ZERO ? "" : "0"),
                Imag != Rational.ZERO
                    ? (Imag > Rational.ZERO && Real != Rational.ZERO ? "+" : "")
                        + (Imag == Rational.ONE ? "" : Imag == Rational.ONE.Neg() ? "-" : Imag.ToString())
                        + "i"
                    : "");
        }

        public static bool operator ==(Complex a, Complex b) =>
            a.Equals(b);

        public static bool operator !=(Complex a, Complex b) =>
            !a.Equals(b);

        public static Complex operator +(Complex a, Complex b) =>
            a.Add(b);

        public static Complex operator -(Complex a, Complex b) =>
            a.Sub(b);

        public static Complex operator *(Complex a, Complex b) =>
            a.Mul(b);

        public static Complex operator /(Complex a, Complex b) =>
            a.Div(b);
    }
}