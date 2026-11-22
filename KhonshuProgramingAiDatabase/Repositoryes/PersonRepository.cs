using KhonshuProgramingAiDatabase.Common;
using KhonshuProgramingAiDatabase.Database;
using KhonshuProgramingAiDatabase.Interfaces;
using KhonshuProgramingAiDatabase.Tables;
using KhonshuProgramingAiShared.Common;
using KhonshuProgramingAiShared.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace KhonshuProgramingAiDatabase.Repositoryes;

public sealed class PersonRepository : IPersonRepo
{
    private ILogger<PersonRepository> logger = null!;
    private KhonshuDbContext db = null!;

    public PersonRepository(KhonshuDbContext db, ILogger<PersonRepository> logger)
    {
        this.db = db;
        this.logger = logger;
    }

    public async Task<Int32> TryAddNewPersonAsync(RegisterDto  person)
    {
        if(await db.Persons.AsNoTracking().AnyAsync(a => a.PersonName == person.Username || a.Email == person.Email))
            return -1;
        
        var addedPerson = new Person()
        {
            PersonName = person.Username,
            Email = person.Email,
            PasswordHash = await person.Password.EncryptAsync(),
        };

        await db.Persons.AddAsync(addedPerson);
        
        await db.SaveChangesAsync();
        logger.LogInformation($"--- Новый пользователь: {person.AsLoggerString()}");
        return addedPerson.Id;
    }

    public async Task<Boolean> IsPersonExistByIdAsync(Int32 id)
    {
        if(id < 0)
            throw new ArgumentOutOfRangeException(nameof(id), "id не может быть меньше 0");
        
        if(await db.Persons.AsNoTracking().AnyAsync(p => p.Id == id))
            return  true;
        
        return false;
    }

    public async Task<Boolean> IsPersonExistByUsernameAsync(String username)
    {
        if(await db.Persons.AsNoTracking().AnyAsync(p => p.PersonName == username))
            return true;

        return false;
    }

    public async Task<Boolean> IsPersonExistByEmailAsync(String email)
    {
        if(await db.Persons.AsNoTracking().AnyAsync(p  => p.Email == email))
            return true;
        
        return false;
    }

    public async Task<LoginUserInfoDto?> GetLoginedUserAsync(LoginDto dto)
    {
        return  await db.Persons.AsNoTracking().Where(w => EF.Functions.Like(w.PersonName, dto.Username))
            .Select(s => new LoginUserInfoDto()
            {
                Username = s.PersonName,
                PasswordHash = s.PasswordHash,
                UserId = s.Id
            }).FirstOrDefaultAsync();
    }

    public async Task<PersonDto?> GetPersonByIdAsync(Int32 id)
    {
        if(id < 0)
            return null;

        return await db.Persons.AsNoTracking().Where(p => p.Id == id).Select(s => new PersonDto()
        {
            Id = s.Id,
            PersonName = s.PersonName,
            Email = s.Email,
            
        }).FirstOrDefaultAsync();
    }

    public async Task<Boolean> UltimateCheckExistPersonByNameOrEmailAsync(String name, String email)
    {
        return await db.Persons.AsNoTracking().AnyAsync(p => p.PersonName == name || p.Email == email);
    }
    
    
}
