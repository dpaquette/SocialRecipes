using System;

namespace SocialRecipesMVC4.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public DateTime PostedOn { get; set; }
        public string CommentValue { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}