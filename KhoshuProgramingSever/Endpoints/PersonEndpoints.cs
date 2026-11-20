using KhonshuProgramingAiDatabase.Interfaces;
using KhonshuProgramingAiDatabase.Repositoryes;
using KhonshuProgramingAiDatabase.Tables;
using KhonshuProgramingAiShared.Dto;
using Microsoft.AspNetCore.Mvc;
namespace KhoshuProgramingSever.Endpoints;

public static class PersonEndpoints
{
    extension(IEndpointRouteBuilder app)
    {
        public void MapPersonEndpoints()
        {
            var group = app.MapGroup("/api/Person").WithTags("Persons");

            // group.MapPost("/AddPerson" , CreateAndAddNewPersonAsync);
            group.MapGet("/CheckPersonById/{id:int}" , ExistUserByIdAsync);
            group.MapGet("/CheckPersonByName/{username}" , ExistUserByNameAsync);
            group.MapGet("/CheckPersonByEmail/{email}" , ExistUserByEmailAsync);
            group.MapGet("/GetById/{id:int}" , GetPersonByIdAsync);
        }
    }

    private static async Task<IResult> CreateAndAddNewPersonAsync(RegisterDto person,  IPersonRepo repo)
    {
        if (await repo.TryAddNewPersonAsync(person) == -1)
            return Results.BadRequest("Пользователь с таким именем или email уже существует");
        
        return Results.Ok();
    }

    private static async Task<IResult> ExistUserByIdAsync(Int32 id, IPersonRepo repo)
    {
        if(!await repo.IsPersonExistByIdAsync(id))
            return Results.NotFound("Такого пользователя нет");
        
        return  Results.Ok();
    }

    private static async Task<IResult> ExistUserByNameAsync(String username, IPersonRepo repo)
    {
        if (!await repo.IsPersonExistByUsernameAsync(username))
            return Results.NotFound("Такого пользователя нет");
        
        return Results.Ok();
    }

    private static async Task<IResult> ExistUserByEmailAsync(String email, IPersonRepo repo)
    {
        if (!await repo.IsPersonExistByEmailAsync(email))
            return Results.NotFound("Такого пользователя нет");
        
        return Results.Ok();
    }

    private static async Task<IResult> GetPersonByIdAsync(Int32 id, IPersonRepo repo)
    {
        var person = await repo.GetPersonByIdAsync(id);
        if(person is null)
            return Results.NotFound();
        
        return Results.Ok(person);
    }
    
    
}
