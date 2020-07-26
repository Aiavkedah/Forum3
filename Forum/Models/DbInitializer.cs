using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class DbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        IdentityRole adminRole;
        IdentityRole userRole;
        ApplicationUserManager userManager;

        protected override void Seed(ApplicationDbContext context)
        {
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            adminRole = new IdentityRole { Name = "admin" };
            userRole = new IdentityRole { Name = "user" };

            roleManager.Create(adminRole);
            roleManager.Create(userRole);

            var admin = new ApplicationUser { UserName = "Admin", Email = "admin@test.com", EmailConfirmed = true, Id = "1" };
            AddUser(admin);
            var user = new ApplicationUser { UserName = "User", Email = "user@test.com", EmailConfirmed = true, Id = "2" };
            AddUser(user);

            context.ForumCategories.Add(new ForumCategory { ID = 1, Text = "Movies" });
            context.ForumCategories.Add(new ForumCategory { ID = 2, Text = "Sports" });
            context.ForumCategories.Add(new ForumCategory { ID = 2, Text = "Games" });

            context.ForumPosts.Add(new ForumPost { ID = 1, Text = "New movies", ForumCategoryId = 1, ForumUserId = "1", Date = DateTime.Now });
            context.ForumPosts.Add(new ForumPost { ID = 2, Text = "Top movies", ForumCategoryId = 1, ForumUserId = "2", Date = DateTime.Now });

            context.ForumComments.Add(new ForumComment { ID = 1, Text = "The Shawshank Redemption. Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", 
                                                         ForumPostId = 1, ForumUserId = "2", Date = DateTime.Now });
            context.ForumComments.Add(new ForumComment { ID = 1, Text = "The Godfather. The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", 
                                                         ForumPostId = 1, ForumUserId = "2", Date = DateTime.Now });
            context.ForumComments.Add(new ForumComment { ID = 1, Text = "Schindler's List. In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.", 
                                                         ForumPostId = 1, ForumUserId = "1", Date = DateTime.Now });
            context.ForumComments.Add(new ForumComment { ID = 1, Text = "The Lord of the Rings: The Return of the King. Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.", 
                                                         ForumPostId = 1, ForumUserId = "1", Date = DateTime.Now });
            context.ForumComments.Add(new ForumComment { ID = 1, Text = "Fight Club. An insomniac office worker and a devil-may-care soapmaker form an underground fight club that evolves into something much, much more.", 
                                                         ForumPostId = 1, ForumUserId = "1", Date = DateTime.Now });
            context.ForumComments.Add(new ForumComment { ID = 1, Text = "Star Wars: Episode V - The Empire Strikes Back. After the Rebels are brutally overpowered by the Empire on the ice planet Hoth, Luke Skywalker begins Jedi training with Yoda, while his friends are pursued by Darth Vader and a bounty hunter named Boba Fett all over the galaxy.", 
                                                         ForumPostId = 1, ForumUserId = "1", Date = DateTime.Now });

            base.Seed(context);
        }

        private void AddUser(ApplicationUser user)
        {
            string password = "Qq!123";
            var result = userManager.Create(user, password);

            if (result.Succeeded)
            {
                if (user.UserName == "Admin")
                {
                    userManager.AddToRole(user.Id, adminRole.Name);
                }

                userManager.AddToRole(user.Id, userRole.Name);
            }
        }
    }
}