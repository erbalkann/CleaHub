using System.Linq.Expressions;
using CleaHub.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleaHub.Infrastructure.Persistence;

public class EfRepository<T,TId> : IRepository<T,TId> 
where T: class{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public EfRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(TId id)
    {
        return await _dbSet.FindAsync(id); 
    }
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate) 
    { 
        return await _dbSet.Where(predicate).ToListAsync(); 
    }
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}

/*
AÇIKLAMA:

DbContext üzerinden DbSet<T> alıyoruz.

EF Core’un FindAsync, ToListAsync, Where gibi metotlarını kullanıyoruz.

SaveChangesAsync ile değişiklikleri kaydediyoruz.

Böylece her entity için ayrı repository yazmaya gerek kalmıyor.


public class UserService
{
    private readonly IRepository<User, Guid> _userRepository;

    public UserService(IRepository<User, Guid> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDto>> GetUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return Result<UserDto>.Failure("Kullanıcı bulunamadı.");

        var dto = new UserDto { Id = user.Id, Username = user.Username, Email = user.Email };
        return Result<UserDto>.Success(dto);
    }
}

Açıklama:
UserService, IRepository<User, Guid> kullanıyor.

Repository sayesinde EF Core bağımlılığı yok, sadece interface üzerinden erişim var.

Clean Architecture’ın Dependency Inversion prensibi uygulanmış oldu.
*/