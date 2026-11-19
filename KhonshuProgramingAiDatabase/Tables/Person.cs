namespace KhonshuProgramingAiDatabase.Tables;

public sealed class Person
{
    public Int32 Id { get; set; }
    public String PersonName { get; set; } = null!;
    public String Email { get; set; } = null!;
    public String PasswordHash { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Chat> Chats { get; set; } = [];

}
