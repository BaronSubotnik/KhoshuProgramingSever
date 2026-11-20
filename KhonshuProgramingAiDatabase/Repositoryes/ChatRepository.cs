using KhonshuProgramingAiDatabase.Database;
using KhonshuProgramingAiDatabase.Interfaces;
using KhonshuProgramingAiDatabase.Tables;
using KhonshuProgramingAiShared.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace KhonshuProgramingAiDatabase.Repositoryes;

public sealed class ChatRepository : IChatsRepo
{
    private ILogger<PersonRepository> logger;
    private KhonshuDbContext db;
    
    public ChatRepository(KhonshuDbContext db,  ILogger<PersonRepository> logger)
    {
        this.db = db;
        this.logger = logger;
    }

    public async Task<Int32> CreateChatAsync(String title, Int32 userId)
    {
        var newChat = new Chat()
        {
            Title = title,
            PersonId = userId,
        };
        await db.Chats.AddAsync(newChat);
        await db.SaveChangesAsync();
        
        return  newChat.Id;
    }

    public async Task<List<ChatDto>> GetAllChatsByPersonIdAsync(Int32 personId)
    {
        return await db.Chats.AsNoTracking().Where(w => w.PersonId == personId).Select(s => new ChatDto()
        {
            Title = s.Title,
            ChatId = s.Id
        }
        ).ToListAsync();
    }

    public async Task DeleteChatAsync(Int32 chatId) 
    {
        var deletedChat = await db.Chats.Where(w => w.Id == chatId).FirstOrDefaultAsync();
        if (deletedChat is null)
            return;
        
        db.Chats.Remove(deletedChat);
        await db.SaveChangesAsync();
    }
}
