namespace KhonshuProgramingAiShared.Dto;

public sealed record RegisteredUserDto
{
    public String Username { get; init; } = null!;
    public Int32 Id { get; init; }
    public String JwtToken { get; init; } = null!;
}
