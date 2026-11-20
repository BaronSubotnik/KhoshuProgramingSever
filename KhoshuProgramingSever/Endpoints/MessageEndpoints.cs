using KhonshuProgramingAiDatabase.Interfaces;
using KhonshuProgramingAiShared.Dto;
namespace KhoshuProgramingSever.Endpoints;

public static class MessageEndpoints
{
    extension(IEndpointRouteBuilder app)
    {
        public void MapMessagesEndpoints()
        {
            var group = app.MapGroup("/api/messages").WithTags("Messages");

            group.MapGet("/GetMessagesInChat/{chatId:int}" , GetMessagesInChatAsync).RequireAuthorization();
            group.MapPost("/", AddNewMessageAsync).RequireAuthorization();
        }
    }

    private static async Task<IResult> GetMessagesInChatAsync(Int32 chatId, IMessagesRepo repo) => Results.Ok(await repo.GetAllMessagesInChatByChatIdAsync(chatId));

    private static async Task<IResult> AddNewMessageAsync(NewMessageDto dto,  IMessagesRepo repo)
    {
        await repo.AddNewMessageAsync(dto);
        return Results.Ok();
    }
    
    
}
