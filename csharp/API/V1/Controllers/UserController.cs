using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.V1.Controllers;

[ApiController]
[Route( "[controller]" )]
[Route( "v{version:apiVersion}/[controller]" )]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepository;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }
    // GET ~/v1/user
    // GET ~/user?api-version=1.0
    [HttpGet]
    public IEnumerable<V1User> Get() => _userRepository.GetUsers().Select(V1Mapper.Map).ToArray();
}
