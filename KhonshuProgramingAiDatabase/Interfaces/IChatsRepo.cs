using KhonshuProgramingAiShared.Dto;
namespace KhonshuProgramingAiDatabase.Interfaces;

public interface IChatsRepo
{
    Task<Int32> CreateChatAsync(String title, Int32 userId);
    Task<List<ChatDto>> GetAllChatsByPersonIdAsync(Int32 personId);
    Task DeleteChatAsync(Int32 chatId);
}
