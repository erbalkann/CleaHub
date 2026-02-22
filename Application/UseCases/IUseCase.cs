using CleaHub.Shared.Results;

namespace CleaHub.Application.UseCases;

public interface IUseCase<TRequest, TResponse>{
    Task<Result<TResponse>> HandleAsync(TRequest request);
}

/*
Açıklama:
TRequest: Use Case’in girdi parametresi (örneğin DTO).

TResponse: Use Case’in çıktısı.

HandleAsync: Use Case’in ana iş mantığını çalıştırır.

Result<TResponse> kullanarak başarı/hata yönetimini standart hale getiriyoruz.


ÖRNEK;

using CleanKernel.Application.UseCases;
using CleanKernel.Shared.Results;

namespace SampleProject.Application.UseCases
{
    public class GetUserByIdUseCase : IUseCase<Guid, UserDto>
    {
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IMapper<User, UserDto> _mapper;

        public GetUserByIdUseCase(IRepository<User, Guid> userRepository, IMapper<User, UserDto> mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> HandleAsync(Guid request)
        {
            var user = await _userRepository.GetByIdAsync(request);
            if (user == null)
                return Result<UserDto>.Failure("Kullanıcı bulunamadı.");

            var dto = _mapper.Map(user);
            return Result<UserDto>.Success(dto);
        }
    }
}

Açıklama:
GetUserByIdUseCase, bir kullanıcıyı Id ile bulma iş mantığını içeriyor.

Repository ile veritabanından kullanıcıyı çekiyoruz.

Mapper ile entity → DTO dönüşümü yapıyoruz.

Sonucu Result<UserDto> ile dönüyoruz.

Böylece iş mantığı net bir şekilde ayrışmış oldu.


Avantajlar
Her iş kuralı ayrı bir sınıfta.

Test etmek kolay (Use Case’i bağımsız test edebilirsin).

Katmanlar arası bağımlılık minimum.

SOLID prensiplerine uygun.
*/