using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using TZZ.Common;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;
using TZZ.Core.TheZachZone.Account.Commands;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.WebShared.Security.Services;

internal class IdentityService(ICurrentUserService _currentUserService, UserManager<User> _userManager, RoleManager<Role> _roleManager, SignInManager<User> _signInManager) : IIdentityService
{
    public async Task<ZachZoneCommand<User>> CreateUser(CreateAccountCommand command)
    {
        var result = await _userManager.CreateAsync(new User()
        {
            Email = command.Email,
            UserName = command.Email
        }, command.Password);

        return result.Succeeded ? 
            ZachZoneCommand.Success((await GetUser(command.Email))!) 
            : ZachZoneCommand.Failure<User>(result.Errors.ToDictionary(k => k.Code, v => v.Description));
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
}
