using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SocialRecipesMVC4.Domain;
using SocialRecipesMVC4.Models;

namespace SocialRecipesMVC4.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private RecipeContext _recipeContext;

        public GroupController(RecipeContext recipeContext)
        {
            _recipeContext = recipeContext;
        }

        //
        // GET: /Group/

        public ActionResult Index()
        {
            DateTime twoDaysAgo = DateTime.Now.AddDays(-2);
            IEnumerable<GroupSummaryModel> summaries =
                _recipeContext.Groups.OrderBy(g => g.Name)
                    .Select(g => new GroupSummaryModel
                        {
                            Id = g.Id,
                            Name = g.Name,
                            Description =  g.Description,
                            UserCount = g.Users.Count(),
                            NewRecipesCount = g.Recipes.Count(r => r.PostedOn > twoDaysAgo)
                        });
            return View(summaries);
        }

        //
        // GET: /Group/Details/5
        public ActionResult Details(int id)
        {
            Group group = _recipeContext.Groups.Single(g => g.Id == id);
            ViewBag.RecentRecipes = group.Recipes.OrderByDescending(r => r.PostedOn).Take(5);
            return View(group);
        }

        //
        // GET: /Group/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Group/Create

        [HttpPost]
        public ActionResult Create(Group group)
        {
            try
            {
                User currentUser = _recipeContext.Users.Single(u => u.Id == User.Identity.Name);
                group.Users.Add(currentUser);
                currentUser.Groups.Add(group);
                _recipeContext.Groups.Add(group);
                _recipeContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Join(int id)
        {
            User currentUser = _recipeContext.Users.Single(u => u.Id == User.Identity.Name);
            if (!currentUser.Groups.Any(g => g.Id == id))
            {
                Group group = _recipeContext.Groups.Single(g => g.Id == id);
                group.Users.Add(currentUser);
                currentUser.Groups.Add(group);
                _recipeContext.SaveChanges();
            }
            return RedirectToAction("Details", new {id});
        }

    }
}
