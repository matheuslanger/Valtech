namespace ASPMVC.Migrations
{
    using ASPMVC.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ASPMVC.Models.MovieDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "ASPMVC.Models.MovieDBContext";
        }

        //protected override void Seed(ASPMVC.Models.MovieDBContext context)
        //{
        //    context.Movies.AddOrUpdate(i => i.Original_title,
        //        new Movie
        //        {
        //            Original_title = "When Harry Met Sally",
        //            Release_date = "1989-1-11",
        //            Genres = "Romantic Comedy",
        //            Vote_average = "PG",
        //            Budget = 7.99M
        //        },

        //         new Movie
        //         {
        //             Original_title = "Ghostbusters ",
        //             Release_date = "1984-3-13",
        //             Genres = "Comedy",
        //             Vote_average = "PG-13",
        //             Budget = 8.99M
        //         },

        //         new Movie
        //         {
        //             Original_title = "Ghostbusters 2",
        //             Release_date = "1986-2-23",
        //             Genres = "Comedy",
        //             Vote_average = "PG-13",
        //             Budget = 9.99M
        //         },

        //       new Movie
        //       {
        //           Original_title = "Rio Bravo",
        //           Release_date = "1959-4-15",
        //           Genres = "Western",
        //           Vote_average = "R",
        //           Budget = 3.99M
        //       }
        //   );

        //}
    }
}
