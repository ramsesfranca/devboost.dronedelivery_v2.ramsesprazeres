using MediatR;

namespace DroneDelivery.Application.Mediatr.Request
{
    public abstract class Request<TResponse> : IRequest<TResponse>
    {
    }
}
