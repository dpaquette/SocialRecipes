using System;
using System.Linq;
using System.Web.Mvc;
using SocialRecipesMVC4.Domain;

namespace SocialRecipesMVC4.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        private RecipeContext _recipeContext;

        public RecipeController(RecipeContext recipeContext)
        {
            _recipeContext = recipeContext;
        }

        //
        // GET: /Recipe/
        public ActionResult Index()
        {
            User currentUser = _recipeContext.Users.Include("Recipes").Include("Recipes.Comments").Single(u => u.Id.ToUpper() == User.Identity.Name.ToUpper());
            return View(currentUser.Recipes);
        }

        //
        // GET: /Recipe/Details/5

        public ActionResult Details(int id)
        {
            return View(_recipeContext.Recipes.Single(r => r.Id == id));
        }

        //
        // GET: /Recipe/Create

        public ActionResult Create()
        {
            User currentUser = _recipeContext.Users.Single(u => u.Id.ToUpper() == User.Identity.Name.ToUpper());
            ViewBag.Groups = currentUser.Groups;
            return View();
        }

        //
        // POST: /Recipe/Create

        [HttpPost]
        public ActionResult Create(Recipe recipe, int[] groups)
        {
            try
            {
                User currentUser = _recipeContext.Users.Single(u => u.Id.ToUpper() == User.Identity.Name.ToUpper());
                recipe.User = currentUser;
                recipe.PostedOn = DateTime.Now;
                IQueryable<Group> selectedGroups = _recipeContext.Groups.Where(g => groups.Contains(g.Id));
                foreach (Group selectedGroup in selectedGroups)
                {
                    recipe.Groups.Add(selectedGroup);
                }
                currentUser.Recipes.Add(recipe);
                _recipeContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Recipe/Edit/5

        public ActionResult Edit(int id)
        {
            User currentUser = _recipeContext.Users.Single(u => u.Id.ToUpper() == User.Identity.Name.ToUpper());
            ViewBag.Groups = currentUser.Groups;
            return View(_recipeContext.Recipes.Single(r => r.Id == id));
        }

        //
        // POST: /Recipe/Edit/5

        [HttpPost]
        public ActionResult Edit(Recipe recipe, int[] groups)
        {
            Recipe originalRecipe = _recipeContext.Recipes.Single(r => r.Id == recipe.Id);
            originalRecipe.Name = recipe.Name;
            originalRecipe.Description = recipe.Description;
            originalRecipe.Ingredients = recipe.Ingredients;
            originalRecipe.Directions = recipe.Directions;

            if (groups == null)
            {
                originalRecipe.Groups.Clear();
            }
            else
            {
                foreach (Group groupToRemove in originalRecipe.Groups.Where(g => !groups.Contains(g.Id)).ToArray())
                {
                    originalRecipe.Groups.Remove(groupToRemove);
                }    
            }
            

            if (groups != null)
            {
                foreach (int groupIdToAdd in groups.Where(groupId => !originalRecipe.Groups.Any(g => g.Id == groupId)))
                {
                    originalRecipe.Groups.Add(_recipeContext.Groups.Single(g => g.Id == groupIdToAdd));
                }
            }

            _recipeContext.SaveChanges();

            return RedirectToAction("Index");
        }

        //
        // GET: /Recipe/Delete/5

        public ActionResult Delete(int id)
        {

            Recipe recipe = _recipeContext.Recipes.Single(r => r.Id == id);
            _recipeContext.Recipes.Remove(recipe);
            _recipeContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddComment(int recipeId, string commentValue)
        {
            Comment comment = new Comment { CommentValue = commentValue };
            User currentUser = _recipeContext.Users.Single(u => u.Id.ToUpper() == User.Identity.Name.ToUpper());
            comment.User = currentUser;
            comment.Recipe = _recipeContext.Recipes.Single(r => r.Id == recipeId);
            comment.PostedOn = DateTime.Now;
            _recipeContext.Comments.Add(comment);
            _recipeContext.SaveChanges();

            return RedirectToAction("Details", new {id = recipeId});
        }
    }
}