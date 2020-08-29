using Flunt.Notifications;
using MediatR;

namespace DroneDelivery.Application.Mediatr.Request
{
    public abstract class Request<TResponse> : Notifiable, IRequest<TResponse>
    {
    }
}
