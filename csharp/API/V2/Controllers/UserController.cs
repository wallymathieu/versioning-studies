using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.V2.Controllers;

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
    // GET ~/v2/user
    // GET ~/user?api-version=2.0
    [HttpGet]
    public IEnumerable<V2User> Get() => _userRepository.GetUsers().Select(V2Mapper.Map).ToArray();
}
