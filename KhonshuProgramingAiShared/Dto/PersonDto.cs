namespace KhonshuProgramingAiShared.Dto;

public sealed record PersonDto
{
    public Int32 Id { get; init; }
    public String PersonName { get; init; } = null!;
    public String Email { get; init; } = null!;
}
