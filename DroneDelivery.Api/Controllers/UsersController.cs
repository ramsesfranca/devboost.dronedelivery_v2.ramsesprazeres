using DroneDelivery.Application.Commands.Users;
using DroneDelivery.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DroneDelivery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(System.Collections.Generic.IReadOnlyCollection<Flunt.Notifications.Notification>), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ResponseVal>> Login([FromBody] LoginUsuarioCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.HasFails)
                return BadRequest(response.Fails);

            return Ok(response.Data);
        }

        [HttpPost("registrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(System.Collections.Generic.IReadOnlyCollection<Flunt.Notifications.Notification>), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Post([FromBody] CriarUsuarioCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.HasFails)
                return BadRequest(response.Fails);

            return Ok();
        }

    }
}
