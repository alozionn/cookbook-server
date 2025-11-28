using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> opts) : DbContext(opts)
{
    public DbSet<Recipe> Recipes => Set<Recipe>();
}
