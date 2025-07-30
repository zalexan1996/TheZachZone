using MediatR;
using TZZ.Core.Common;
using TZZ.Core.Common.Services;

namespace TZZ.Core.TheZachZone.Account.Queries;

public class DoesUserExistQuery : IRequest<ZachZoneCommandResponse<bool>>
{
  public DoesUserExistQuery(string email)
  {
    Email = email;
  }

  public string Email { get; set; }
}

public class DoesUserExistQueryHandler(IIdentityService identityService) : IRequestHandler<DoesUserExistQuery, ZachZoneCommandResponse<bool>>
{
  public async Task<ZachZoneCommandResponse<bool>> Handle(DoesUserExistQuery request, CancellationToken cancellationToken)
  {
    var result = await identityService.DoesUserExist(request.Email);

    var output = ZachZoneCommandResponse.Success(result);

    if (result)
    {
      output.Infos.Add("Email", "The user exists.");
    }
    else
    {

      output.Infos.Add("Email", "The user does not exist.");
    }

    return output;
  }
}
