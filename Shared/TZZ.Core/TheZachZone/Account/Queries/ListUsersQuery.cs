using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Core.Common.Services;
using TZZ.Domain.Entities.TheZachZone;

namespace TZZ.Core.TheZachZone.Account.Queries;

public class ListUsersDto
{
  public int UserId { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public string? Email { get; set; }
  [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Setter required to replace roles.")]
  public IList<string> Roles { get; set; } = [];
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
    var results = await dbContext.Entity<User>()
      .Where(x => request.UserId == null || request.UserId == x.Id)
      .Where(x => request.Email == null || x.Email!.ToLower() == request.Email.ToLower())
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