namespace CleaHub.Shared.Results;

public class Result
{
    public bool IsSuccess { get; }
    public string Error { get; }

    protected Result(bool isSuccess, string error)
    {
        IsSuccess = isSuccess; Error = error;
    }

    public static Result Success() => new(true, string.Empty);
    public static Result Failure(string error) => new(false, error);
}

public class Result<T> : Result
{
    public T Value { get; }
    private Result(T value, bool isSuccess, string error) : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(value, true, string.Empty);

    public static new Result<T> Failure(string error) => new(default!, false, error);
}

/*
Kullanım örneği ;
public Result<UserDto> GetUserById(Guid id) { 
    if (id == Guid.Empty) 
        return Result<UserDto>.Failure("Id geçersiz."); 
    var user = new UserDto { Id = id, Username = "erhan", Email = "erhan@example.com" }; 
    return Result<UserDto>.Success(user); 
    } 
}
*/