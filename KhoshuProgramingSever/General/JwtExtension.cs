using System.Security.Claims;
using KhonshuProgramingAiShared.Dto;
namespace KhoshuProgramingSever.General;

public static class JwtExtension
{
    extension(ClaimsPrincipal user)
    {
        public Int32 GetIdFromJwt()
        {
            if (!Int32.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out Int32 id))
                return -1;
            
            return id;
        }

        public String? GetNameFromJwt()
        {
            return user.FindFirstValue(ClaimTypes.Name);
        }

        public ClaimsDto GetClaimsDto()
        {
            return new ClaimsDto()
            {
                UserId = (Int32.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out Int32 id))? id : -1,
                Username =  user.FindFirstValue(ClaimTypes.Name),
            };
        }
    }
}
