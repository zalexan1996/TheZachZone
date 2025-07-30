using MediatR;
using TZZ.Common.Enums.Constants;
using TZZ.Core.Common;
using TZZ.Core.Common.Services;

namespace TZZ.Core.TheZachZone.Account.Commands;

public class CreateAccountCommand : IRequest<ZachZoneCommandResponse>
{
  public required string Email { get; set; }
  public required string Password { get; set; }
  public required string ConfirmPassword { get; set; }
}

public class CreateAccountCommandHandler(IIdentityService identityService)
  : IRequestHandler<CreateAccountCommand, ZachZoneCommandResponse>
{
  public async Task<ZachZoneCommandResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
  {
    var createUserResponse = await identityService.CreateUser(request)
      .ConfigureAwait(false);
    if (!createUserResponse.IsValid)
    {
      return createUserResponse;
    }

    var roleResult = await identityService.AddClaim(createUserResponse.Result!.Id, ClaimTypes.Role, Roles.User)
      .ConfigureAwait(false);

    if (!roleResult)
    {
      createUserResponse.Errors.Add("Exception", "Failed to add user role to requested user.");
    }

    return createUserResponse;
  }
}