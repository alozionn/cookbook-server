global using FastEndpoints;
global using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();

var app = builder.Build();
app.UseFastEndpoints();
app.Run();
// https://dev.to/djnitehawk/building-rest-apis-in-net-6-the-easy-way-3h0d
