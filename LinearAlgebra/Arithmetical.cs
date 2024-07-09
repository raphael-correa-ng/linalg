namespace Linalg
{
    public interface Arithmetical<T>
    {
        T Add(T t);
        T Sub(T t);
        T Mul(T t);
        T Div(T t);
    }
}