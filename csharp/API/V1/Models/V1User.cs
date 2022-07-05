namespace API.V1;

public record V1User(
        int Id,
        string Email,
        bool IsActive,
        string Name, 
        IEnumerable<string> Roles);

