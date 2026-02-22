using CleaHub.Application.UseCases;
using CleaHub.Shared.Results;

namespace CleaHub.Infrastructure.Decorators;

public class AuthorizationDecorator<TRequest, TResponse>
(IUseCase<TRequest, TResponse> inner, Func<bool> isAuthorized) 
: UseCaseDecorator<TRequest, TResponse>(inner) { 
    private readonly Func<bool> _isAuthorized = isAuthorized;

    public override async Task<Result<TResponse>> HandleAsync(TRequest request) { 
        if (!_isAuthorized()) 
            return Result<TResponse>.Failure("Unauthorized."); 
        return await _inner.HandleAsync(request); 
    } 
}