using KhonshuProgramingAiDatabase.Common;
namespace KhonshuProgramingAiDatabase.Tables;

public sealed class Message
{
    public Int32 Id { get; set; }
    public Int32 ChatId { get; set; }
    public Chat Chat { get; set; } = null!;

    public String Text { get; set; } = null!;
    public String? ImagePath { get; set; }
    public String? ImageContentType { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Role Role { get; set; }
}
