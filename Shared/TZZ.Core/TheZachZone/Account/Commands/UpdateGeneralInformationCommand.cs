using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.Core.TheZachZone.Account.Commands;

public class UpdateGeneralInformationCommand : IRequest<ZachZoneCommandResponse>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}

public class UpdateGeneralInformationCommandHandler(IDatabaseService _dbService, ICurrentUserService _currentUserService) : IRequestHandler<UpdateGeneralInformationCommand, ZachZoneCommandResponse>
{
    public async Task<ZachZoneCommandResponse> Handle(UpdateGeneralInformationCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        Guard.Against.Null(userId, null, "User not found.");

        var user = await _dbService.Set<User>().SingleAsync(x => x.Id == userId, cancellationToken);

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        
        await _dbService.SaveChanges(cancellationToken);

        return ZachZoneCommandResponse.Success();
    }
}
