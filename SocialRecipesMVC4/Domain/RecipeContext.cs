using System.Data.Entity;

namespace SocialRecipesMVC4.Domain
{
    public class RecipeContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}