

using DroneDelivery.Application.Interfaces;
using DroneDelivery.Domain.Entidades;
using DroneDelivery.Infra.Security;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;

namespace DroneDelivery.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _settings;

        public TokenService(JwtSettings settings)
        {
            _settings = settings;
        }

        private static ClaimsIdentity GetClaimsIdentity(Cliente usuario)
        {
            var identity = new ClaimsIdentity
            (
                new GenericIdentity(usuario.Email),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, usuario.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, usuario.Nome)
                }
            );

            foreach (var role in usuario.Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            foreach (var policy in usuario.Permissions)
            {
                identity.AddClaim(new Claim("permissions", policy));
            }

            return identity;
        }

        public JsonWebToken CreateJWT(Cliente usuario)
        {
            var identity = GetClaimsIdentity(usuario);
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identity,
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                IssuedAt = _settings.IssuedAt,
                NotBefore = _settings.NotBefore,
                Expires = _settings.AccessTokenExpiration,
                SigningCredentials = _settings.SigningCredentials
            });

            var accessToken = handler.WriteToken(securityToken);

            return new JsonWebToken
            {
                AccessToken = accessToken,
                RefreshToken = CreateRefreshToken(usuario.Email),
                ExpiresIn = (long)TimeSpan.FromMinutes(_settings.ValidForMinutes).TotalSeconds
            };
        }

        private RefreshToken CreateRefreshToken(string email)
        {
            var refreshToken = new RefreshToken
            {
                Username = email,
                ExpirationDate = _settings.RefreshTokenExpiration
            };

            string token;
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                token = Convert.ToBase64String(randomNumber);
            }

            refreshToken.Token = token.Replace("+", string.Empty)
                .Replace("=", string.Empty)
                .Replace("/", string.Empty);

            return refreshToken;
        }
    }
}
