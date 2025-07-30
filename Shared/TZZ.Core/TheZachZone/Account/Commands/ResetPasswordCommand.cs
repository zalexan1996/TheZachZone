using Ardalis.GuardClauses;
using MediatR;
using TZZ.Core.Common;
using TZZ.Core.Common.Services;

namespace TZZ.Core.TheZachZone.Account.Commands;

public class ResetPasswordCommand : IRequest<ZachZoneCommandResponse>
{
  public required string ResetToken { get; set; }
  public required string NewPassword { get; set; }
  public required string ConfirmPassword { get; set; }
}

public class ResetPasswordCommandHandler(ICurrentUserService currentUserService, IIdentityService identityService)
  : IRequestHandler<ResetPasswordCommand, ZachZoneCommandResponse>
{
  public async Task<ZachZoneCommandResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
  {
    var userId = currentUserService.UserId;
    Guard.Against.Null(userId, null, "User not found.");

    var result = await identityService.ResetPassword(userId.Value, request.ResetToken, request.NewPassword).ConfigureAwait(false);

    return result ? ZachZoneCommandResponse.Success() : ZachZoneCommandResponse.Failure("summary", "Failed to reset password.");
  }
}
