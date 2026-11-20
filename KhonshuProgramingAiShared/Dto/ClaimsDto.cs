namespace KhonshuProgramingAiShared.Dto;

public sealed record ClaimsDto
{
    public String? Username { get; init; } = null!;
    public Int32 UserId {get; init;} 
}
