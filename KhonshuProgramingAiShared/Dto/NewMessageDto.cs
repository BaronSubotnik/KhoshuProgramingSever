using KhonshuProgramingAiShared.Common;
namespace KhonshuProgramingAiShared.Dto;

public sealed record NewMessageDto
{
    public String TextMessage { get; set; } = null!;
    public String? ImagePath { get; set; }
    public String? ImageType { get; set; }
    public Int32 TargetChatId { get; set; }
    public Role Role { get; set; }
}
