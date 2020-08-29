using DroneDelivery.Application.Mediatr.Request;
using DroneDelivery.Application.Response;
using Flunt.Notifications;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DroneDelivery.Application.Validador
{
    public class RequestsValidationMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : Request<ResponseVal>
         where TResponse : ResponseVal
    {
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            return request.Invalid
                ? Errors(request.Notifications)
                : next();
        }

        private static Task<TResponse> Errors(IEnumerable<Notification> notifications)
        {
            var response = new ResponseVal();
            response.AddNotifications(notifications);

            return Task.FromResult(response as TResponse);
        }
    }

}
