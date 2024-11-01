using MediatR;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;
using static TZZ.Common.Shared.Enums.ZachZoneConstants;

namespace TZZ.Core.TheZachZone.Account.Commands;

public class CreateAccountCommand : IRequest<ZachZoneCommandResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
}

public class CreateAccountCommandHandler(IIdentityService _identityService) : IRequestHandler<CreateAccountCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var createUserResponse = await _identityService.CreateUser(request);
        if (!createUserResponse.IsValid)
        {
            return createUserResponse;
        }

        var roleResult = await _identityService.AddClaim(createUserResponse.Result!.Id, ClaimTypes.Role, Roles.User);

        if (!roleResult)
        {
            createUserResponse.Errors.Add("Exception", "Failed to add user role to requested user.");
        }

        return createUserResponse;
    }
}