using API.V1;
using API.V2;
using AutoFixture.Xunit2;
using Domain;

namespace Tests;

public class MapperTests
{
    [Theory,AutoData]
    public void Can_map_from_user_to_api_version_1(User user)
    {
        var result = V1Mapper.Map(user);
        Assert.Equal(user.Email,result.Email);
    }
    [Theory,AutoData]
    public void Can_map_from_user_to_api_version_2(User user)
    {
        var result = V2Mapper.Map(user);
        Assert.Equal(user.Email,result.Email);
    }

}