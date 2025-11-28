using Microsoft.EntityFrameworkCore;

public class RecipesRepository
{
    private readonly AppDbContext _dbContext;

    public RecipesRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<RecipeResponse>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext
            .Recipes.Select(r => new RecipeResponse
            {
                Name = r.Name,
                Description = r.Description,
                Category = r.Category,
            })
            .ToListAsync(ct);
    }

    public async Task<CreateRecipeResponse> CreateAsync(RecipeRequest req, CancellationToken ct)
    {
        var recipe = new Recipe
        {
            Name = req.Name,
            Description = req.Description,
            Category = req.Category,
        };
        await _dbContext.Recipes.AddAsync(recipe, ct);

        await _dbContext.SaveChangesAsync(ct);

        return new CreateRecipeResponse { Id = recipe.Id, Name = recipe.Name };
    }
}
