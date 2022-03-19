using IdentityTemplate.Api.Data.DbContexts;
using IdentityTemplate.Api.Entities;
using IdentityTemplate.Api.Features.UserRegister;

namespace IdentityTemplate.Api.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _db;

    public UserService(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<long> Register(UserRegisterInputModel inputModel, string applicationUserId)
    {
        var user = new User
        {
            FirstName = inputModel.FirstName,
            LastName = inputModel.LastName,
            Email = inputModel.Email,
            ApplicationUserId = applicationUserId
        };
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
        return user.Id.GetValueOrDefault();
    }
}