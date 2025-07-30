using MediatR;
using Microsoft.EntityFrameworkCore;
using TZZ.Common.Interfaces;
using TZZ.Core.Common;
using TZZ.Domain.Entities.PocketPersona;

namespace TZZ.Core.PocketPersona.SocialLinks;

public class SetSocialLinkCommand : IRequest<ZachZoneCommandResponse>
{
  public required string Name { get; set; }
  public required int ArcanaId { get; set; }
  public required int CharacterId { get; set; }
}

public class AddSocialLinkCommandHandler(IDatabaseService dbContext)
  : IRequestHandler<SetSocialLinkCommand, ZachZoneCommandResponse>
{
  private Arcana? arcana;
  private Character? character;

  public async Task<ZachZoneCommandResponse> Handle(SetSocialLinkCommand request, CancellationToken cancellationToken)
  {
    if ((arcana = await TryGetArcana(request.ArcanaId, cancellationToken)) is null)
      return ArcanaNotFoundResult();

    if ((character = await TryGetCharacter(request.CharacterId, cancellationToken)) is null)
      return CharacterNotFoundResult();

    await ProcessSocialLink(request, cancellationToken);

    return ZachZoneCommandResponse.Success();
  }

  private async Task ProcessSocialLink(SetSocialLinkCommand request, CancellationToken cancellationToken)
  {
    await RemoveConflictingSocialLink(request.ArcanaId, character!.GameId, request.CharacterId, cancellationToken);
    await SetSocialLink(request, character, arcana!, cancellationToken);
    await dbContext.SaveChanges(cancellationToken);
  }

  private static ZachZoneCommandResponse CharacterNotFoundResult()
    => ZachZoneCommandResponse.Failure(nameof(SetSocialLinkCommand.CharacterId), "The specified character does not exist.");

  private static ZachZoneCommandResponse ArcanaNotFoundResult()
    => ZachZoneCommandResponse.Failure(nameof(SetSocialLinkCommand.ArcanaId), "The specified arcana does not exist.");

  private Task<Arcana?> TryGetArcana(int arcanaId, CancellationToken cancellationToken)
    => dbContext.Entity<Arcana>().SingleOrDefaultAsync(x => x.Id == arcanaId, cancellationToken);

  private Task<Character?> TryGetCharacter(int characterId, CancellationToken cancellationToken)
    => dbContext.Entity<Character>().SingleOrDefaultAsync(x => x.Id == characterId, cancellationToken);

  private async Task<SocialLink> SetSocialLink(SetSocialLinkCommand request, Character character, Arcana arcana, CancellationToken cancellationToken)
  {
    var socialLink = await dbContext.Entity<SocialLink>().SingleOrDefaultAsync(x => x.ArcanaId == request.ArcanaId && x.CharacterId == request.CharacterId, cancellationToken);
    if (socialLink is null)
    {
      socialLink = new SocialLink()
      {
        Name = request.Name,
        ArcanaId = request.ArcanaId,
        CharacterId = request.CharacterId,
        Arcana = arcana,
        Character = character,
      };
      await dbContext.Add(socialLink, cancellationToken);
    }
    else
    {
      socialLink.CharacterId = request.CharacterId;
      socialLink.Name = request.Name;
      socialLink.ArcanaId = request.ArcanaId;
    }

    return socialLink!;
  }

  private async Task RemoveConflictingSocialLink(int arcanaId, int gameId, int characterId, CancellationToken cancellationToken)
  {
    var conflictingSocialLink = await dbContext.Entity<SocialLink>()
      .SingleOrDefaultAsync(x => x.ArcanaId == arcanaId
                    && x.Character.GameId == gameId
                    && x.CharacterId != characterId, cancellationToken);

    if (conflictingSocialLink is not null)
    {
      dbContext.Remove(conflictingSocialLink);
    }
  }
}