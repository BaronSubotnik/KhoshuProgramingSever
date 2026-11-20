namespace KhonshuProgramingAiShared.Dto;

public sealed record RegisterDto
{
    public String Username { get; init; } = null!;
    public String Email { get; init; } = null!;
    public String Password { get; init; } = null!;
}
