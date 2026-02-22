namespace CleaHub.Domain.Entities;

public abstract class BaseEntity<TId>{
    public required TId Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

/*
Örnek Kullanım ;
public class User : BaseEntity<Guid> { 
public string Username { get; set; } 
public string Email { get; set; } 
// Domain mantığına özgü ek alanlar 
public bool IsActive { get; set; } } }
*/