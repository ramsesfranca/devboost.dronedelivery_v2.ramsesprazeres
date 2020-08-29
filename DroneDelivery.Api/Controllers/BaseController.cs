using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DroneDelivery.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;

        protected BaseController(IMediator mediator)
        {
            this._mediator = mediator;
        }
    }
}
