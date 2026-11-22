using KhonshuProgramingAiShared.Common;
namespace KhonshuProgramingAiShared.Dto;

public sealed record NewMessageDto
{
    public String TextMessage { get; init; } = null!;
    public String? ImagePath { get; init; } = null;
    public String? ImageType { get; init; } = null;
    public Int32 TargetChatId { get; init; }
    public DateTime CreatedAt { get; init; }
    public Role Role { get; init; }
}
