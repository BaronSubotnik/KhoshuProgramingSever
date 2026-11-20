namespace KhonshuProgramingAiShared.Dto;

public sealed record ChatMessagesDto
{
    public Int32 MessagesChatId { get; set; }
    public List<ViewMessageDto> Messages { get; init; } = [];
}
