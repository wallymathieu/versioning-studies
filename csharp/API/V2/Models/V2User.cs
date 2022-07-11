namespace API.V2;

public record V2User(
    Uri UserUri,
    string Email,
    bool IsActive,
    V2UserName Name,
    IEnumerable<V2UserRole> Roles);
