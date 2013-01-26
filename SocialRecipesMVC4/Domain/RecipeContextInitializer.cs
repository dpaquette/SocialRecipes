using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace SocialRecipesMVC4.Domain
{
    public class RecipeContextInitializer : CreateDatabaseIfNotExists<RecipeContext>
    {
        protected override void Seed(RecipeContext context)
        {
            Random random = new Random();
            context.Users.Add(new User { Id = "dpaq", Name = "David Paquette" });
            context.Users.Add(new User { Id = "aschott", Name = "Alvin M. Schott" });
            context.Users.Add(new User { Id = "cbusses", Name = "Carson Busses" });
            context.Users.Add(new User { Id = "ebyrd", Name = "Earl E. Byrd" });
            context.Users.Add(new User { Id = "pcakes", Name = "Patty Cakes" });
            context.Users.Add(new User { Id = "danne", Name = "Derri Anne" });
            context.Users.Add(new User { Id = "mdess", Name = "Moe Dess" });
            context.Users.Add(new User { Id = "ldoggslife", Name = "Leda Doggslife" });
            context.Users.Add(new User { Id = "ddruff", Name = "Dan Druff" });
            context.Users.Add(new User { Id = "afresco", Name = "Al Fresco" });
            context.Users.Add(new User { Id = "ihoe", Name = "Ido Hoe" });
            context.Users.Add(new User { Id = "hkisses", Name = "Howie Kisses" });
            context.Users.Add(new User { Id = "lease", Name = "Len Lease" });
            context.Users.Add(new User { Id = "pmeup", Name = "Phil Meup" });
            context.Users.Add(new User { Id = "ipent", Name = "Ira Pent" });
            context.Users.Add(new User { Id = "rtattoo", Name = "Rose Tattoo" });
            context.Users.Add(new User { Id = "eburger", Name = "Etta Burger" });

            RandomPhraseGenerator groupNameGenerator = new RandomPhraseGenerator(
                new[] { "Calgary", "Edmonton", "Winnipeg", "Saskatoon", "Regina", "Vancouver", "Lethbridge", 
                "Barrie", "Belleville", "Brampton", "Brantford", "Cornwall", "Guelph", "Hamilton", "Kingston", "Kitchener-Waterloo", "London",
                "Mississauga", "Niagara", "Norfolk", "North Bay", "Oakville", "Oshawa", "Ottawa", "Peterborough", "Sarnia",
                "Sault Ste. Marie", "St. Catharines", "Thunder Bay", "Toronto", "Windsor", "York"},
                new[] { "Hikers", "Cycling Team", ".NET User Group", "Web Developers", "Triathlets", "Cross Country Skiing", "Foodies", "Geeks" }
            );

            for (int i = 0; i < 100; i++)
            {
                Group group = new Group();
                group.Name = groupNameGenerator.GetPhrase();
                IEnumerable<User> groupMembers = context.Users.Local.ChoseRandomSet(random.Next(3, context.Users.Local.Count - 2));
                foreach (User groupMember in groupMembers)
                {
                    group.Users.Add(groupMember);
                }
                context.Groups.Add(group);
            }

            RandomPhraseGenerator recipeNameGenerator = new RandomPhraseGenerator(
                new[] { "Delicious", "Amazing", "Grandma's", "Tasty", "Mom's", "Dad's", "Sweet and Sour", "Best-Ever", "Classy", "Easy", "French", "Flaky", "Fast and Easy", "Killer", "Ginger", "Curried", "Magic", "Stuffed", "Wild"},
                new[] { "Apple", "Bran", "Ham", "Chicken", "Beef", "Steak", "Lobster", "Mango", "Vegetable", "Potato", "Liver", "Halibut", "Cod", "Shrimp", "Snapper", "Eggplant", "Ground Beef", "Blueberry", "Chocolate", "Mushroom", "Sausage", "Unicorn", "Fish", "Venison", "Elk" },
                new[] { "Lasagna", "Pie", "Muffins", "Casserole", "Stew", "Cake", "Cheesecake", "Salad", "Fajitas", "Soup", "Bake", "Stir-Fry", "Pilaf", "Dip", "Triangles", "Cookies" }
                );

            RandomPhraseGenerator ingredientGenerator = new RandomPhraseGenerator(
                new[] { "1/4", "1/3", "1/2", "2/3", "1", " 1 1/4", "1 1/2", "2", "3", "4", "5"},
                new[] { "Tbsp.", "Tsp.", "ml", "Lbs", "Cup", "Kg"},
                new[] { "Salt", "Pepper", "Olive Oil", "Basil", "Chicken Broth", "Cloves", "Garlic", "Onion", "Eggs",
                        "Tomatoes", "Chicken Thighs", "Chicken Breast", "Pork Ribs", "Mozzarella Cheese", "Parmesan Cheese", "Butter", "Nutmeg",
                        "Fettuccine", "Parsley", "Orange Zest", "Whipping Cream", "Cranberries", "Tomatoe Paste", "Mushrooms", "Cambozola Cheese",
                        "Green Onions", "Flour", "Bran", "Green Beans", "Lima Beans", "Vinegar", "Mustard", "Water", "Raisins", "Liver", "Potato",
                        "Corn", "Tabasco", "Cashews", "Celery", "Ground Beef", "Rib Steak", "Asparagus", "Cooking Oil", "Soy Sauce",
                        "Ketchup", "Round Roast", "Beef Tenderloin", "Pork Tenderloin", "Pork Ribs"
                }
            );

            RandomPhraseGenerator directionsGenerator = new RandomPhraseGenerator(
                new []{"Soak", "Mix", "Pour", "Throw Out", "Beat", "Bake", "Stir", "Place", "Roll", "Combine"},
                new []{"the"},
                new[] { "Salt", "Pepper", "Olive Oil", "Basil", "Chicken Broth", "Cloves", "Garlic", "Onion", "Eggs",
                        "Tomatoes", "Chicken Thighs", "Chicken Breast", "Pork Ribs", "Mozzarella Cheese", "Parmesan Cheese", "Butter", "Nutmeg",
                        "Fettuccine", "Parsley", "Orange Zest", "Whipping Cream", "Cranberries", "Tomatoe Paste", "Mushrooms", "Cambozola Cheese",
                        "Green Onions", "Flour", "Bran", "Green Beans", "Lima Beans", "Vinegar", "Mustard", "Water", "Raisins",
                        "Corn", "Tabasco", "Cashews", "Celery", "Ground Beef", "Rib Steak", "Asparagus", "Cooking Oil", "Soy Sauce",
                        "Ketchup", "Round Roast", "Beef Tenderloin", "Pork Tenderloin", "Pork Ribs"
                },
                new [] {"and", "or"},
                new[] { "Salt", "Pepper", "Olive Oil", "Basil", "Chicken Broth", "Cloves", "Garlic", "Onion", "Eggs",
                        "Tomatoes", "Chicken Thighs", "Chicken Breast", "Pork Ribs", "Mozzarella Cheese", "Parmesan Cheese", "Butter", "Nutmeg",
                        "Fettuccine", "Parsley", "Orange Zest", "Whipping Cream", "Cranberries", "Tomatoe Paste", "Mushrooms", "Cambozola Cheese",
                        "Green Onions", "Flour", "Bran", "Green Beans", "Lima Beans", "Vinegar", "Mustard", "Water", "Raisins",
                        "Corn", "Tabasco", "Cashews", "Celery", "Ground Beef", "Rib Steak", "Asparagus", "Cooking Oil", "Soy Sauce",
                        "Ketchup", "Round Roast", "Beef Tenderloin", "Pork Tenderloin", "Pork Ribs"
                },
                new[] { "in", "on", "over" },
                new [] {"oven", "bowl", "pan", "casserole dish", "large bowl", "waxed paper", "tin foil", "cookie sheet", "stove"},
                new [] {"for"},
                new [] {"2" , "5", "10", "15", "25", "35", "45", "60"},
                new [] {"minutes", "seconds"}                
                );

            for (int i = 0; i < 500; i++)
            {
                Recipe recipe = new Recipe();
                recipe.Name = recipeNameGenerator.GetPhrase();
                recipe.User = context.Users.Local[random.Next(context.Users.Local.Count - 1)];
                recipe.PostedOn = DateTime.Now.AddDays(random.Next(-30, 30)).AddHours(random.Next(-12, 12));
                IEnumerable<Group> groupsToShareWith =
                    context.Groups.Local.ChoseRandomSet(random.Next(context.Groups.Local.Count - 15));
                foreach (Group groupToShareWith in groupsToShareWith)
                {
                    recipe.Groups.Add(groupToShareWith);
                }
                int numberOfIngredients = random.Next(8, 15);
                StringBuilder ingredients = new StringBuilder();
                for (int j = 0; j < numberOfIngredients - 1; j++)
                {
                    ingredients.AppendLine(ingredientGenerator.GetPhrase());
                }
                recipe.Ingredients = ingredients.ToString();

                int numberOfDirections = random.Next(6, 12);
                StringBuilder directions = new StringBuilder();
                for (int j = 1; j < numberOfDirections; j++)
                {
                    directions.AppendFormat("{0}. {1}", j, directionsGenerator.GetPhrase());
                    directions.AppendLine();
                }
                recipe.Directions = directions.ToString();
                context.Recipes.Add(recipe);
            }
            base.Seed(context);
        }
    }

    public static class CollectionExtesions
    {
        public static IEnumerable<T> ChoseRandomSet<T>(this IList<T> collection, int numberOfItems)
        {
            List<T> result = new List<T>();
            Random random = new Random();
            for (int i = 0; i < numberOfItems; i++)
            {
                T nextItem = collection[random.Next(collection.Count - 1)];
                while (result.Contains(nextItem))
                {
                    nextItem = collection[random.Next(collection.Count - 1)];
                }
                result.Add(nextItem);
            }
            return result;
        }
    }

    public class RandomPhraseGenerator
    {
        private List<IList<string>> _phraseChoices;
        private Random _random;


        public RandomPhraseGenerator(params IEnumerable<string>[] phraseChoices)
        {
            _random = new Random();
            _phraseChoices = new List<IList<string>>();
            foreach (IEnumerable<string> phraseChoice in phraseChoices)
            {
                _phraseChoices.Add(new List<string>(phraseChoice));
            }
        }

        public string GetPhrase()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (IList<string> phraseChoice in _phraseChoices)
            {
                stringBuilder.Append(phraseChoice[_random.Next(phraseChoice.Count - 1)]);
                stringBuilder.Append(" ");
            }
            return stringBuilder.ToString().Trim();
        }

    }
}