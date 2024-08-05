using TGZ.API;
using TZZ.Common;
using TZZ.Core;
using TZZ.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCommon(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCore(builder.Configuration);
builder.Services.AddTheGameZone(builder.Environment, builder.Configuration);

var app = builder.Build();

app.ConfigureTheGameZone();

app.Run();
