using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.V2.Controllers;

[ApiController]
[Route( "[controller]" )]
[Route( "v{version:apiVersion}/[controller]" )]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }
    // GET ~/v2/user
    // GET ~/user?api-version=2.0
    [HttpGet]
    public IEnumerable<V2User> Get()
    {
        return new User[] { }.Select(V2Mapper.Map).ToArray();
    }
}
