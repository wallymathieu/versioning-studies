using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.V1.Controllers;

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
    // GET ~/v1/user
    // GET ~/user?api-version=1.0
    [HttpGet]
    public IEnumerable<V1User> Get()
    {
        return  new User[] { }.Select(V1Mapper.Map).ToArray();
    }
}
