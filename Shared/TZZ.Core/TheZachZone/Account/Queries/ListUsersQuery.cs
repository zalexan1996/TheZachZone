using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Shared.Interfaces;
using TZZ.Core.Shared;
using TZZ.Core.Shared.Services;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.Core.TheZachZone.Account.Queries;

public class ListUsersDto
{
    public int UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string[] Roles { get; set; } = [];
}

public class ListUsersQuery : IRequest<ZachZoneCommandResponse<List<ListUsersDto>>>
{
    public int? UserId { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
}

public class ListUsersQueryHandler(IDatabaseService dbContext, IIdentityService identityService)
    : IRequestHandler<ListUsersQuery, ZachZoneCommandResponse<List<ListUsersDto>>>
{
    public async Task<ZachZoneCommandResponse<List<ListUsersDto>>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        var results = await dbContext.Set<User>()
            .Where(x => request.UserId == null || request.UserId == x.Id)
            .Where(x => request.Email == null || request.Email.ToLower() == x.Email!.ToLower())
            .Where(x => request.Name == null ||
                (x.FirstName == null ? false : x.FirstName.Contains(request.Name) ||
                x.LastName == null ? false : x.LastName.Contains(request.Name)))
            .Select(x => new ListUsersDto
            {
                UserId = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
            })
            .ToListAsync(cancellationToken);

        foreach (var r in results)
        {
            r.Roles = await identityService.GetRoles(r.UserId);
        }
        return ZachZoneCommandResponse.Success(results);
    }
}