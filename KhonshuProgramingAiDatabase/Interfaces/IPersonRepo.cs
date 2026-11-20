using KhonshuProgramingAiShared.Dto;
namespace KhonshuProgramingAiDatabase.Interfaces;

public interface IPersonRepo
{
    Task<Int32> TryAddNewPersonAsync(RegisterDto person);
    Task<Boolean> IsPersonExistByIdAsync(Int32 id);
    Task<Boolean> IsPersonExistByUsernameAsync(String username);
    Task<Boolean> IsPersonExistByEmailAsync(String email);
    Task<LoginUserInfoDto?> GetLoginedUserAsync(LoginDto dto);
    Task<PersonDto?> GetPersonByIdAsync(Int32 id);
}
