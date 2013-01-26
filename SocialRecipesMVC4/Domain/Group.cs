using System.Collections.Generic;

namespace SocialRecipesMVC4.Domain
{
    public class Group
    {
        public Group()
        {
            Users = new HashSet<User>();
            Recipes = new HashSet<Recipe>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
