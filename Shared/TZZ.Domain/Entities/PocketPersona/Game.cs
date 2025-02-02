﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TZZ.Domain.Entities.PocketPersona;

public class Game
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class GameEntityTypeConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable(nameof(Game), "PP");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.Name).HasMaxLength(100);

        builder.HasIndex(x => x.Name).IsUnique();
    }
}