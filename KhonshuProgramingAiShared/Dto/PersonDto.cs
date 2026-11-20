namespace KhonshuProgramingAiShared.Dto;

public sealed record PersonDto
{
    public Int32 Id { get; set; }
    public String PersonName { get; set; } = null!;
    public String Email { get; set; } = null!;
}
