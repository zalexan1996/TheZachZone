using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.Core.TheZachZone.Account.Queries;

public record GeneralInformationDto(string? email, string? firstName, string? lastName);

public class GetGeneralInformationQuery : IRequest<ZachZoneCommand<GeneralInformationDto>> { }

public class GetGeneralInformationQueryHandler(ICurrentUserService currentUserService, IDatabaseService dbContext)
    : IRequestHandler<GetGeneralInformationQuery, ZachZoneCommand<GeneralInformationDto>>
{
    public async Task<ZachZoneCommand<GeneralInformationDto>> Handle(GetGeneralInformationQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Set<User>()
            .SingleAsync(x => x.Id == currentUserService.UserId, cancellationToken);

        return ZachZoneCommand.Success(new GeneralInformationDto(user.Email, user.FirstName, user.LastName));
    }
}
