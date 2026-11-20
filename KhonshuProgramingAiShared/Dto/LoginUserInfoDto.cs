namespace KhonshuProgramingAiShared.Dto;

public  sealed record LoginUserInfoDto
{
    public String Username { get; init; } = null!;
    public String PasswordHash { get; init; } = null!;
    public Int32 UserId { get; init; }
}
