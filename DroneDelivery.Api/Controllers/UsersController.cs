using DroneDelivery.Application.Commands.Users;
using DroneDelivery.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DroneDelivery.Api.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(System.Collections.Generic.IReadOnlyCollection<Flunt.Notifications.Notification>), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ResponseVal>> Login([FromBody] LoginUsuarioCommand command)
        {
            var response = await this._mediator.Send(command);

            return response.HasFails ? (ActionResult<ResponseVal>)BadRequest(response.Fails) : Ok(response.Data);
        }

        [HttpPost("registrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(System.Collections.Generic.IReadOnlyCollection<Flunt.Notifications.Notification>), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Post([FromBody] CriarUsuarioCommand command)
        {
            var response = await this._mediator.Send(command);

            return response.HasFails ? (ActionResult)BadRequest(response.Fails) : Ok();
        }
    }
}
