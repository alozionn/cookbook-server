using Microsoft.EntityFrameworkCore;

public class GetRecipeRequest
{
    public required int Id { get; set; }
}

public class GetRecipeResponse
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }

    // public string imageUrl { get; set; }
    // public List<string> Ingredients { get; set; }
    // public List<string> Instructions { get; set; }
}

public class GetRecipe : Endpoint<GetRecipeRequest, GetRecipeResponse>
{
    private readonly AppDbContext _context;

    public GetRecipe(AppDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/public/recipes/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetRecipeRequest req, CancellationToken ct)
    {
        var recipe = await _context
            .Recipes.Where(r => r.Id == req.Id)
            .Select(r => new GetRecipeResponse
            {
                Name = r.Name,
                Description = r.Description,
                Category = r.Category,
            })
            .SingleAsync(ct);

        await Send.OkAsync(recipe);
    }
}
