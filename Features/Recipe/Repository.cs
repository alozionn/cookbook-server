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

    public async Task<Recipe> CreateAsync(Recipe recipe, CancellationToken ct)
    {
        await _dbContext.Recipes.AddAsync(recipe, ct);

        await _dbContext.SaveChangesAsync(ct);

        return recipe;
    }
}
