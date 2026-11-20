using KhonshuProgramingAiShared.Dto;
namespace KhonshuProgramingAiDatabase.Interfaces;

public interface IMessagesRepo
{
    Task AddNewMessageAsync(NewMessageDto dto);
    Task<ChatMessagesDto> GetAllMessagesInChatByChatIdAsync(Int32 chatId);
}
