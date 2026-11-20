using KhonshuProgramingAiShared.Common;
namespace KhonshuProgramingAiShared.Dto;

public sealed record ViewMessageDto
{
    public String MessageText { get; init; } = null!;
    public String? ImagePath { get; init; }
    public Role Role { get; init; }
    public DateTime CreatedAt { get; init; }
    
}
