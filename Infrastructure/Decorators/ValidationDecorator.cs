using CleaHub.Application.UseCases;
using CleaHub.Shared.Results;

namespace CleaHub.Infrastructure.Decorators;

public class ValidationDecorator<TRequest, TResponse>
(IUseCase<TRequest, TResponse> inner, Func<TRequest, bool> validator) 
: UseCaseDecorator<TRequest, TResponse>(inner) {
     
    private readonly Func<TRequest, bool> _validator = validator;

    public override async Task<Result<TResponse>> HandleAsync(TRequest request) { 
        if (!_validator(request)) 
            return Result<TResponse>.Failure("Validation failed."); 
        return await _inner.HandleAsync(request); 
    } 
}