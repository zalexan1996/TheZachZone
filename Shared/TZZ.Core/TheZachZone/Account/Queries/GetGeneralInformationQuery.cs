using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.Core.TheZachZone.Account.Queries;

public record GeneralInformationDto(int? id, string? email, string? firstName, string? lastName);

public class GetGeneralInformationQuery : IRequest<ZachZoneCommandResponse<GeneralInformationDto>> { }

public class GetGeneralInformationQueryHandler(ICurrentUserService currentUserService, IDatabaseService dbContext)
    : IRequestHandler<GetGeneralInformationQuery, ZachZoneCommandResponse<GeneralInformationDto>>
{
    public async Task<ZachZoneCommandResponse<GeneralInformationDto>> Handle(GetGeneralInformationQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Set<User>()
            .SingleAsync(x => x.Id == currentUserService.UserId, cancellationToken);

        return ZachZoneCommandResponse.Success(new GeneralInformationDto(user.Id, user.Email, user.FirstName, user.LastName));
    }
}
