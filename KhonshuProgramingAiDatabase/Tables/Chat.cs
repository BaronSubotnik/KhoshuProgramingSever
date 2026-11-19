namespace KhonshuProgramingAiDatabase.Tables;

public sealed class Chat
{
    public Int32 Id { get; set; }
    public Int32 PersonId { get; set; }
    public Person Person { get; set; } = null!;
    public String Title { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Message> Messages { get; set; } = [];
}
