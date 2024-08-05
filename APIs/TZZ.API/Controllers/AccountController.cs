﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using TZZ.Core.Shared;
using TZZ.Core.TheZachZone.Account.Commands;
using TZZ.Core.TheZachZone.Account.Queries;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TZZ.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(ISender sender) : ControllerBase
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
    public async Task<ActionResult<ZachZoneCommand>> CreateAccount(CreateAccountCommand command)
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
    [Authorize(Policy = "default")]
    public async Task<ActionResult<string>> Logout()
    {
        var result = await sender.Send(new LogoutCommand());
        return Ok("http://localhost:8000");
    }

    [HttpGet("[action]")]
    [Authorize(Policy = "default")]
    public async Task<ActionResult<UserInfoDto>> UserInfo()
    {
        var result = await sender.Send(new GetUserInfoQuery());

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
}
