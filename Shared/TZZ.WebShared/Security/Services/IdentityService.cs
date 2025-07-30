using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TZZ.Common;
using TZZ.Common.Enums.Constants;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Core.Common.Services;
using TZZ.Core.TheZachZone.Account.Commands;
using TZZ.Domain.Entities.TheGameZone;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.WebShared.Security.Services;

public sealed class IdentityService(IDatabaseService dbContext, ICurrentUserService currentUserService, UserManager<User> userManager, SignInManager<User> signInManager) : IIdentityService
{
  public async Task<ZachZoneCommandResponse<User>> CreateUser(CreateAccountCommand command)
  {
    var result = await userManager.CreateAsync(new User()
    {
      Email = command.Email,
      UserName = command.Email
    }, command.Password);

    return result.Succeeded ?
      ZachZoneCommandResponse.Success((await GetUser(command.Email))!)
      : ZachZoneCommandResponse.Failure<User>(result.Errors.ToDictionary(k => k.Code, v => v.Description));
  }

  public async Task<ZachZoneCommandResponse> DeleteUser(int userId)
  {
    var user = await GetUser(userId);
    Guard.Against.Null(user, null, "User not found.");

    using var txn = await dbContext.BeginTransaction(default);

    try
    {
      // Delete user's games first.
      var games = dbContext.Entity<Game>()
        .Where(x => x.AuthorId == userId).ToList();
      dbContext.RemoveRange(games);
      await dbContext.SaveChanges(default);

      await txn.CommitAsync();

      // Delete user account.
      var result = await userManager.DeleteAsync(user);

      return result.Succeeded
        ? ZachZoneCommandResponse.Success()
        : ZachZoneCommandResponse.Failure(result.Errors.ToDictionary(k => k.Code, v => v.Description));
    }
    catch (DbUpdateException e)
    {
      await txn.RollbackAsync();
      return ZachZoneCommandResponse.Failure("Database Error", e.Message);
    }


  }

  public async Task<bool> DoesUserExist(int userId)
  {
    var user = await userManager.FindByIdAsync(userId.ToString(CultureInfo.InvariantCulture));
    return user is not null;
  }

  public async Task<bool> DoesUserExist(string email)
  {
    var user = await userManager.FindByEmailAsync(email);
    return user is not null;
  }

  public async Task<User?> GetUser(string email)
  {
    return await userManager.FindByEmailAsync(email);
  }

  public async Task<User?> GetUser(int id)
  {
    return await userManager.FindByIdAsync(id.ToString(CultureInfo.InvariantCulture));
  }

  public async Task<bool> SignIn(string email, string password)
  {
    var result = await signInManager.PasswordSignInAsync(email, password, true, false);
    return result.Succeeded;
  }

  public async Task<bool> ResetPassword(int id, string token, string password)
  {
    var user = await GetUser(id);
    Guard.Against.Null(user, null, "User not found.");
    var result = await userManager.ResetPasswordAsync(user, token, password);

    return result.Succeeded;
  }

  public async Task LogoutCurrentUser()
  {
    await signInManager.SignOutAsync();
  }

  public async Task<string> GetPasswordResetToken()
  {
    Guard.Against.False(currentUserService.UserId.HasValue);

    return await GetPasswordResetToken(currentUserService.UserId!.Value);
  }

  public async Task<string> GetPasswordResetToken(int userId)
  {
    var user = await GetUser(userId);
    Guard.Against.Null(user, null, "User not found.");

    return await userManager.GeneratePasswordResetTokenAsync(user);
  }

  public async Task<string?> GetClaim(int userId, string claimType)
  {
    var user = await GetUser(userId);
    Guard.Against.Null(user, null, "User not found.");

    var claims = await userManager.GetClaimsAsync(user);
    return claims.FirstOrDefault(x => x.Type == claimType)?.Value;
  }
  public async Task<bool> HasClaim(int userId, string claimType, string? claimValue = null)
  {
    var user = await GetUser(userId);
    Guard.Against.Null(user, null, "User not found.");

    var claims = await userManager.GetClaimsAsync(user);
    return claims.Any(x => x.Type == claimType && (claimValue == null || x.Value == claimValue));
  }
  public async Task<bool> AddClaim(int userId, string claimType, string claimValue)
  {
    var user = await GetUser(userId);
    Guard.Against.Null(user, null, "User not found.");

    var result = await userManager.AddClaimAsync(user, new System.Security.Claims.Claim(claimType, claimValue));

    return result.Succeeded;
  }
  public async Task<bool> RemoveClaim(int userId, string claimType, string claimValue)
  {
    var user = await GetUser(userId);
    Guard.Against.Null(user, null, "User not found.");

    var result = await userManager.RemoveClaimAsync(user, new System.Security.Claims.Claim(claimType, claimValue));

    return result.Succeeded;
  }
  public async Task<string[]> GetRoles(int userId)
  {
    var user = await GetUser(userId);
    Guard.Against.Null(user, null, "User not found.");

    var claims = await userManager.GetClaimsAsync(user);

    return claims
      .Where(c => c.Type == ClaimTypes.Role)
      .Select(x => x.Value)
      .ToArray();
  }

}