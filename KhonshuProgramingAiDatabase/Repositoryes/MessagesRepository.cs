using KhonshuProgramingAiDatabase.Database;
using KhonshuProgramingAiDatabase.Interfaces;
using KhonshuProgramingAiDatabase.Tables;
using KhonshuProgramingAiShared.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace KhonshuProgramingAiDatabase.Repositoryes;

public sealed class MessagesRepository : IMessagesRepo
{
    private ILogger<PersonRepository> logger = null!;
    private KhonshuDbContext db = null!;
    
    public MessagesRepository(KhonshuDbContext db,  ILogger<PersonRepository> logger)
    {
        this.db = db;
        this.logger = logger;
    }

    public async Task AddNewMessageAsync(NewMessageDto dto)
    {
        await db.Messages.AddAsync(new Message()
        {
            ChatId = dto.TargetChatId,
            Text = dto.TextMessage,
            ImagePath = dto.ImagePath,
            ImageContentType = dto.ImageType,
            Role =  dto.Role,
        });
        
        await db.SaveChangesAsync();
    }

    public async Task<ChatMessagesDto> GetAllMessagesInChatByChatIdAsync(Int32 chatId)
    {
        var messagesListFromCurrentChat = await db.Messages.AsNoTracking().Where(w => w.ChatId == chatId).OrderBy(o => o.CreatedAt).Select(s => new ViewMessageDto()
        {
            MessageText = s.Text,
            ImagePath = s.ImagePath,
            Role = s.Role,
            CreatedAt = s.CreatedAt,
        }).ToListAsync();

        return new ChatMessagesDto()
        {
            Messages = messagesListFromCurrentChat,
            MessagesChatId = chatId
        };
    }
    
}
