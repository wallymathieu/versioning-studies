using System.Net;
using Domain;
using FluentAssertions;
using FluentAssertions.Json;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace Tests;

public class ContractTests :IDisposable
{
    private readonly TestApplicationFactory _adapter;

    public ContractTests()
    {
        _adapter = new TestApplicationFactory(svc =>
        {
            var svcFake = new UserRepositoryFake();
            svcFake.UserList.Add(new User(new UserId(1), "user", "psw", "test@test.es", true, "Firstname",
                "Lastname", "Firstname Lastname", new[]
                {
                    UserRole.Normal
                }));
            svcFake.UserList.Add(new User(new UserId(2), "support", "psw", "test2@test.se", true, "Firstname",
                "Lastname", "Firstname Lastname", new[]
                {
                    UserRole.Support
                }));
            svc.AddSingleton<IUserRepository>(svcFake);
        });
    }
    class UserRepositoryFake : IUserRepository
    {
        public readonly List<User> UserList = new List<User>();
        public IEnumerable<User> GetUsers()
        {
            return UserList;
        }
    }
       
    [Fact]
    public async Task GetAllUsersUrlV1()
    {
        var result = await _adapter.CreateClient().GetAsync("/v1/User");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var stringResult = await result.Content.ReadAsStringAsync();
        AssertV1Data(stringResult);
    }
    [Fact]
    public async Task GetAllUsersParamV1()
    {
        var result = await _adapter.CreateClient().GetAsync("/User?api-version=1.0");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var stringResult = await result.Content.ReadAsStringAsync();
        AssertV1Data(stringResult);
    }
    [Fact]
    public async Task GetAllUsersDefaultToV2()
    {
        var result = await _adapter.CreateClient().GetAsync("/User");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var stringResult = await result.Content.ReadAsStringAsync();
        AssertV2Data(stringResult);
    }
    [Fact]
    public async Task GetAllUsersV2()
    {
        var result = await _adapter.CreateClient().GetAsync("/v2.0/User");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var stringResult = await result.Content.ReadAsStringAsync();
        AssertV2Data(stringResult);
    }
    [Fact]
    public async Task GetAllUsersV2Param()
    {
        var result = await _adapter.CreateClient().GetAsync("/User?api-version=2.0");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var stringResult = await result.Content.ReadAsStringAsync();
        AssertV2Data(stringResult);
    }

    public void Dispose()
    {
        _adapter.Dispose();
    }

    private static void AssertV1Data(string stringResult)
    {
        JToken.Parse(stringResult).Should().BeEquivalentTo(JToken.Parse(@"[{
    ""id"": 1,
    ""email"": ""test@test.es"",
    ""isActive"": true,
    ""name"": ""Firstname Lastname"",
    ""roles"": [""N""]
},{
    ""id"": 2,
    ""email"": ""test2@test.se"",
    ""isActive"": true,
    ""name"": ""Firstname Lastname"",
    ""roles"": [""S""]
}]"));
    }

    private static void AssertV2Data(string stringResult)
    {
        JToken.Parse(stringResult).Should().BeEquivalentTo(JToken.Parse(@"[{
    ""userUri"": ""/user/1"",
    ""email"": ""test@test.es"",
    ""isActive"": true,
    ""name"": {
        ""firstName"": ""Firstname"",
        ""lastName"": ""Lastname""
    },
    ""roles"": [""USR""]
},{
    ""userUri"": ""/user/2"",
    ""email"": ""test2@test.se"",
    ""isActive"": true,
    ""name"": {
        ""firstName"": ""Firstname"",
        ""lastName"": ""Lastname""
    },
    ""roles"": [""SUP""]
}]"));
    }
}