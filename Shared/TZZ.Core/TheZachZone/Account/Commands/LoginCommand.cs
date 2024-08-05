using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;

namespace TZZ.Core.TheZachZone.Account.Commands;

public class LoginCommand : IRequest<ZachZoneCommand<bool>>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class LoginCommandHandler(IIdentityService _identityService) : IRequestHandler<LoginCommand, ZachZoneCommand<bool>>
{
    public async Task<ZachZoneCommand<bool>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var signInSuccessful = await _identityService.SignIn(request.Email, request.Password);

        if (signInSuccessful)
        {
            return ZachZoneCommand.Success(true);
        }

        return ZachZoneCommand.Failure<bool>("Email", "Sign in failed.");
    }
}
