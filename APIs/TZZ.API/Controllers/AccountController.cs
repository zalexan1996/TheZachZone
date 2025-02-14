using MediatR;
using Microsoft.AspNetCore.Mvc;
using TZZ.Core.Shared;
using TZZ.Core.TheZachZone.Account.Commands;
using TZZ.Core.TheZachZone.Account.Queries;
using Microsoft.AspNetCore.Authorization;
using TZZ.Core.Shared.Services;
using TZZ.Common.Shared.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TZZ.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = ZachZoneConstants.Policies.Default)]
public class AccountController(ISender sender, IIdentityService identityService) : ControllerBase
{
    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<ActionResult<bool>> DoesUserExist(string email)
    {
        var result = await sender.Send(new DoesUserExistQuery(email));

        if (result.IsValid)
        {
            return Ok(result.Result);
        }
        return BadRequest(result);
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<ActionResult<ZachZoneCommandResponse>> CreateAccount(CreateAccountCommand command)
    {
        var result = await sender.Send(command);

        if (result.IsValid)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<ActionResult> Login(string email, string password)
    {
        var result = await sender.Send(new LoginCommand()
        { 
            Email = email,
            Password = password
        });

        if (result.IsValid && result.Result)
        {
            return Ok();
        }

        return Forbid();
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<string>> Logout()
    {
        var result = await sender.Send(new LogoutCommand());
        return Ok("http://localhost:8000");
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<UserInfoDto>> UserInfo()
    {
        var result = await sender.Send(new GetUserInfoQuery());

        if (result.IsValid)
        {
            return Ok(result.Result);
        }

        return BadRequest();
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<GeneralInformationDto>> GetGeneralInformation()
    {
        var result = await sender.Send(new GetGeneralInformationQuery());
        
        if (result.IsValid)
        {
            return Ok(result.Result);
        }

        return BadRequest();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateGeneralInformation(UpdateGeneralInformationCommand command)
    {
        var result = await sender.Send(command);

        if (result.IsValid)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpGet("[action]")]
    public async Task<string> GetPasswordResetToken()
    {
        return await identityService.GetPasswordResetToken();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
    {
        var result = await sender.Send(command);

        if (result.IsValid)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpGet("[action]")]
    [Authorize(Policy = ZachZoneConstants.Policies.Admin)]
    public async Task<ActionResult<ListUsersDto[]>> ListUsers()
    {
        var result = await sender.Send(new ListUsersQuery());

        if (result.IsValid)
        {
            return Ok(result.Result);
        }

        return BadRequest();
    }

    [HttpPost("[action]")]
    [Authorize(Policy = ZachZoneConstants.Policies.Admin)]
    public async Task<ActionResult<int>> GrantAdminAccess(int userId)
    {
        var result = await sender.Send(new GrantAdminAccessCommand()
        {
            UserId = userId
        });

        if (result.IsValid)
        {
            return Ok(result.Result);
        }

        return BadRequest(result);
    }

    [HttpPost("[action]")]
    [Authorize(Policy = ZachZoneConstants.Policies.Admin)]
    public async Task<ActionResult<int>> RevokeAdminAccess(int userId)
    {
        var result = await sender.Send(new RevokeAdminAccessCommand()
        {
            UserId = userId
        });

        if (result.IsValid)
        {
            return Ok(result.Result);
        }

        return BadRequest(result);
    }

    [HttpDelete("[action]")]
    [Authorize(Policy = ZachZoneConstants.Policies.Admin)]
    public async Task<ActionResult<ZachZoneCommandResponse>> DeleteUser(int userId)
    {
        var result = await sender.Send(new DeleteUserCommand()
        {
            UserId = userId
        });

        if (result.IsValid)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

}
