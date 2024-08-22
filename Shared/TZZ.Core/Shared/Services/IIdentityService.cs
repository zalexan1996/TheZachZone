using Microsoft.AspNetCore.Identity;
using TZZ.Core.TheZachZone.Account.Commands;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.Core.Shared.Services;

public interface IIdentityService
{
    Task<User?> GetUser(string email);
    Task<User?> GetUser(int id);
    Task<ZachZoneCommand<User>> CreateUser(CreateAccountCommand command);
    Task<bool> DoesUserExist(int userId);
    Task<bool> DoesUserExist(string userName);
    Task<bool> SignIn(string email, string password);
    Task<string> GetPasswordResetToken();
    Task<string> GetPasswordResetToken(int userId);
    Task<bool> ResetPassword(int id, string token, string password);
    Task LogoutCurrentUser();
}