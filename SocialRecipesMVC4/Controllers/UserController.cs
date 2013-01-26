using System.Linq;
using System.Web.Mvc;
using SocialRecipesMVC4.Domain;

namespace SocialRecipesMVC4.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        private RecipeContext _recipeContext;

        public UserController(RecipeContext recipeContext)
        {
            _recipeContext = recipeContext;
        }

        //
        // GET: /User/Details/5
        public ActionResult Details(string id)
        {
            User user = _recipeContext.Users.Single(s => s.Id.ToUpper() == id.ToUpper());

            ViewBag.RecentRecipes = user.Recipes.OrderByDescending(r => r.PostedOn).Take(5);
            ViewBag.RecentComments = user.Comments.OrderByDescending(c => c.PostedOn).Take(5);
            return View(user);
        }

    }
}
