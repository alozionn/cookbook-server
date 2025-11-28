using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> opts) : DbContext(opts)
{
    public DbSet<Recipe> Recipes => Set<Recipe>();
}
// using System;
// using System.Collections.Generic;
// public partial class AppDbContext : DbContext
// {
//     public AppDbContext(DbContextOptions<AppDbContext> options)
//         : base(options) { }

//     public DbSet<Recipe> Recipes { get; set; }
// }
