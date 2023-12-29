namespace CoreBussiness.ResultPattern;

public class Maybe<T>
{
    public T Value { get; }

    private Maybe(T value)
    {
        Value = value;
    }

    public static Maybe<T> Some(T value) => new Maybe<T>(value);
    public static Maybe<T> None() => new Maybe<T>(default);
}