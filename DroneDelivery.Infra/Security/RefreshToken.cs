using System;

namespace DroneDelivery.Infra.Security
{
    public class RefreshToken
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
