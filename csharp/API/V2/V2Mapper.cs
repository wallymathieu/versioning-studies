using API.Abstractions;
using Domain;

namespace API.V2;

public class V2Mapper: IMapper<User,V2User>
{
    public static V2User Map(User value)
    {
        return new V2User(
            UserUri: new Uri("/user/" + Uri.EscapeDataString(value.Id.Id.ToString())),
            Email: value.Email,
            IsActive: value.IsActive,
            Name: new V2UserName(
                FirstName: value.FirstName,
                LastName: value.LastName),
            Roles: value.Roles.Select(MapRole).ToArray());
    }

    private static V2UserRole MapRole(UserRole arg)
    {
        switch (arg)
        {
            case UserRole.Administrator: return V2UserRole.ADM;
            case UserRole.Support: return V2UserRole.SUP;
            case UserRole.Normal: return V2UserRole.USR;
            default: throw new Exception(arg.ToString());
        }
    }
}