using CleaHub.Application.Interfaces;

namespace CleaHub.Infrastructure.Mappers;

public class SimpleMapper<TSource,TDestination> : IMapper<TSource,TDestination>
where TDestination : new(){
    public TDestination Map(TSource source){
        var destination = new TDestination();

        foreach (var prop in typeof(TSource).GetProperties()){
            var destProp = typeof(TDestination).GetProperty(prop.Name);
            if(destProp != null && destProp.CanWrite){
                destProp.SetValue(destination,prop.GetValue(source));
            }
        }

        return destination;
    }

    public IEnumerable<TDestination> Map(IEnumerable<TSource> sourceList){
        foreach (var source in sourceList){
            yield return Map(source);
        }
    }   
}

/*
Açıklama:
Reflection kullanarak aynı isimdeki property’leri eşliyoruz.

TDestination : new() kısıtı sayesinde boş bir instance oluşturabiliyoruz.

Bu basit mapper, küçük projeler için yeterli olabilir.

Daha büyük projelerde AutoMapper gibi kütüphaneler kullanılabilir, ama burada amacımız kernel kütüphanesi olduğu için kendi generic mapper’ımızı yazıyoruz.

KULLANIM ÖRNEĞİ ;

var user = new User { Id = Guid.NewGuid(), Username = "erhan", Email = "erhan@example.com" };
var mapper = new SimpleMapper<User, UserDto>();

UserDto dto = mapper.Map(user);

// dto.Username = "erhan"
// dto.Email = "erhan@example.com"

Açıklama:
User entity’sini UserDto’ya dönüştürdük.

Property isimleri aynı olduğu için otomatik eşleşti.

Böylece manuel mapleme kodu yazmamıza gerek kalmadı.

Avantajlar
Tek satırda entity → DTO dönüşümü.

Liste dönüşümleri kolay.

Katmanlar arası bağımlılık azalır.
*/