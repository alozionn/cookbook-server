using Microsoft.EntityFrameworkCore;

public class GetRecipeListResponse
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }

    // public string imageUrl { get; set; }
    // public List<string> Ingredients { get; set; }
    // public List<string> Instructions { get; set; }
}

public class GetRecipeList : EndpointWithoutRequest<List<GetRecipeListResponse>>
{
    private readonly AppDbContext _context;

    public GetRecipeList(AppDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/public/recipes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var recipes = await _context
            .Recipes.Select(r => new GetRecipeListResponse
            {
                Name = r.Name,
                Description = r.Description,
                Category = r.Category,
            })
            .ToListAsync(ct);

        await Send.OkAsync(recipes);
    }
}
