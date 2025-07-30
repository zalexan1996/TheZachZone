using MediatR;
using TZZ.Core.Common;
using TZZ.Core.Common.Services;

namespace TZZ.Core.TheZachZone.Account.Commands;

public class LoginCommand : IRequest<ZachZoneCommandResponse<bool>>
{
  public required string Email { get; set; }
  public required string Password { get; set; }
}

public class LoginCommandHandler(IIdentityService identityService) : IRequestHandler<LoginCommand, ZachZoneCommandResponse<bool>>
{
  public async Task<ZachZoneCommandResponse<bool>> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    var signInSuccessful = await identityService.SignIn(request.Email, request.Password);

    if (signInSuccessful)
    {
      return ZachZoneCommandResponse.Success(true);
    }

    return ZachZoneCommandResponse.Failure<bool>("Email", "Sign in failed.");
  }
}
