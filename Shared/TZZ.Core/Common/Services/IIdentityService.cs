using TZZ.Core.TheZachZone.Account.Commands;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.Core.Common.Services;

public interface IIdentityService
{
  Task<User?> GetUser(string email);
  Task<User?> GetUser(int id);
  Task<ZachZoneCommandResponse<User>> CreateUser(CreateAccountCommand command);
  Task<ZachZoneCommandResponse> DeleteUser(int userId);
  Task<bool> DoesUserExist(int userId);
  Task<bool> DoesUserExist(string email);
  Task<bool> SignIn(string email, string password);
  Task<string> GetPasswordResetToken();
  Task<string> GetPasswordResetToken(int userId);
  Task<bool> ResetPassword(int id, string token, string password);
  Task LogoutCurrentUser();

  Task<string?> GetClaim(int userId, string claimType);
  Task<bool> HasClaim(int userId, string claimType, string? claimValue = null);
  Task<bool> AddClaim(int userId, string claimType, string claimValue);
  Task<bool> RemoveClaim(int userId, string claimType, string claimValue);

  Task<string[]> GetRoles(int userId);
}