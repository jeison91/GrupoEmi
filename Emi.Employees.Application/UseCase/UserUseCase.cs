using AutoMapper;
using BCrypt.Net;
using Emi.Common.Exceptions;
using Emi.Common.ResponseModel;
using Emi.Employees.Application.DTO;
using Emi.Employees.Application.Port;
using Emi.Employees.Domain.Entities;
using Emi.Employees.Domain.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Emi.Employees.Application.UseCase
{
    public class UserUseCase(IUserRepository _userRepository, IRolRepository _rolRepository, IMapper _mapper, IConfiguration _configuration) : IUserPort
    {
        private const string msg = "Usuario o contraseña invalidos";
        public async Task<bool> Create(UserRequest userRequest)
        {
            var entity = _mapper.Map<UserEntity>(userRequest);
            var userEntity = await _userRepository.GetByUserName(userRequest.Username);
            if (userEntity != null)
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = "El usuario ya se encuentra registrado en el sistema." }));
            if ((await _rolRepository.GetById(userRequest.UserRole)) is null)
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = "El Rol enviado no es válido." }));

            entity.Password = Encript(userRequest.Password);
            await _userRepository.Create(entity);
            return true;
        }

        public async Task<TokenResponse> Login(TokenRequest userRequest)
        {
            var msgResponse = JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = msg });
            var userEntity = await _userRepository.GetByUserName(userRequest.UserName) ?? throw new BadRequestException(msgResponse);
            if (!DesEncript(userRequest.Password, userEntity.Password))
                throw new BadRequestException(msgResponse);

            return CreateToken(userEntity);
        }

        public TokenResponse CreateToken(UserEntity user)
        {
            int ExpireMinut = Convert.ToInt32(_configuration["Jwt:ExpiresMinutes"]);
            var claims = new[]
            {
                new Claim("Document", user.Username.ToString()),
                new Claim("IdUser", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.RoleTrace.Code),
                new Claim("Policy", user.RoleTrace.Code),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(ExpireMinut),
                signingCredentials: credentials);

            var Tokenresults = new TokenResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireIn = ExpireMinut,
                Token_type = "Bearer"
            };

            return Tokenresults;
        }

        private static string Encript(string Pass)
        {
            string PassWord = BCrypt.Net.BCrypt.EnhancedHashPassword(Pass, HashType.SHA512);
            return PassWord;
        }

        private static bool DesEncript(string PasswordLogin, string PasswordBD)
        {
            bool Validate = BCrypt.Net.BCrypt.EnhancedVerify(PasswordLogin, PasswordBD, HashType.SHA512);
            return Validate;
        }
    }
}
