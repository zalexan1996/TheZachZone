using Cocona;
using MediatR;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using System.Globalization;
using TZZ.Common;
using TZZ.Console;
using TZZ.Core;
using TZZ.Core.TheZachZone.Account.Commands;
using TZZ.Core.TheZachZone.Account.Queries;
using TZZ.Infrastructure;


var builder = CoconaApp.CreateBuilder();

builder.Services.AddCommon(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCore(builder.Configuration);
builder.Services.AddConsole(builder.Configuration);

var app = builder.Build();

app.AddSubCommand("user", x =>
{
  x.AddCommand("list", async (ISender sender, int? userId, string? email, string? name) =>
  {

    var response = await sender.Send(new ListUsersQuery()
    {
      UserId = userId,
      Email = email,
      Name = name
    });

    if (!response.IsValid)
    {
      AnsiConsole.WriteLine("[red]Command failed.[/]");
      foreach (var kvp in response.Errors)
      {
        AnsiConsole.Write(new Markup($"[red] - {kvp.Key} - {kvp.Value}[/]"));
      }
    }

    var table = new Table();
    table.AddColumn("UserId");
    table.AddColumn("Email");
    table.AddColumn("Name");
    table.AddColumn("Roles");

    response.Result?.ForEach(r => table.AddRow(r.UserId.ToString(), r.Email ?? string.Empty, $"{r.FirstName} {r.LastName}", string.Join(", ", r.Roles)));
    AnsiConsole.Write(table);

  });

  x.AddCommand("grant-admin", async (ISender sender, int userId) =>
  {
    var response = await sender.Send(new GrantAdminAccessCommand()
    {
      UserId = userId,
    });

    if (!response.IsValid)
    {
      AnsiConsole.Write(new Markup("[red]Command failed.[/]"));
      foreach (var kvp in response.Errors)
      {
        AnsiConsole.Write(new Markup($"[red] - {kvp.Key} - {kvp.Value}[/]"));
      }
    }

    AnsiConsole.Write(new Markup("[green]Role successfully added![/]"));
  });
});

app.Run();