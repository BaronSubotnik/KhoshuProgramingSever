namespace KhonshuProgramingAiShared.Dto;

public sealed record LoginDto
{
    public String Username { get; init; } = null!;
    public String Password { get; init; } = null!;
}
