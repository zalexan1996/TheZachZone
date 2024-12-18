﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Enums;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.Core.TheZachZone.Account.Queries;

public class GeneralInformationDto
{
    public int? Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName => FirstName is not null && LastName is not null ? $"{FirstName} {LastName}" : null;
    public string[] Roles { get; set; } = [];
    public bool IsAdmin => Roles.Contains(ZachZoneConstants.Roles.Admin);
}

public class GetGeneralInformationQuery : IRequest<ZachZoneCommandResponse<GeneralInformationDto>> { }

public class GetGeneralInformationQueryHandler(ICurrentUserService currentUserService, IDatabaseService dbContext, IIdentityService identityService)
    : IRequestHandler<GetGeneralInformationQuery, ZachZoneCommandResponse<GeneralInformationDto>>
{
    public async Task<ZachZoneCommandResponse<GeneralInformationDto>> Handle(GetGeneralInformationQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Set<User>()
            .SingleOrDefaultAsync(x => x.Id == currentUserService.UserId, cancellationToken);
        if (user is null)
        {
            var errorResponse = new ZachZoneCommandResponse<GeneralInformationDto>()
            {
                Result = new(),
                Errors =
                {
                    { "Id", "User is not logged in." }
                }
            };
            return errorResponse;
        }
        var roles = await identityService.GetRoles(user.Id);

        return ZachZoneCommandResponse.Success(new GeneralInformationDto()
        {
            Email= user.Email,
            FirstName= user.FirstName,
            LastName= user.LastName,
            Id = user.Id,
            Roles = roles ?? []
        });
    }
}
