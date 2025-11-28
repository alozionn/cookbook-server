global using FastEndpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CookbookContext"))
);

var app = builder.Build();
app.UseFastEndpoints();
app.Run();

// robust api setup example: https://github.com/erwinkramer/bank-api/tree/main?tab=readme-ov-file#bank-api-
// https://dev.to/djnitehawk/building-rest-apis-in-net-6-the-easy-way-3h0d
// https://learn.microsoft.com/en-us/dotnet/core/docker/build-container?tabs=windows&pivots=dotnet-9-0
/*testing stack(possible)
Test runner     TUnit
Assertions      built-in (TUnit.Assertions)
Mocking         Moq (works fine with TUnit)
Integration     Microsoft.AspNetCore.Mvc.Testing
DB              optional Testcontainers; maybe in memory SQLite
Data fakes      Bogus
*/
