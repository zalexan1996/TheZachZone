using MediatR;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;

namespace TZZ.Core.TheZachZone.Account.Commands;

public class CreateAccountCommand : IRequest<ZachZoneCommand>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
}

public class CreateAccountCommandHandler(IIdentityService _identityService) : IRequestHandler<CreateAccountCommand, ZachZoneCommand>
{
    public async Task<ZachZoneCommand> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.CreateUser(request);
    }
}