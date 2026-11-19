using KhonshuProgramingAiDatabase.Database;
using Microsoft.Extensions.Logging;
namespace KhonshuProgramingAiDatabase.Repositoryes;

public sealed class ChatRepository
{
    private ILogger<PersonRepository> logger = null!;
    private KhonshuDbContext db = null!;
    
    public ChatRepository(KhonshuDbContext db,  ILogger<PersonRepository> logger)
    {
        this.db = db;
        this.logger = logger;
    }
}
