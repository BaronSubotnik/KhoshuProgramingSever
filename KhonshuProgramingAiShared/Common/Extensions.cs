using KhonshuProgramingAiShared.Dto;
namespace KhonshuProgramingAiShared.Common;

public static class Extensions
{
    extension(RegisterDto dto)
    {
        public String AsLoggerString()
        {
            return $"Name: {dto.Username}, Email: {dto.Email}, Password: {dto.Password}";
        }
    }

    extension(NewMessageDto dto)
    {
        public ViewMessageDto AsViewMessageDto()
        {
            return new ViewMessageDto()
            {
                CreatedAt = dto.CreatedAt,
                ImagePath =  dto.ImagePath,
                MessageText = dto.TextMessage,
                Role = dto.Role
            };
        }
    }
}
