namespace Linalg
{ 
    public sealed partial class Rational : Arithmetical<Rational>, ICloneable, IComparable<Rational>
    {
        public static readonly Rational ZERO = new Rational(0);
        public static readonly Rational ONE = new Rational(1);

        public long Nom { get; private set; } = 0;
        public long Den { get; private set; } = 1;

        public Rational() 
            : this(0)
        {
        }

        public Rational(long nom)
            : this(nom, 1)
        {
        }

        public Rational(long nom, long den)
        {
            if (den == 0)
                throw new DivideByZeroException();

            Nom = nom;
            Den = den;
            Simplify();
        }

        public double Magnitude()
        {
            return Math.Abs((double)this);
        }

        public Rational Add(Rational that)
        {
            long lcm = Utils.Lcm(Den, that.Den);
            long nom = ((Nom * lcm) / Den) + ((that.Nom * lcm) / that.Den);
            return new Rational(nom, lcm);
        }

        public Rational Sub(Rational that)
        {
            long lcm = Utils.Lcm(Den, that.Den);
            long nom = ((Nom * lcm) / Den) - ((that.Nom * lcm) / that.Den);
            return new Rational(nom, lcm);
        }

        public Rational Mul(Rational that)
        {
            return new Rational(Nom * that.Nom, Den * that.Den);
        }

        public Rational Div(Rational that)
        {
            return new Rational(Nom * that.Den, Den * that.Nom);
        }

        public Rational Sqrt()
        {
            return new Rational((long) Math.Sqrt(Nom), (long) Math.Sqrt(Den));
        }

        public Rational Inverse()
        {
            return new Rational(Den, Nom);
        }

        public Rational Neg()
        {
            return new Rational(-Nom, Den);
        }

        private void Simplify()
        {
            long gcd = Utils.Gcd(Nom, Den);
            Nom /= gcd;
            Den /= gcd;
            if (Den < 0)
            {
                Nom *= -1;
                Den *= -1;
            }
        }

        public object Clone()
        {
            return new Rational(Nom, Den);
        }

        public int CompareTo(Rational that)
        {
            if (Nom == that.Nom && Den == that.Den)
                return 0;
            long lcm = Utils.Lcm(Den, that.Den);
            return Math.Sign(Nom * (lcm / Den) - that.Nom * (lcm / that.Den));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Rational))
                return false;

            Rational that = (Rational)obj;

            return Nom == that.Nom
                && Den == that.Den;
        }

        public override int GetHashCode()
        {
            int result = 17;
            result += 31 * result + Nom.GetHashCode();
            result += 31 * result + Den.GetHashCode();
            return result;
        }

        public override string ToString()
        {
            if (Nom == 0) return "0";
            if (Den == 1) return Nom.ToString();
            return $"{Nom}/{Den}";
        }

        public static bool operator ==(Rational a, Rational b) =>
            a.Equals(b);

        public static bool operator !=(Rational a, Rational b) =>
            !a.Equals(b);

        public static Rational operator +(Rational a, Rational b) =>
            a.Add(b);

        public static Rational operator -(Rational a, Rational b) =>
            a.Sub(b);

        public static Rational operator *(Rational a, Rational b) =>
            a.Mul(b);

        public static Rational operator /(Rational a, Rational b) =>
            a.Div(b);

        public static bool operator <(Rational a, Rational b) =>
            a.CompareTo(b) < 0;

        public static bool operator >(Rational a, Rational b) =>
            a.CompareTo(b) > 0;

        public static bool operator <=(Rational a, Rational b) =>
            a.CompareTo(b) <= 0;

        public static bool operator >=(Rational a, Rational b) =>
            a.CompareTo(b) >= 0;

        public static explicit operator Complex(Rational a) =>
            new Complex(a);

        public static explicit operator double(Rational a) =>
            a.Nom / (double)a.Den;
    }
}