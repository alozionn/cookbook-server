public class GetRecipeList : EndpointWithoutRequest<List<RecipeResponse>>
{
    private readonly AppDbContext _context;

    public GetRecipeList(AppDbContext context)
    {
        _context = context;
    }

    public RecipesRepository RecipesRepository => new RecipesRepository(_context);

    public override void Configure()
    {
        Get("/recipes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var recipes = await RecipesRepository.GetAllAsync(ct);

        await Send.OkAsync(recipes);
    }
}
