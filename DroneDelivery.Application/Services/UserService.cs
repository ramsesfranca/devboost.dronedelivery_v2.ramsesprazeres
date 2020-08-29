using DroneDelivery.Application.Interfaces;
using DroneDelivery.Application.Models;
using DroneDelivery.Application.Response;
using DroneDelivery.Data.Repositorios.IRepository;
using DroneDelivery.Domain.Entidades;
using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DroneDelivery.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, ITokenService tokenService, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }


        public async Task<ResponseVal> Authentication(LoginModel loginModel)
        {
            ResponseVal _response = new ResponseVal();
            // Recupera o usuário
            var user = await _unitOfWork.Users.ObterPorEmailAsync(loginModel.Email);

            // Verifica se o usuário existe
            if (user == null)
                _response.AddNotification(new Notification("user", "Usuário ou senha inválidos"));


            var passwordResult = _passwordHasher.VerifyHashedPassword(user, user.Password, loginModel.Password);

            if (passwordResult == PasswordVerificationResult.Failed)
                _response.AddNotification(new Notification("user", "Usuário ou senha inválidos"));


            if (_response.HasFails)
                return _response;

            var jwt = _tokenService.CreateJWT(user);
            _response.AddValue(new
            {
                loginModel.Email,
                jwt
            });
            return _response;
        }

        public async Task<ResponseVal> CriarUsuario(CreateUserModel createUserModel)
        {
            ResponseVal _response = new ResponseVal();
            var user = await _unitOfWork.Users.ObterPorEmailAsync(createUserModel.Email);

            // Verifica se o usuário existe
            if (user != null)
            {
                _response.AddNotification(new Notification("user", "Email já foi utilizado"));
                return _response;
            }

            user = new User
            {
                Email = createUserModel.Email,
                Username = createUserModel.Username,
                Role = createUserModel.Role,
            };

            //hash password
            var passwordHash = _passwordHasher.HashPassword(user, createUserModel.Password);
            user.Password = passwordHash;

            await _unitOfWork.Users.AdicionarAsync(user);
            await _unitOfWork.SaveAsync();

            _response.AddValue(new
            {
                createUserModel.Email
            });
            return _response;


        }
    }
}
