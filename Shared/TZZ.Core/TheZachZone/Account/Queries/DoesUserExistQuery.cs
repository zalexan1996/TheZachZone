using MediatR;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;

namespace TZZ.Core.TheZachZone.Account.Queries;

public class DoesUserExistQuery : IRequest<ZachZoneCommand<bool>>
{
    public DoesUserExistQuery(string email)
    {
        Email = email;
    }

    public string Email { get; set; }
}

public class DoesUserExistQueryHandler(IIdentityService _identityService) : IRequestHandler<DoesUserExistQuery, ZachZoneCommand<bool>>
{
    public async Task<ZachZoneCommand<bool>> Handle(DoesUserExistQuery request, CancellationToken cancellationToken)
    {
        return ZachZoneCommand.Success(await _identityService.DoesUserExist(request.Email));
    }
}
