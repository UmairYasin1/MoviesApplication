using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MoviesApplication.Models
{
    public class ApplicationDbContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeGenre(context);
            base.Seed(context);
        }


        private void InitializeGenre(ApplicationDbContext context)
        {
            Genre genre1 = new Genre()
            {
                Name = "Action",
                Description = "An action story is similar to adventure, and the protagonist usually takes a risky turn, which leads to desperate situations (including explosions, fight scenes, daring escapes, etc.)"
            };

            Genre genre2 = new Genre()
            {
                Name = "Comedy",
                Description = "Comedy is a story that tells about a series of funny, or comical events, intended to make the audience laugh. It is a very open genre, and thus crosses over with many other genres on a frequent basis."
            };
            Genre genre3 = new Genre()
            {
                Name = "Horror",
                Description = "A horror story is told to deliberately scare or frighten the audience, through suspense, violence or shock."
            };

            context.Genre.Add(genre1);
            context.Genre.Add(genre2);
            context.Genre.Add(genre3);
        }


    }
}