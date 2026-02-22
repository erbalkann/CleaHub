namespace CleaHub.Application.Dtos;

public abstract class BaseDto<TId>{
    public required TId Id { get; set; }
}

/*
Örnek kullanım ;
public class UserDto : BaseDto<Guid> { 
public string Username { get; set; } 
public string Email { get; set; } }
*/