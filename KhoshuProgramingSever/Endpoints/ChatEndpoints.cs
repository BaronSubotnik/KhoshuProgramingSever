using System.Security.Claims;
using KhonshuProgramingAiDatabase.Interfaces;
using KhonshuProgramingAiShared.Dto;
using KhoshuProgramingSever.General;
namespace KhoshuProgramingSever.Endpoints;

public static class ChatEndpoints
{
    extension(IEndpointRouteBuilder app)
    {
        public void MapChatsEndpoints()
        {
            var group = app.MapGroup("/api/chats").WithTags("Chats");

            group.MapPost("/newChat" , CreateNewChat).RequireAuthorization();
            group.MapGet("/myChats", GetAllUserChats).RequireAuthorization();
            group.MapDelete("/deleteChat/{chatId:int}", DeleteChatAsync).RequireAuthorization();
        }
    }

    private static async Task<IResult> CreateNewChat(CreateChatDto createChatDto, ClaimsPrincipal jwtUser, IChatsRepo repo)
    {
        Int32 currentUserId = jwtUser.GetIdFromJwt();
        if (currentUserId == -1)
            return Results.BadRequest("Неизвестная ошибка 1488!");

        Int32 newChatId = await repo.CreateChatAsync(createChatDto.ChatTitle, currentUserId);
        return Results.Ok(new ChatDto()
        {
            ChatId = newChatId,
            Title = createChatDto.ChatTitle
        });
    }

    private static async Task<IResult> GetAllUserChats(ClaimsPrincipal jwtUser, IChatsRepo repo)
    {
        Int32 currentUserId = jwtUser.GetIdFromJwt();
        if (currentUserId == -1)
            return Results.BadRequest("Неизвестная ошибка 1489!");
        
        var allUserChats = await repo.GetAllChatsByPersonIdAsync(currentUserId);
        if (allUserChats.Count == 0)
            return Results.NotFound("У этого пользователя 0 чатов");
        
        return Results.Ok(allUserChats);
    }

    private static async Task<IResult> DeleteChatAsync(Int32 chatId, IChatsRepo repo)
    {
        await repo.DeleteChatAsync(chatId);
        return Results.Ok();
    }
}
