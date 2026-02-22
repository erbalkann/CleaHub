using CleaHub.Application.UseCases;
using CleaHub.Infrastructure.Decorators;

namespace CleaHub.Infrastructure.Builders;

public class UseCaseBuilder<TRequest, TResponse>
(IUseCase<TRequest, TResponse> useCase)
{ 
    private IUseCase<TRequest, TResponse> _useCase = useCase;

    public UseCaseBuilder<TRequest, TResponse> WithLogging() { 
        _useCase = new LoggingDecorator<TRequest, TResponse>(_useCase); 
        return this; 
    } 
    
    public UseCaseBuilder<TRequest, TResponse> WithValidation(Func<TRequest, bool> validator) { 
        _useCase = new ValidationDecorator<TRequest, TResponse>(_useCase, validator); 
        return this; 
    } 
    
    public UseCaseBuilder<TRequest, TResponse> WithAuthorization(Func<bool> isAuthorized) { 
        _useCase = new AuthorizationDecorator<TRequest, TResponse>(_useCase, isAuthorized); 
        return this; 
    } 

    public IUseCase<TRequest, TResponse> Build() { return _useCase; } 

}

/*
Açıklama:
UseCaseBuilder, bir IUseCase alır.

WithLogging, WithValidation, WithAuthorization gibi metotlarla decorator ekler.

Her metot zincirleme (return this) döner.

Build() ile son halini alır.

- ÖRNEK

var useCase = new UseCaseBuilder<Guid, UserDto>(
                    new GetUserByIdUseCase(userRepository, mapper))
                .WithValidation(id => id != Guid.Empty)
                .WithAuthorization(() => currentUser.IsAdmin)
                .WithLogging()
                .Build();

var result = await useCase.HandleAsync(Guid.NewGuid());

Açıklama:
GetUserByIdUseCase’i aldık.

Üzerine Validation, Authorization, Logging decorator’larını zincirleme ekledik.

Build() ile son halini aldık.

Artık Use Case çalıştırıldığında önce validation, sonra authorization, sonra logging devreye giriyor.

Avantajlar
Dekoratörleri zincirleme şekilde eklemek çok kolay.

Kod okunabilirliği artıyor.

Cross-cutting concerns (logging, validation, authorization, transaction) standart hale geliyor.
*/