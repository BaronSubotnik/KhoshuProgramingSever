using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using KhonshuProgramingAiDatabase.Interfaces;
using KhonshuProgramingAiShared.Dto;
using KhoshuProgramingSever.Auth;
using KhoshuProgramingSever.General;
using Microsoft.IdentityModel.Tokens;
namespace KhoshuProgramingSever.Endpoints;

public static class AuthEndpoints
{
    extension(IEndpointRouteBuilder app)
    {
        public void MapAuthEndpoints()
        {
            var group = app.MapGroup("/api/auth").WithTags("Auth");

            group.MapPost("/login", LoginUser);
            group.MapPost("/register", RegisterNewUserAsync);
        }

        private static async Task<IResult> RegisterNewUserAsync(RegisterDto dto, IPersonRepo repo)
        {
            if(await repo.TryAddNewPersonAsync(dto)  is var id && id == -1 )
                return Results.BadRequest("Ошибка добавления");

            return Results.Ok(new RegisteredUserDto()
            {
                Username = dto.Username,
                Id = id,
                JwtToken = GetRegisterJwt(id,dto.Username)
            });

        }

        private static String GetRegisterJwt(Int32 id, String name)
        {
            List<Claim> claims = [new Claim(ClaimTypes.Name, name), new Claim(ClaimTypes.NameIdentifier, id.ToString())];

            var jwt = new JwtSecurityToken
            (
                issuer: AuthHandler.validUser,
                audience: AuthHandler.validAud,
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: new(AuthHandler.GetKey(), SecurityAlgorithms.HmacSha256)
            );
            
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private static async Task<IResult> LoginUser(LoginDto dto, IPersonRepo repo, EncryptHandler coder)
        {
            var searchedUser = await repo.GetLoginedUserAsync(dto);
            if (searchedUser is null)
                return Results.NotFound("Пользователя не существует");
            
            if(searchedUser.PasswordHash != await coder.EncryptAsync(dto.Password))
                return Results.BadRequest("Неверный пароль");

            return Results.Ok(new RegisteredUserDto()
            {
                Username = searchedUser.Username,
                Id = searchedUser.UserId,
                JwtToken = GetRegisterJwt(searchedUser.UserId, searchedUser.Username)
            });
        }
            
    }
}
