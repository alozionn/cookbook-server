public class CreateRecipeRequest
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }

    // public string imageUrl { get; set; }
    // public List<string> Ingredients { get; set; }
    // public List<string> Instructions { get; set; }
}

public class CreateRecipeResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}

public class CreateRecipe : EndpointWithMapping<CreateRecipeRequest, CreateRecipeResponse, Recipe>
{
    private readonly AppDbContext _context;

    public CreateRecipe(AppDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Post("/admin/recipes");
        AllowAnonymous(); //TODO: refactor when auth is added
    }

    public override async Task HandleAsync(CreateRecipeRequest req, CancellationToken ct)
    {
        Recipe entity = MapToEntity(req);

        await _context.Recipes.AddAsync(entity, ct);

        await _context.SaveChangesAsync(ct);

        Response = MapFromEntity(entity);
        await Send.OkAsync(Response);
    }

    public override Recipe MapToEntity(CreateRecipeRequest r) =>
        new()
        {
            Name = r.Name,
            Description = r.Description,
            Category = r.Category,
        };

    public override CreateRecipeResponse MapFromEntity(Recipe e) =>
        new() { Id = e.Id, Name = e.Name };
}
