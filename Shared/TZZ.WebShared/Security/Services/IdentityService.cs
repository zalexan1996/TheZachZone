using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TZZ.Common;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;
using TZZ.Core.TheZachZone.Account.Commands;
using TZZ.Domain.Entities.TheGameZone;
using TZZ.Domain.Entities.TheZachZone;
using static TZZ.Common.Shared.Enums.ZachZoneConstants;

namespace TZZ.WebShared.Security.Services;

internal class IdentityService(IDatabaseService dbContext, ICurrentUserService _currentUserService, UserManager<User> _userManager, SignInManager<User> _signInManager) : IIdentityService
{
    public async Task<ZachZoneCommandResponse<User>> CreateUser(CreateAccountCommand command)
    {
        var result = await _userManager.CreateAsync(new User()
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
            var games = dbContext.Set<Game>()
                .Where(x => x.AuthorId == userId).ToList();
            dbContext.RemoveRange(games);
            await dbContext.SaveChanges(default);

            await txn.CommitAsync();

            // Delete user account.
            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded
                ? ZachZoneCommandResponse.Success()
                : ZachZoneCommandResponse.Failure(result.Errors.ToDictionary(k => k.Code, v => v.Description));
        }
        catch (Exception e)
        {
            await txn.RollbackAsync();
            return ZachZoneCommandResponse.Failure("Database Error", e.Message);
        }


    }

    public async Task<bool> DoesUserExist(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        return user is not null;
    }

    public async Task<bool> DoesUserExist(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user is not null;
    }

    public async Task<User?> GetUser(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<User?> GetUser(int id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<bool> SignIn(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, true, false);
        return result.Succeeded;
    }

    public async Task<bool> ResetPassword(int id, string token, string password)
    {
        var user = await GetUser(id);
        Guard.Against.Null(user, null, "User not found.");
        var result = await _userManager.ResetPasswordAsync(user, token, password);

        return result.Succeeded;
    }

    public async Task LogoutCurrentUser()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<string> GetPasswordResetToken()
    {
        Guard.Against.False(_currentUserService.UserId.HasValue);

        return await GetPasswordResetToken(_currentUserService.UserId!.Value);
    }

    public async Task<string> GetPasswordResetToken(int userId)
    {
        var user = await GetUser(userId);
        Guard.Against.Null(user, null, "User not found.");

        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<string?> GetClaim(int userId, string claimType)
    {
        var user = await GetUser(userId);
        Guard.Against.Null(user, null, "User not found.");

        var claims = await _userManager.GetClaimsAsync(user);
        return claims.FirstOrDefault(x => x.Type == claimType)?.Value;
    }
    public async Task<bool> HasClaim(int userId, string claimType, string? claimValue = null)
    {
        var user = await GetUser(userId);
        Guard.Against.Null(user, null, "User not found.");

        var claims = await _userManager.GetClaimsAsync(user);
        return claims.Any(x => x.Type == claimType && (claimValue == null || x.Value == claimValue));
    }
    public async Task<bool> AddClaim(int userId, string claimType, string claimValue)
    {
        var user = await GetUser(userId);
        Guard.Against.Null(user, null, "User not found.");

        var result = await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(claimType, claimValue));

        return result.Succeeded;
    }
    public async Task<bool> RemoveClaim(int userId, string claimType, string claimValue)
    {
        var user = await GetUser(userId);
        Guard.Against.Null(user, null, "User not found.");

        var result = await _userManager.RemoveClaimAsync(user, new System.Security.Claims.Claim(claimType, claimValue));

        return result.Succeeded;
    }
    public async Task<string[]> GetRoles(int userId)
    {
        var user = await GetUser(userId);
        Guard.Against.Null(user, null, "User not found.");

        var claims = await _userManager.GetClaimsAsync(user);

        return claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(x => x.Value)
            .ToArray();
    }

}