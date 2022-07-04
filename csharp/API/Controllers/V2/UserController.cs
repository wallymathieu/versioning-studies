using API.V2;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V2;

[ApiController]
[Route("v2/user")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetUsers")]
    public IEnumerable<V2User> Get()
    {
        return new User[] { }.Select(V2Mapper.Map).ToArray();
    }
}
