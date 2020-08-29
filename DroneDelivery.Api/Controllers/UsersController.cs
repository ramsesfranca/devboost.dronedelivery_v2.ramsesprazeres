using DroneDelivery.Application.Models;
using DroneDelivery.Application.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DroneDelivery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("registrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(System.Collections.Generic.IReadOnlyCollection<Flunt.Notifications.Notification>), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ResponseVal>> Post([FromBody] CreateUserModel createUserModel)
        {
            var response = await _userService.CriarUsuario(createUserModel);
            if (response.HasFails)
                return BadRequest(response.Fails);

            return Ok(response.Data);
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(System.Collections.Generic.IReadOnlyCollection<Flunt.Notifications.Notification>), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ResponseVal>> Login(LoginModel loginModel)
        {
            var response = await _userService.Authentication(loginModel);

            if (response.HasFails)
                return BadRequest(response.Fails);

            return Ok(response.Data);
        }

    }
}
