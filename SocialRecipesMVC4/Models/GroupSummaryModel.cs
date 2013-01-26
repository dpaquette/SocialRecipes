using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialRecipesMVC4.Models
{
    public class GroupSummaryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserCount { get; set; }
        public int NewRecipesCount { get; set; }
    }
}