public class CreateRecipe : Endpoint<RecipeRequest, CreateRecipeResponse>
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

    public override async Task HandleAsync(RecipeRequest req, CancellationToken ct)
    {
        var recipe = await RecipesRepository.CreateAsync(req, ct);

        await Send.OkAsync(recipe);
    }
}
