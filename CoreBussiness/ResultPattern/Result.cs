namespace CoreBussiness.ResultPattern;

public class Result<T>
{
    public bool Success { get; }
    public T Data { get; }
    public int StatusCode { get; }
    public string Error { get; }

    private Result(bool success, T data, int statusCode, string error)
    {
        Success = success;
        Data = data;
        StatusCode = statusCode;
        Error = error;
    }

    public static Result<T> IsSuccess(T data) => new Result<T>(true, data, 200, null);

    public static Result<T> Fail(string error, int statusCode = 500) =>
        new Result<T>(false, default(T), statusCode, error);

    public Result<T> Ensure(Func<T, bool> predicate, string errorMessage, int statusCode = 400)
    {
        if (Success && !predicate(Data))
        {
            return Fail(errorMessage, statusCode);
        }

        return this;
    }

    public Result<TOut> Map<TOut>(Func<T, TOut> func)
    {
        if (Success)
        {
            return Result<TOut>.IsSuccess(func(Data));
        }

        return Result<TOut>.Fail(Error, StatusCode);
    }

    public async Task<Result<TOut>> Bind<TOut>(Func<T, Task<Result<TOut>>> asyncFunc)
    {
        if (Success)
        {
            return await asyncFunc(Data);
        }

        return Result<TOut>.Fail(Error);
    }

    public TOut Match<TOut>(Func<T, TOut> onSuccess, Func<string, TOut> onError)
    {
        if (Success)
        {
            return onSuccess(Data);
        }

        return onError(Error);
    }

    public Result<Maybe<T>> Maybe()
    {
        return Result<Maybe<T>>.IsSuccess(Maybe<T>.Some(Data));
    }
}