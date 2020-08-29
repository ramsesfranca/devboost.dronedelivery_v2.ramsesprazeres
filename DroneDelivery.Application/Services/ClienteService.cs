using DroneDelivery.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Linq;

namespace DroneDelivery.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClienteService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public Guid GetCurrentId()
        {
            var userId = this._httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;

            return Guid.Parse(userId!);
        }
    }
}
