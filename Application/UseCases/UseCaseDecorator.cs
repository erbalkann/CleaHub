using CleaHub.Shared.Results;

namespace CleaHub.Application.UseCases;

public abstract class UseCaseDecorator<TRequest,TResponse>
(IUseCase<TRequest, TResponse> inner) : IUseCase<TRequest,TResponse>{
    
    protected readonly IUseCase<TRequest,TResponse> _inner = inner;

    public abstract Task<Result<TResponse>> HandleAsync(TRequest request);
}

/*
Açıklama:
UseCaseDecorator, bir IUseCase’i sarmalar.

HandleAsync metodu override edilerek ek davranış eklenir.

Böylece zincirleme şekilde farklı decorator’lar eklenebilir.
*/