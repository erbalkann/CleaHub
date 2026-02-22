namespace CleaHub.Application.Interfaces;

public interface IMapper<TSource, TDestination>
{
    TDestination Map(TSource source);
    IEnumerable<TDestination> Map(IEnumerable<TSource> sourceList);
}

/*
Açıklama:
Map: tek bir objeyi dönüştürür.

Map(IEnumerable<TSource>): listeyi dönüştürür.

Generic olduğu için her entity/DTO çifti için kullanılabilir.


*/