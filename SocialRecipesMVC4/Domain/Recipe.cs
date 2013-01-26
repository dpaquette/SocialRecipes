using System;
using System.Collections.Generic;

namespace SocialRecipesMVC4.Domain
{
    public class Recipe
    {
        public Recipe()
        {
            Groups = new HashSet<Group>();
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string Name { get; set;}
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Directions { get; set; }
        public DateTime PostedOn { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } 
    }
}