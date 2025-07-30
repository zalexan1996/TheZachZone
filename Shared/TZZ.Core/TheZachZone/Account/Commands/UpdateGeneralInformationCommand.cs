using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Core.Common.Services;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.Core.TheZachZone.Account.Commands;

public class UpdateGeneralInformationCommand : IRequest<ZachZoneCommandResponse>
{
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
}

public class UpdateGeneralInformationCommandHandler(IDatabaseService dbService, ICurrentUserService currentUserService) : IRequestHandler<UpdateGeneralInformationCommand, ZachZoneCommandResponse>
{
  public async Task<ZachZoneCommandResponse> Handle(UpdateGeneralInformationCommand request, CancellationToken cancellationToken)
  {
    var userId = currentUserService.UserId;
    Guard.Against.Null(userId, null, "User not found.");

    var user = await dbService.Entity<User>().SingleAsync(x => x.Id == userId, cancellationToken);

    user.FirstName = request.FirstName;
    user.LastName = request.LastName;

    await dbService.SaveChanges(cancellationToken);

    return ZachZoneCommandResponse.Success();
  }
}
