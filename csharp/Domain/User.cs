namespace Domain;

public record User(
    UserId Id,
    string Login,
    string Password,
    string Email,
    bool IsActive,
    string FirstName,
    string LastName,
    string Name, 
    IEnumerable<UserRole> Roles);