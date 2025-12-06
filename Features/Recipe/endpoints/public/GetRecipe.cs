public class GetRecipe : Endpoint<GetRecipeRequest, RecipeResponse>
{
    private readonly AppDbContext _context;

    public GetRecipe(AppDbContext context)
    {
        _context = context;
    }

    public RecipesRepository RecipesRepository => new RecipesRepository(_context);

    public override void Configure()
    {
        Get("/public/recipes/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetRecipeRequest req, CancellationToken ct)
    {
        var recipe = await RecipesRepository.GetAsync(req.Id, ct);

        await Send.OkAsync(recipe);
    }
}
