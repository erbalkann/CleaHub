using CleaHub.Application.UseCases;
using CleaHub.Shared.Results;

namespace CleaHub.Infrastructure.Decorators;

public class LoggingDecorator<TRequest, TResponse> : UseCaseDecorator<TRequest, TResponse>{
    
    public LoggingDecorator(IUseCase<TRequest,TResponse> inner):base(inner){}

    public override async Task<Result<TResponse>> HandleAsync(TRequest request) { 
        Console.WriteLine($"[LOG] UseCase started with request: {typeof(TRequest).Name}"); 
        var result = await _inner.HandleAsync(request); 
        Console.WriteLine($"[LOG] UseCase finished with success: {result.IsSuccess}"); 
        return result; 
    }
}