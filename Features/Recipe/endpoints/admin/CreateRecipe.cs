public class CreateRecipe : EndpointWithMapping<CreateRecipeRequest, CreateRecipeResponse, Recipe>
{
    private readonly AppDbContext _context;

    public CreateRecipe(AppDbContext context)
    {
        _context = context;
    }

    public RecipesRepository RecipesRepository => new RecipesRepository(_context);

    public override void Configure()
    {
        Post("/recipes");
        AllowAnonymous(); //TODO: refactor when auth is added
    }

    public override async Task HandleAsync(CreateRecipeRequest req, CancellationToken ct)
    {
        Recipe entity = MapToEntity(req);
        var recipe = await RecipesRepository.CreateAsync(entity, ct);

        Response = MapFromEntity(recipe);
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
