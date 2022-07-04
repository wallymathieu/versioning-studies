using API.V1;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1;

[ApiController]
[Route("v1/user")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetUsers")]
    public IEnumerable<V1User> Get()
    {
        return  new User[] { }.Select(V1Mapper.Map).ToArray();
    }
}
