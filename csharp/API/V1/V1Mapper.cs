using API.Abstractions;
using Domain;

namespace API.V1;

public class V1Mapper : IMapper<User,V1User>
{
    public static V1User Map(User value)
    {
        return new V1User(
            Id: value.Id.Id, 
            Email: value.Email, 
            IsActive: value.IsActive, 
            Name: value.Name, 
            Roles: value.Roles.Select(MapRole).ToArray());
    }

    private static string MapRole(UserRole arg)
    {
        switch (arg)
        {
            case UserRole.Administrator: return "A";
            case UserRole.Normal: return "N";
            case UserRole.Support: return "S";
            default: throw new Exception(arg.ToString());
        }
    }
}